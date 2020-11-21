using RageLib.Resources.GTA5.PC.Meta.Types;

namespace RageLib.Resources.GTA5.PC.Meta.Structures
{
    public class CMapTypes : IMetaStructure
	{
		public int StructureSize => 0x50;

		public uint pad_0h;
		public uint pad_4h;
		public Array_StructurePointer extensions;
		public Array_StructurePointer archetypes;
		public MetaHash name;
		public uint pad_2Ch;
		public Array_uint dependencies;
		public Array_Structure compositeEntityTypes;

		public StructureInfo GetStructureInfo()
		{
			throw new System.NotImplementedException();
		}
	}
}