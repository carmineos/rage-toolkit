using RageLib.Resources.GTA5.PC.Meta.Types;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CExtensionDefAudioCollisionSettings : CExtensionDef
    {
        public int BlockLengthStructure => 0x30;

        public MetaHash settings;
        public uint pad_24h;
        public uint pad_28h;
        public uint pad_2Ch;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
