﻿using System;
using DataTool.Flag;
using DataTool.ToolLogic.Extract;

namespace DataTool.ToolLogic.Debug {
    [Tool("te-map-placable-dump", Description = "", IsSensitive = true, CustomFlags = typeof(ExtractFlags))]
    class DebugMapDump : ITool {
        public void Parse(ICLIFlags toolFlags) {
            throw new Exception("does not work in ow2");
            /*var flags = toolFlags as ExtractFlags;
            var testguids = flags.Positionals.Skip(3).Select(x => uint.Parse(x, System.Globalization.NumberStyles.HexNumber));
            foreach (var guid in Program.TrackedFiles[0x9F]) {
                if (testguids.Contains(teResourceGUID.Index(guid))) {
                    var path = Path.Combine(flags.OutputPath, "teMapPlacable", teResourceGUID.Index(guid).ToString("X"));
                    STUMapHeader map = GetInstance<STUMapHeader>(guid);

                    foreach (var t in Enum.GetValues(typeof(Enums.teMAP_PLACEABLE_TYPE))) {
                        var teType = (Enums.teMAP_PLACEABLE_TYPE) t;
                        if (teType == Enums.teMAP_PLACEABLE_TYPE.UNKNOWN) {
                            continue;
                        }

                        var o = Path.Combine(path, teType.ToString());
                        if (teMapPlaceableData.Manager.Types.ContainsKey(teType)) {
                            continue;
                        }

                        if (!Directory.Exists(o)) {
                            Directory.CreateDirectory(o);
                        }

                        teMapPlaceableData placable = GetPlaceableData(map, teType);
                        for (int i = 0; i < placable.Header.PlaceableCount; ++i) {
                            var commonStructure = placable.CommonStructures[i];
                            using (var f = File.OpenWrite(Path.Combine(o, commonStructure.UUID.Value.ToString("N")))) {
                                f.Write(((teMapPlaceableDummy) placable.Placeables[i]).Data, 0, ((teMapPlaceableDummy) placable.Placeables[i]).Data.Length);
                            }
                        }
                    }
                }
            }*/
        }

        /*public static teMapPlaceableData GetPlaceableData(STUMapHeader map, Enums.teMAP_PLACEABLE_TYPE modelGroup) {
            using (Stream stream = OpenFile(map.GetChunkKey(modelGroup))) {
                if (stream == null) return null;
                return new teMapPlaceableData(stream, modelGroup);
            }
        }

        public static teMapPlaceableData GetPlaceableData(STUMapHeader map, byte type) {
            return GetPlaceableData(map, (Enums.teMAP_PLACEABLE_TYPE) type);
        }*/
    }
}
