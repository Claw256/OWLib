// Generated by TankLibHelper
using TankLib.STU.Types.Enums;

// ReSharper disable All
namespace TankLib.STU.Types
{
    [STU(0x494C33C4, 240)]
    public class STUGameMode : STUInstance
    {
        [STUField(0xA43573F4, 8)] // size: 16
        public teStructuredDataAssetRef<STUIdentifier>[] m_A43573F4;

        [STUField(0xCF63B633, 24)] // size: 16
        public teStructuredDataAssetRef<STU_6BE90C5C> m_CF63B633;

        [STUField(0x3CE93B76, 40, ReaderType = typeof(InlineInstanceFieldReader))] // size: 16
        public STUGameModeVarValuePair[] m_3CE93B76;

        [STUField(0xF88BA3B9, 56)] // size: 16
        public teStructuredDataAssetRef<STU_6BE90C5C> m_F88BA3B9;

        [STUField(0xAD4BF17F, 72, ReaderType = typeof(InlineInstanceFieldReader))] // size: 16
        public STUGameModeVarValuePair[] m_AD4BF17F;

        [STUField(0x5DB91CE2, 88)] // size: 16
        public teStructuredDataAssetRef<ulong> m_displayName;

        [STUField(0x6EB38130, 104)] // size: 16
        public teStructuredDataAssetRef<ulong> m_6EB38130;

        [STUField(0xE04197AF, 120)] // size: 16
        public teStructuredDataAssetRef<STUGameRulesetSchema>[] m_gameRulesetSchemas;

        [STUField(0xD440A0F7, 136, ReaderType = typeof(InlineInstanceFieldReader))] // size: 16
        public STUGameModeTeam[] m_teams;

        [STUField(0xDA642982, 152, ReaderType = typeof(InlineInstanceFieldReader))] // size: 16
        public STUGameModeLoadoutOverride[] m_loadoutOverrides;

        [STUField(0xF82653D0, 168)] // size: 16
        public teStructuredDataAssetRef<STUCelebration>[] m_F82653D0;

        [STUField(0x7F5B54B2, 184)] // size: 16
        public teStructuredDataAssetRef<ulong> m_7F5B54B2;

        [STUField(0xCBAE46D4, 200)] // size: 4
        public float m_CBAE46D4;

        [STUField(0x37D4F9CD, 204)] // size: 4
        public int m_37D4F9CD;

        [STUField(0x8A5415B9, 208)] // size: 4
        public Enum_1964FED7 m_gameModeType;

        [STUField(0xE2B6AAC3, 212)] // size: 4
        public Enum_3285FBF5 m_E2B6AAC3 = Enum_3285FBF5.x0FB42EC4;

        [STUField(0x7E0B4B96, 216)] // size: 4
        public float m_7E0B4B96 = 1f;

        [STUField(0x47712EFC, 220)] // size: 4
        public float m_47712EFC = 0.25f;

        [STUField(0x70064613, 224)] // size: 1
        public byte m_70064613;

        [STUField(0xF3E24B6F, 225)] // size: 1
        public byte m_F3E24B6F;

        [STUField(0x040601B2, 226)] // size: 1
        public byte m_040601B2;

        [STUField(0x0DD0C65E, 227)] // size: 1
        public byte m_0DD0C65E;

        [STUField(0x0FC17230, 228)] // size: 1
        public byte m_0FC17230;

        [STUField(0x372E20EB, 229)] // size: 1
        public byte m_372E20EB;

        [STUField(0xF40CE5D4, 230)] // size: 1
        public byte m_F40CE5D4 = 0x1;

        [STUField(0x96287171, 231)] // size: 1
        public byte m_96287171;

        [STUField(0xC61D2304, 232)] // size: 1
        public byte m_C61D2304 = 0x1;

        [STUField(0x8197C483, 233)] // size: 1
        public byte m_8197C483 = 0x0;

        [STUField(0x7B4403BD, 234)] // size: 1
        public byte m_7B4403BD = 0x0;

        [STUField(0x96C5A6F6, 235)] // size: 1
        public byte m_96C5A6F6;

        [STUField(0x7F0EE67B, 236)] // size: 1
        public byte m_7F0EE67B;
    }
}
