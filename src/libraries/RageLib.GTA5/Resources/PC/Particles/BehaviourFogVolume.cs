// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System;

namespace RageLib.Resources.GTA5.PC.Particles
{
    // ptxu_FogVolume
    public class BehaviourFogVolume : Behaviour
    {
        public override long BlockLength => 0x430;

        // structure data
        public ResourcePointerList64<KeyframeProp> KeyframeProps;
        public ulong Unknown_20h; // 0x0000000000000000
        public ulong Unknown_28h; // 0x0000000000000000
        public KeyframeProp KeyframeProp0;
        public KeyframeProp KeyframeProp1;
        public KeyframeProp KeyframeProp2;
        public KeyframeProp KeyframeProp3;
        public KeyframeProp KeyframeProp4;
        public KeyframeProp KeyframeProp5;
        public KeyframeProp KeyframeProp6;
        public uint Unknown_420h;
        public uint Unknown_424h;
        public uint Unknown_428h;
        public uint Unknown_42Ch;

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
            this.KeyframeProp2 = reader.ReadBlock<KeyframeProp>();
            this.KeyframeProp3 = reader.ReadBlock<KeyframeProp>();
            this.KeyframeProp4 = reader.ReadBlock<KeyframeProp>();
            this.KeyframeProp5 = reader.ReadBlock<KeyframeProp>();
            this.KeyframeProp6 = reader.ReadBlock<KeyframeProp>();
            this.Unknown_420h = reader.ReadUInt32();
            this.Unknown_424h = reader.ReadUInt32();
            this.Unknown_428h = reader.ReadUInt32();
            this.Unknown_42Ch = reader.ReadUInt32();
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
            writer.WriteBlock(this.KeyframeProp2);
            writer.WriteBlock(this.KeyframeProp3);
            writer.WriteBlock(this.KeyframeProp4);
            writer.WriteBlock(this.KeyframeProp5);
            writer.WriteBlock(this.KeyframeProp6);
            writer.Write(this.Unknown_420h);
            writer.Write(this.Unknown_424h);
            writer.Write(this.Unknown_428h);
            writer.Write(this.Unknown_42Ch);
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(16, KeyframeProps),
                new Tuple<long, IResourceBlock>(48, KeyframeProp0),
                new Tuple<long, IResourceBlock>(192, KeyframeProp1),
                new Tuple<long, IResourceBlock>(336, KeyframeProp2),
                new Tuple<long, IResourceBlock>(480, KeyframeProp3),
                new Tuple<long, IResourceBlock>(624, KeyframeProp4),
                new Tuple<long, IResourceBlock>(768, KeyframeProp5),
                new Tuple<long, IResourceBlock>(912, KeyframeProp6)
            };
        }
    }
}
