using System;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CScenarioPointLookUps : IMetaStructure
    {
        public int StructureSize => 0x60;

        public Array_uint TypeNames;
        public Array_uint PedModelSetNames;
        public Array_uint VehicleModelSetNames;
        public Array_uint GroupNames;
        public Array_uint InteriorNames;
        public Array_uint RequiredIMapNames;

        public StructureInfo GetStructureInfo()
        {
            throw new NotImplementedException();
        }
    }
}
