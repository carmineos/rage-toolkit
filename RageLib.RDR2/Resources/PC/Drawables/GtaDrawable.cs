// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using RageLib.Resources.RDR2.PC.Bounds;
using System;
using System.Collections.Generic;

namespace RageLib.Resources.RDR2.PC.Drawables
{
    // pgBase
    // rmcDrawableBase
    // rmcDrawable
    // gtaDrawable
    public class GtaDrawable : PgBase64
	{
		public override long BlockLength => 0xD0;

		// structure data
		private ulong ShaderGroupPointer;
		public ulong Unknown_18h; // 0x0000000000000000                   
		public RmcLodGroup LodGroup;
		public ulong Unknown_90h; // 0x0000000000000000
		public ulong Unknown_98h; // 0x0000000000000000
		public ulong Unknown_A0h; // 0x0000000000000000
		private ulong NamePointer;
		public ulong Unknown_B0h; // 0x0000000000000000
		private ulong BoundPointer;
		private ulong Unknown_C0h;
		public ulong Unknown_C8h; // 0x0000000000000000

		// reference data
		public GrmShaderGroup? ShaderGroup { get; set; }
		public string_r? Name { get; set; }
		public PhBound? Bound { get; set; }

        public GtaDrawable()
        {
			LodGroup = new RmcLodGroup();
        }

		public override void Read(ResourceDataReader reader, params object[] parameters)
		{
			base.Read(reader, parameters);

			// read structure data
			ShaderGroupPointer = reader.ReadUInt64();
			Unknown_18h = reader.ReadUInt64();
			LodGroup = reader.ReadBlock<RmcLodGroup>();
			Unknown_90h = reader.ReadUInt64();
			Unknown_98h = reader.ReadUInt64();
			Unknown_A0h = reader.ReadUInt64();
			NamePointer = reader.ReadUInt64();
			Unknown_B0h = reader.ReadUInt64();
			BoundPointer = reader.ReadUInt64();
			Unknown_C0h = reader.ReadUInt64();
			Unknown_C8h = reader.ReadUInt64();

			// read reference data
			ShaderGroup = reader.ReadBlockAt<GrmShaderGroup>(ShaderGroupPointer);
			Name = reader.ReadBlockAt<string_r>(NamePointer);
			Bound = reader.ReadBlockAt<PhBound>(BoundPointer);
		}

		public override void Write(ResourceDataWriter writer, params object[] parameters)
		{
			base.Write(writer, parameters);

			// update structure data
			ShaderGroupPointer = (ulong)(ShaderGroup?.BlockPosition ?? 0);
			NamePointer = (ulong)(Name?.BlockPosition ?? 0);
			BoundPointer = (ulong)(Bound?.BlockPosition ?? 0);

			// write structure data
			writer.Write(ShaderGroupPointer);
			writer.Write(Unknown_18h);
			writer.WriteBlock(LodGroup);
			writer.Write(Unknown_90h);
			writer.Write(Unknown_98h);
			writer.Write(Unknown_A0h);
			writer.Write(NamePointer);
			writer.Write(Unknown_B0h);
			writer.Write(BoundPointer);
			writer.Write(Unknown_C0h);
			writer.Write(Unknown_C8h);
		}

		public override Tuple<long, IResourceBlock>[] GetParts()
		{
			return new Tuple<long, IResourceBlock>[]
			{
				new Tuple<long, IResourceBlock>(0x20, LodGroup),
			};
		}

		public override IResourceBlock[] GetReferences()
		{
			var list = new List<IResourceBlock>(base.GetReferences());
			if (ShaderGroup is not null) list.Add(ShaderGroup);
			if (Name is not null) list.Add(Name);
			if (Bound is not null) list.Add(Bound);
			return list.ToArray();
		}
	}
}
