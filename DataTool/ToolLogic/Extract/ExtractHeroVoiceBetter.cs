﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using DataTool.DataModels;
using DataTool.DataModels.Hero;
using DataTool.FindLogic;
using DataTool.Flag;
using DataTool.Helper;
using DataTool.JSON;
using TankLib;
using TankLib.STU.Types;
using static DataTool.Helper.STUHelper;
using static DataTool.Helper.IO;
using SkinTheme = DataTool.SaveLogic.Unlock.SkinTheme;

namespace DataTool.ToolLogic.Extract {
    [Tool("extract-hero-voice", Description = "Extracts hero voicelines.", CustomFlags = typeof(ExtractFlags))]
    class ExtractHeroVoice : ExtractHeroVoiceBetter {
        protected override string Container => "HeroVoice";
    }

    [Tool("extract-hero-voice-better", IsSensitive = true, Description = "Extracts hero voicelines but groups them a bit better.", CustomFlags = typeof(ExtractFlags))]
    class ExtractHeroVoiceBetter : JSONTool, ITool {
        protected virtual string Container => "BetterHeroVoice";
        private static readonly HashSet<ulong> SoundIdCache = new HashSet<ulong>();

        public void Parse(ICLIFlags toolFlags) {
            string basePath;
            if (toolFlags is ExtractFlags flags) {
                basePath = Path.Combine(flags.OutputPath, Container);
            } else {
                throw new Exception("no output path");
            }

            // Do normal heroes first then NPCs, this is because NPCs have a lot of duplicate sounds and normal heroes (should) have none
            // so any duplicate sounds would only come up while processing NPCs which can be ignored as they (probably) belong to heroes
            var heroes = Program.TrackedFiles[0x75]
                .Select(x => new Hero(x))
                .OrderBy(x => !x.IsHero)
                .ThenBy(x => x.GUID.GUID)
                .ToArray();

            var validNames = heroes.GroupBy(x => x.Name ?? $"Unknown{teResourceGUID.Index(x.GUID)}").ToDictionary(x => x.Key, x => x.Select(y => y.GUID.GUID).ToArray(), StringComparer.InvariantCultureIgnoreCase);
            var validGuids = heroes.Select(x => teResourceGUID.LongKey(x.GUID.GUID)).ToHashSet();

            var query = new HashSet<ulong>();
            foreach (var positional in flags.Positionals.Skip(3)) {
                if(validNames.TryGetValue(positional, out var guids)) {
                    foreach (var guid in guids) {
                        query.Add(teResourceGUID.LongKey(guid));
                    }
                    continue;
                }

                if (TryGetLocalizedName(0x75, positional, out var localizedNameGuid) && validGuids.Contains(localizedNameGuid)) {
                    query.Add(teResourceGUID.LongKey(localizedNameGuid));
                    continue;
                }

                if (ulong.TryParse(positional, NumberStyles.HexNumber, null, out var parsedGuid)) {
                    query.Add(teResourceGUID.LongKey(parsedGuid));
                }
            }

            foreach (var hero in heroes) {
                if (query.Count > 0 && !query.Contains(teResourceGUID.LongKey(hero.GUID.GUID))) {
                    continue;
                }

                var heroStu = GetInstance<STUHero>(hero.GUID);

                string heroName = GetValidFilename(GetCleanString(heroStu.m_0EDCE350) ?? $"Unknown{teResourceGUID.Index(hero.GUID)}");

                Logger.Log($"Processing {heroName}");

                Combo.ComboInfo baseInfo = default;
                var heroVoiceSetGuid = GetInstance<STUVoiceSetComponent>(heroStu.m_gameplayEntity)?.m_voiceDefinition;

                if (SaveVoiceSet(flags, basePath, heroName, "Default", heroVoiceSetGuid, ref baseInfo)) {
                    var skins = new ProgressionUnlocks(heroStu).GetUnlocksOfType(UnlockType.Skin);

                    foreach (var unlock in skins) {
                        var unlockSkinTheme = unlock.STU as STUUnlock_SkinTheme;
                        if (unlockSkinTheme?.m_0B1BA7C1 != 0) {
                            continue; // no idea what this is
                        }

                        TACTLib.Logger.Debug("Tool", $"Processing skin {unlock.GetName()}");
                        Combo.ComboInfo info = default;
                        var skinTheme = GetInstance<STUSkinBase>(unlockSkinTheme.m_skinTheme);
                        if (skinTheme == null) {
                            continue;
                        }

                        SaveVoiceSet(flags, basePath, heroName, GetValidFilename(unlock.GetName()), heroVoiceSetGuid, ref info, baseInfo, SkinTheme.GetReplacements(skinTheme));
                    }
                }
            }
        }

        public static bool SaveVoiceSet(ExtractFlags flags, string basePath, string heroName, string unlockName, ulong? voiceSetGuid, ref Combo.ComboInfo info, Combo.ComboInfo baseCombo = null, Dictionary<ulong, ulong> replacements = null, bool ignoreGroups = false) {
            if (voiceSetGuid == null) {
                return false;
            }

            info = new Combo.ComboInfo();
            var saveContext = new SaveLogic.Combo.SaveContext(info);
            Combo.Find(info, voiceSetGuid.Value, replacements);

            // if we're processing a skin, baseCombo is the combo from the hero, this remove duplicate check removes any sounds that belong to the base hero
            // this ensures you only get sounds unique to the skin when processing a skin
            if (baseCombo != null) {
                if (!Combo.RemoveDuplicateVoiceSetEntries(baseCombo, ref info, voiceSetGuid.Value, Combo.GetReplacement(voiceSetGuid.Value, replacements)))
                    return false;
            }

            foreach (var voiceSet in info.m_voiceSets) {
                if (voiceSet.Value.VoiceLineInstances == null) continue;

                foreach (var voicelineInstanceInfo in voiceSet.Value.VoiceLineInstances) {
                    foreach (var voiceLineInstance in voicelineInstanceInfo.Value) {
                        if (!voiceLineInstance.SoundFiles.Any()) {
                            continue;
                        }

                        var stimulus = GetInstance<STUVoiceStimulus>(voiceLineInstance.VoiceStimulus);
                        if (stimulus == null) continue;

                        var groupName = GetVoiceGroup(voiceLineInstance.VoiceStimulus, stimulus.m_category, stimulus.m_87DCD58E) ??
                                        Path.Combine("Unknown",$"{teResourceGUID.Index(voiceLineInstance.VoiceStimulus):X}.{teResourceGUID.Type(voiceLineInstance.VoiceStimulus):X3}");

                        var stack = new List<string> { basePath };

                        CalculatePathStack(flags, heroName, unlockName, ignoreGroups ? "" : groupName, stack);

                        var path = Path.Combine(stack.Where(x => x.Length > 0).ToArray());

                        stack.Clear();
                        stack.Add(basePath);

                        string hero03FDir;
                        if (flags.VoiceGroup03FInType) {
                            hero03FDir = Path.Combine(path, "03F");
                        } else {
                            CalculatePathStack(flags, heroName, unlockName, "03F", stack);
                            hero03FDir = Path.Combine(stack.ToArray());
                        }

                        // 99% of voiceline instances only have a single sound file however there are cases where some NPCs have multiple
                        // the Junkenstein Narrator is an example, the lines are the same however they are spoken differently.
                        foreach (var soundFile in voiceLineInstance.SoundFiles) {
                            var soundFileGuid = teResourceGUID.AsString(soundFile);
                            string filename = null;

                            if (!flags.VoiceGroupByHero && !ignoreGroups) {
                                filename = $"{heroName}-{soundFileGuid}";
                            }

                            if (SoundIdCache.Contains(soundFile)) {
                                TACTLib.Logger.Debug("Tool", "Duplicate sound detected, ignoring.");
                                continue;
                            }

                            SoundIdCache.Add(soundFile);
                            //SaveLogic.Combo.SaveSoundFile(flags, path, saveContext, soundFile, true, filename);
                            SaveLogic.Combo.SaveVoiceLineInstance(flags, path, voiceLineInstance, filename, soundFile);
                        }

                        // Saves Wrecking Balls squeak sounds, no other heroes have sounds like this it seems
                        var stuSound = GetInstance<STUSound>(voiceLineInstance.ExternalSound);
                        if (stuSound?.m_C32C2195?.m_soundWEMFiles != null) {
                            foreach (var mSoundWemFile in stuSound?.m_C32C2195?.m_soundWEMFiles) {
                                if (BadSoundFiles.Contains(mSoundWemFile)) {
                                    continue;
                                }

                                SaveLogic.Combo.SaveSoundFile(flags, hero03FDir, mSoundWemFile);
                            }
                        }
                    }
                }
            }

            return true;
        }

        /* Here is the explanation for the code below:
        1. CalculatePathStack is a function that calculates the path stack needed to create the path for the voice line.
        2. flags is the same variable that was created in the code above, it is used to determine how the path stack is built.
        3. heroName is the name of the hero that the voice line is for.
        4. unlockName is the name of the hero's unlockable skin that the voice line is for.
        5. groupName is the name of the group that the voice line is for.
        6. stack is the path stack that is being built.
        7. If the voice group by locale flag is enabled, it adds the current locale to the path stack.
        8. If the voice group by hero flag is enabled, it checks if the voice group by type flag is disabled.
        9. If the voice group by type flag is disabled, it adds the group name to the path stack.
        10. It then adds the hero name to the path stack.
        11. If the voice group by skin flag is enabled, it adds the unlock name to the path stack.
        12. If the voice group by hero flag is disabled or the voice group by type flag is enabled, it adds the group name to the path stack.
        13. It then returns the path stack.

        in English:
        1. If VoiceGroupByLocale is set, it will add the current locale to the path stack.
        2. If VoiceGroupByHero is set, it will add the hero name, the voice group name (if VoiceGroupByType is not set) and the unlock name (if VoiceGroupBySkin is set) to the path stack.
        3. If VoiceGroupByHero is not set or VoiceGroupByType is set, it will add the voice group name to the path stack. */
        private static void CalculatePathStack(ExtractFlags flags, string heroName, string unlockName, string groupName, List<string> stack) {
            if (flags.VoiceGroupByLocale) {
                stack.Add(Program.Client.CreateArgs.SpeechLanguage ?? "enUS");
            }

            if (flags.VoiceGroupByHero) {
                if (!flags.VoiceGroupByType) {
                    stack.Add(groupName);
                }

                stack.Add(heroName);

                if (flags.VoiceGroupBySkin) {
                    stack.Add(unlockName);
                }
            }

            if (!flags.VoiceGroupByHero || flags.VoiceGroupByType) {
                stack.Add(groupName);
            }
        }

        public static string GetVoiceGroup(ulong stimulusGuid, ulong categoryGuid, ulong unkGuid) {
            return GetNullableGUIDName(stimulusGuid) ?? GetNullableGUIDName(categoryGuid) ?? GetNullableGUIDName(unkGuid);
        }

        // these are weird UI sounds that seem to be on every STUSound
        public static readonly HashSet<ulong> BadSoundFiles = new HashSet<ulong>() {
            0x7C0000000147482, // 000000147482.03F
            0x7C00000000958E8, // 0000000958E8.03F
            0x7C00000000958E6, // 0000000958E6.03F
            0x7C00000001BD800, // 0000001BD800.03F
            0x07C00000001BD7FF, // 0000001BD7FF.03F
        };
    }
}
