// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System;
using System.Collections.Generic;

namespace RageLib.Resources.RDR2.PC.Drawables
{
    // rage::grmShaderGroup
    // VFT = 0x0000000140912C88
    public class GrmShaderGroup : DatBase64
	{
		public override long BlockLength => 0x40;

		// structure data
		private ulong TextureDictionaryPointer;
		public ResourcePointerList64<Shader> Shaders;
		private ulong Unknown_20h; // 0x0000000000000000
		private ulong Unknown_28h; // 0x0000000000000000
		private ulong Unknown_30h; // 0x0000000000000000
		private ulong Unknown_38h; // 0x0000000000000000

		// reference data
		public PgDictionary64<Texture>? TextureDictionary { get; set; }

        public GrmShaderGroup()
        {
			Shaders = new ResourcePointerList64<Shader>();
        }

		public override void Read(ResourceDataReader reader, params object[] parameters)
		{
			base.Read(reader, parameters);

			// read structure data
			TextureDictionaryPointer = reader.ReadUInt64();
			Shaders = reader.ReadBlock<ResourcePointerList64<Shader>>();
			Unknown_20h = reader.ReadUInt64();
			Unknown_28h = reader.ReadUInt64();
			Unknown_30h = reader.ReadUInt64();
			Unknown_38h = reader.ReadUInt64();

			// read reference data
			TextureDictionary = reader.ReadBlockAt<PgDictionary64<Texture>>(TextureDictionaryPointer);
		}

		public override void Write(ResourceDataWriter writer, params object[] parameters)
		{
			base.Write(writer, parameters);

			// update structure data
			TextureDictionaryPointer = (ulong)(TextureDictionary?.BlockPosition ?? 0);

			// write structure data
			writer.Write(TextureDictionaryPointer);
			writer.WriteBlock(Shaders);
			writer.Write(Unknown_20h);
			writer.Write(Unknown_28h);
			writer.Write(Unknown_30h);
			writer.Write(Unknown_38h);
		}

		public override Tuple<long, IResourceBlock>[] GetParts()
		{
			return new Tuple<long, IResourceBlock>[] {
				new Tuple<long, IResourceBlock>(0x10, Shaders)
			};
		}

		public override IResourceBlock[] GetReferences()
		{
			var list = new List<IResourceBlock>(base.GetReferences());
			if (TextureDictionary is not null) list.Add(TextureDictionary);
			return list.ToArray();
		}
	}
}
