// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Runtime.CompilerServices;

namespace RageLib.Resources.Common
{
    // PgRef64<T> ?
    public class RAGE_Pointer<T> : ResourceSystemBlock where T : IResourceBlock, new()
	{
		public override long BlockLength => 0x8;

		// structure data
		public ulong Pointer;

		// reference data
		public T? Data { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
		{
			// read structure data
			this.Pointer = reader.ReadUInt64();

			// read reference data
			this.Data = reader.ReadBlockAt<T>(this.Pointer);
		}

		public override void Write(ResourceDataWriter writer, params object[] parameters)
		{
			// update structure data
			this.Pointer = (ulong)(this.Data?.BlockPosition ?? 0);

			// write structure data
			writer.Write(this.Pointer);
		}

		public override IResourceBlock[] GetReferences()
        {
			return Data is null ? Array.Empty<IResourceBlock>() : new IResourceBlock[] { Data };
		}
	}

	public struct PgRef64<T> where T : IResourceBlock, new()
	{
        // structure data
        private ulong Pointer;

        // reference data
        public T? Data { get; set; }

        //public PgRef64(ulong pointer)
        //{
        //	Pointer = pointer;
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator T?(PgRef64<T> pgRef64) => pgRef64.Data;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator PgRef64<T>(T? data) => new PgRef64<T>() { Data = data };

        public void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            Pointer = reader.ReadUInt64();
        }

        public void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            this.Pointer = (ulong)(this.Data?.BlockPosition ?? 0);

            // write structure data
            writer.Write(this.Pointer);
        }

        public void ReadReference(ResourceDataReader reader, params object[] parameters)
        {
			Data = reader.ReadBlockAt<T>(Pointer, parameters);
        }
	}
}
