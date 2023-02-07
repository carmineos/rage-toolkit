// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Collections;
using System;

namespace RageLib.Resources.GTA5.PC.Particles
{
    // ptxu_Wind
    public class BehaviourWind : Behaviour
    {
        public override long BlockLength => 0xF0;

        // structure data
        public ResourcePointerList64<KeyframeProp> KeyframeProps;
        public ulong Unknown_20h; // 0x0000000000000000
        public ulong Unknown_28h; // 0x0000000000000000
        public KeyframeProp KeyframeProp0;
        public ulong Unknown_C0h; // 0x0000000000000000
        public ulong Unknown_C8h; // 0x0000000000000000
        public uint Unknown_D0h;
        public uint Unknown_D4h;
        public uint Unknown_D8h;
        public uint Unknown_DCh;
        public uint Unknown_E0h;
        public uint Unknown_E4h; // 0x00000000
        public ulong Unknown_E8h; // 0x0000000000000000

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
            this.Unknown_C0h = reader.ReadUInt64();
            this.Unknown_C8h = reader.ReadUInt64();
            this.Unknown_D0h = reader.ReadUInt32();
            this.Unknown_D4h = reader.ReadUInt32();
            this.Unknown_D8h = reader.ReadUInt32();
            this.Unknown_DCh = reader.ReadUInt32();
            this.Unknown_E0h = reader.ReadUInt32();
            this.Unknown_E4h = reader.ReadUInt32();
            this.Unknown_E8h = reader.ReadUInt64();
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
            writer.Write(this.Unknown_C0h);
            writer.Write(this.Unknown_C8h);
            writer.Write(this.Unknown_D0h);
            writer.Write(this.Unknown_D4h);
            writer.Write(this.Unknown_D8h);
            writer.Write(this.Unknown_DCh);
            writer.Write(this.Unknown_E0h);
            writer.Write(this.Unknown_E4h);
            writer.Write(this.Unknown_E8h);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(16, KeyframeProps),
                new Tuple<long, IResourceBlock>(48, KeyframeProp0)
            };
        }
    }
}
