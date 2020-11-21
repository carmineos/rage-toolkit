namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CCompEntityAnims : IMetaStructure
    {
        public int StructureSize => 0xD8;

        public ArrayOfChars64 AnimDict;
        public ArrayOfChars64 AnimName;
        public ArrayOfChars64 AnimatedModel;
        public float punchInPhase;
        public float punchOutPhase;
        public Array_Structure effectsData;

        public StructureInfo GetStructureInfo()
        {
            throw new System.NotImplementedException();
        }
    }
}
