// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System;

namespace RageLib.Resources.GTA5.PC.Particles
{
    // ptxu_Decal
    public class BehaviourDecal : Behaviour
    {
        public override long BlockLength => 0x180;

        // structure data
        public ResourcePointerList64<KeyframeProp> KeyframeProps;
        public ulong Unknown_20h; // 0x0000000000000000
        public ulong Unknown_28h; // 0x0000000000000000
        public KeyframeProp KeyframeProp0;
        public KeyframeProp KeyframeProp1;
        public uint Unknown_150h;
        public float Unknown_154h;
        public float Unknown_158h;
        public float Unknown_15Ch;
        public float Unknown_160h;
        public float Unknown_164h;
        public float Unknown_168h;
        public float Unknown_16Ch;
        public uint Unknown_170h;
        public float Unknown_174h; // 0x3E99999A
        public float Unknown_178h; // 0x3F800000
        public uint Unknown_17Ch; // 0x00000000

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.KeyframeProps = reader.ReadBlock<ResourcePointerList64<KeyframeProp>>();
            this.Unknown_20h = reader.ReadUInt64();
            this.Unknown_28h = reader.ReadUInt64();
            this.KeyframeProp0 = reader.ReadBlock<KeyframeProp>();
            this.KeyframeProp1 = reader.ReadBlock<KeyframeProp>();
            this.Unknown_150h = reader.ReadUInt32();
            this.Unknown_154h = reader.ReadSingle();
            this.Unknown_158h = reader.ReadSingle();
            this.Unknown_15Ch = reader.ReadSingle();
            this.Unknown_160h = reader.ReadSingle();
            this.Unknown_164h = reader.ReadSingle();
            this.Unknown_168h = reader.ReadSingle();
            this.Unknown_16Ch = reader.ReadSingle();
            this.Unknown_170h = reader.ReadUInt32();
            this.Unknown_174h = reader.ReadSingle();
            this.Unknown_178h = reader.ReadSingle();
            this.Unknown_17Ch = reader.ReadUInt32();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.WriteBlock(this.KeyframeProps);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_28h);
            writer.WriteBlock(this.KeyframeProp0);
            writer.WriteBlock(this.KeyframeProp1);
            writer.Write(this.Unknown_150h);
            writer.Write(this.Unknown_154h);
            writer.Write(this.Unknown_158h);
            writer.Write(this.Unknown_15Ch);
            writer.Write(this.Unknown_160h);
            writer.Write(this.Unknown_164h);
            writer.Write(this.Unknown_168h);
            writer.Write(this.Unknown_16Ch);
            writer.Write(this.Unknown_170h);
            writer.Write(this.Unknown_174h);
            writer.Write(this.Unknown_178h);
            writer.Write(this.Unknown_17Ch);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(16, KeyframeProps),
                new Tuple<long, IResourceBlock>(48, KeyframeProp0),
                new Tuple<long, IResourceBlock>(192, KeyframeProp1)
            };
        }
    }
}
