using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CStreamingRequestFrame : IMetaStructure
    {
        public int StructureSize => 0x70;

        public Array_uint AddList;
        public Array_uint RemoveList;
        public Array_uint Unk_896120921;
        public Vector3 CamPos;
        public float pad_3Ch;
        public Vector3 CamDir;
        public float pad_4Ch;
        public Array_byte Unk_1762439591;
        public uint Flags;
        public uint pad_64h;
        public uint pad_68h;
        public uint pad_6Ch;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
