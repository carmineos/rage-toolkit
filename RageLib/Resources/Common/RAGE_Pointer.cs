using System;

namespace RageLib.Resources.Common
{
    // PgRef64<T> ?
    //public class RAGE_Pointer<T> : ResourceSystemBlock where T : IResourceBlock, new()
	//{
	//	public override long BlockLength => 0x8;
	//
	//	// structure data
	//	public ulong Pointer;
	//
	//	// reference data
	//	public T Data;
	//
	//	public override void Read(ResourceDataReader reader, params object[] parameters)
	//	{
	//		// read structure data
	//		this.Pointer = reader.ReadUInt64();
	//
	//		// read reference data
	//		this.Data = reader.ReadBlockAt<T>(this.Pointer);
	//	}
	//
	//	public override void Write(ResourceDataWriter writer, params object[] parameters)
	//	{
	//		// update structure data
	//		this.Pointer = (ulong)(this.Data != null ? this.Data.BlockPosition : 0);
	//
	//		// write structure data
	//		writer.Write(this.Pointer);
	//	}
	//
	//	public override IResourceBlock[] GetReferences()
    //    {
	//		return Data is null ? Array.Empty<IResourceBlock>() : new IResourceBlock[] { Data };
	//	}
	//}

	public struct Ref<T> where T : IResourceBlock, new()
    {
		public ulong Pointer;
		public T Data;

		public Ref(ulong position)
        {
			Pointer = position;
            Data = default;
        }

		public Ref(ulong position, T data)
		{
			Pointer = position;
			Data = data;
		}

		public static implicit operator ulong(Ref<T> item) => item.Pointer;
		
		public static implicit operator Ref<T>(ulong position) => new Ref<T>(position);

		public void ReadBlock(ResourceDataReader reader, params object[] parameters)
        {
			Data = reader.ReadBlockAt<T>(Pointer, parameters);
        }
	}
}
