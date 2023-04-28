// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Collections;
using RageLib.Resources.GTA5.PC.Bounds;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Clothes
{
    // pgBase
    // phVerletCloth
    public class VerletCloth : PgBase64
    {
        public override long BlockLength => 0x180;

        // structure data
        private uint Unknown_10h; // 0x00000000
        private uint Unknown_14h; // 0x00000000
        public ulong BoundPointer;
        private uint Unknown_20h; // 0x00000000
        private uint Unknown_24h; // 0x00000000
        private uint Unknown_28h; // 0x00000000
        private uint Unknown_2Ch; // 0x00000000
        private uint Unknown_30h;
        private uint Unknown_34h;
        private uint Unknown_38h;
        private uint Unknown_3Ch;
        private uint Unknown_40h;
        private uint Unknown_44h;
        private uint Unknown_48h;
        private uint Unknown_4Ch;
        private uint Unknown_50h;
        private uint Unknown_54h; // 0x00000001
        private uint Unknown_58h; // 0x00000000
        private uint Unknown_5Ch; // 0x00000000
        private uint Unknown_60h; // 0x00000000
        private uint Unknown_64h; // 0x00000000
        private uint Unknown_68h; // 0x00000000
        private uint Unknown_6Ch; // 0x00000000
        private SimpleList64<Vector4> Unknown_70h;
        private SimpleList64<Vector4> Unknown_80h;
        private uint Unknown_90h; // 0x00000000
        private uint Unknown_94h; // 0x00000000
        private uint Unknown_98h; // 0x00000000
        private uint Unknown_9Ch; // 0x00000000
        private uint Unknown_A0h; // 0x00000000
        private uint Unknown_A4h; // 0x00000000
        private uint Unknown_A8h;
        private uint Unknown_ACh;
        private uint Unknown_B0h; // 0x00000000
        private uint Unknown_B4h; // 0x00000000
        private uint Unknown_B8h; // 0x00000000
        private uint Unknown_BCh; // 0x00000000
        private uint Unknown_C0h; // 0x00000000
        private uint Unknown_C4h; // 0x00000000
        private uint Unknown_C8h; // 0x00000000
        private uint Unknown_CCh; // 0x00000000
        private uint Unknown_D0h; // 0x00000000
        private uint Unknown_D4h; // 0x00000000
        private uint Unknown_D8h; // 0x00000000
        private uint Unknown_DCh; // 0x00000000
        private uint Unknown_E0h; // 0x00000000
        private uint Unknown_E4h; // 0x00000000
        private uint Unknown_E8h;
        public uint NumEdges;
        private uint Unknown_F0h;
        private uint Unknown_F4h; // 0x00000000
        private uint Unknown_F8h;
        private uint Unknown_FCh; // 0x00000000
        public SimpleList64<EdgeData> CustomEdgeData;
        public SimpleList64<EdgeData> EdgeData;
        private uint Unknown_120h; // 0x00000000
        private uint Unknown_124h; // 0x00000000
        private uint Unknown_128h; // 0x00000000
        private uint Unknown_12Ch; // 0x00000000
        public ulong BehaviorPointer;
        private uint Unknown_138h; // 0x00100000
        private uint Unknown_13Ch; // 0x00000000
        private ulong Unknown_140h_Pointer;
        private uint Unknown_148h;
        private uint Unknown_14Ch; // 0x00000000
        private uint Unknown_150h; // 0x00000000
        private uint Unknown_154h; // 0x00000000
        private uint Unknown_158h;
        private uint Unknown_15Ch; // 0x00000000
        private uint Unknown_160h; // 0x00000000
        private uint Unknown_164h; // 0x00000000
        private uint Unknown_168h; // 0x00000000
        private uint Unknown_16Ch; // 0x00000000
        private uint Unknown_170h; // 0x00000000
        private uint Unknown_174h; // 0x00000000
        private uint Unknown_178h; // 0x00000000
        private uint Unknown_17Ch; // 0x00000000

        // reference data
        public Bound? Bound { get; set; }
        public EnvClothVerletBehavior? Behavior { get; set; }
        public Unknown_C_007? Unknown_140h_Data { get; set; }

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Unknown_10h = reader.ReadUInt32();
            this.Unknown_14h = reader.ReadUInt32();
            this.BoundPointer = reader.ReadUInt64();
            this.Unknown_20h = reader.ReadUInt32();
            this.Unknown_24h = reader.ReadUInt32();
            this.Unknown_28h = reader.ReadUInt32();
            this.Unknown_2Ch = reader.ReadUInt32();
            this.Unknown_30h = reader.ReadUInt32();
            this.Unknown_34h = reader.ReadUInt32();
            this.Unknown_38h = reader.ReadUInt32();
            this.Unknown_3Ch = reader.ReadUInt32();
            this.Unknown_40h = reader.ReadUInt32();
            this.Unknown_44h = reader.ReadUInt32();
            this.Unknown_48h = reader.ReadUInt32();
            this.Unknown_4Ch = reader.ReadUInt32();
            this.Unknown_50h = reader.ReadUInt32();
            this.Unknown_54h = reader.ReadUInt32();
            this.Unknown_58h = reader.ReadUInt32();
            this.Unknown_5Ch = reader.ReadUInt32();
            this.Unknown_60h = reader.ReadUInt32();
            this.Unknown_64h = reader.ReadUInt32();
            this.Unknown_68h = reader.ReadUInt32();
            this.Unknown_6Ch = reader.ReadUInt32();
            this.Unknown_70h = reader.ReadBlock<SimpleList64<Vector4>>();
            this.Unknown_80h = reader.ReadBlock<SimpleList64<Vector4>>();
            this.Unknown_90h = reader.ReadUInt32();
            this.Unknown_94h = reader.ReadUInt32();
            this.Unknown_98h = reader.ReadUInt32();
            this.Unknown_9Ch = reader.ReadUInt32();
            this.Unknown_A0h = reader.ReadUInt32();
            this.Unknown_A4h = reader.ReadUInt32();
            this.Unknown_A8h = reader.ReadUInt32();
            this.Unknown_ACh = reader.ReadUInt32();
            this.Unknown_B0h = reader.ReadUInt32();
            this.Unknown_B4h = reader.ReadUInt32();
            this.Unknown_B8h = reader.ReadUInt32();
            this.Unknown_BCh = reader.ReadUInt32();
            this.Unknown_C0h = reader.ReadUInt32();
            this.Unknown_C4h = reader.ReadUInt32();
            this.Unknown_C8h = reader.ReadUInt32();
            this.Unknown_CCh = reader.ReadUInt32();
            this.Unknown_D0h = reader.ReadUInt32();
            this.Unknown_D4h = reader.ReadUInt32();
            this.Unknown_D8h = reader.ReadUInt32();
            this.Unknown_DCh = reader.ReadUInt32();
            this.Unknown_E0h = reader.ReadUInt32();
            this.Unknown_E4h = reader.ReadUInt32();
            this.Unknown_E8h = reader.ReadUInt32();
            this.NumEdges = reader.ReadUInt32();
            this.Unknown_F0h = reader.ReadUInt32();
            this.Unknown_F4h = reader.ReadUInt32();
            this.Unknown_F8h = reader.ReadUInt32();
            this.Unknown_FCh = reader.ReadUInt32();
            this.CustomEdgeData = reader.ReadBlock<SimpleList64<EdgeData>>();
            this.EdgeData = reader.ReadBlock<SimpleList64<EdgeData>>();
            this.Unknown_120h = reader.ReadUInt32();
            this.Unknown_124h = reader.ReadUInt32();
            this.Unknown_128h = reader.ReadUInt32();
            this.Unknown_12Ch = reader.ReadUInt32();
            this.BehaviorPointer = reader.ReadUInt64();
            this.Unknown_138h = reader.ReadUInt32();
            this.Unknown_13Ch = reader.ReadUInt32();
            this.Unknown_140h_Pointer = reader.ReadUInt64();
            this.Unknown_148h = reader.ReadUInt32();
            this.Unknown_14Ch = reader.ReadUInt32();
            this.Unknown_150h = reader.ReadUInt32();
            this.Unknown_154h = reader.ReadUInt32();
            this.Unknown_158h = reader.ReadUInt32();
            this.Unknown_15Ch = reader.ReadUInt32();
            this.Unknown_160h = reader.ReadUInt32();
            this.Unknown_164h = reader.ReadUInt32();
            this.Unknown_168h = reader.ReadUInt32();
            this.Unknown_16Ch = reader.ReadUInt32();
            this.Unknown_170h = reader.ReadUInt32();
            this.Unknown_174h = reader.ReadUInt32();
            this.Unknown_178h = reader.ReadUInt32();
            this.Unknown_17Ch = reader.ReadUInt32();

            // read reference data
            this.Bound = reader.ReadBlockAt<Bound>(
              this.BoundPointer // offset
            );
            this.Behavior = reader.ReadBlockAt<EnvClothVerletBehavior>(
                this.BehaviorPointer // offset
            );
            this.Unknown_140h_Data = reader.ReadBlockAt<Unknown_C_007>(
              this.Unknown_140h_Pointer // offset
          );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.BoundPointer = (ulong)(this.Bound?.BlockPosition ?? 0);
            this.BehaviorPointer = (ulong)(this.Behavior?.BlockPosition ?? 0);
            this.Unknown_140h_Pointer = (ulong)(this.Unknown_140h_Data?.BlockPosition ?? 0);

            // write structure data
            writer.Write(this.Unknown_10h);
            writer.Write(this.Unknown_14h);
            writer.Write(this.BoundPointer);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_24h);
            writer.Write(this.Unknown_28h);
            writer.Write(this.Unknown_2Ch);
            writer.Write(this.Unknown_30h);
            writer.Write(this.Unknown_34h);
            writer.Write(this.Unknown_38h);
            writer.Write(this.Unknown_3Ch);
            writer.Write(this.Unknown_40h);
            writer.Write(this.Unknown_44h);
            writer.Write(this.Unknown_48h);
            writer.Write(this.Unknown_4Ch);
            writer.Write(this.Unknown_50h);
            writer.Write(this.Unknown_54h);
            writer.Write(this.Unknown_58h);
            writer.Write(this.Unknown_5Ch);
            writer.Write(this.Unknown_60h);
            writer.Write(this.Unknown_64h);
            writer.Write(this.Unknown_68h);
            writer.Write(this.Unknown_6Ch);
            writer.WriteBlock(this.Unknown_70h);
            writer.WriteBlock(this.Unknown_80h);
            writer.Write(this.Unknown_90h);
            writer.Write(this.Unknown_94h);
            writer.Write(this.Unknown_98h);
            writer.Write(this.Unknown_9Ch);
            writer.Write(this.Unknown_A0h);
            writer.Write(this.Unknown_A4h);
            writer.Write(this.Unknown_A8h);
            writer.Write(this.Unknown_ACh);
            writer.Write(this.Unknown_B0h);
            writer.Write(this.Unknown_B4h);
            writer.Write(this.Unknown_B8h);
            writer.Write(this.Unknown_BCh);
            writer.Write(this.Unknown_C0h);
            writer.Write(this.Unknown_C4h);
            writer.Write(this.Unknown_C8h);
            writer.Write(this.Unknown_CCh);
            writer.Write(this.Unknown_D0h);
            writer.Write(this.Unknown_D4h);
            writer.Write(this.Unknown_D8h);
            writer.Write(this.Unknown_DCh);
            writer.Write(this.Unknown_E0h);
            writer.Write(this.Unknown_E4h);
            writer.Write(this.Unknown_E8h);
            writer.Write(this.NumEdges);
            writer.Write(this.Unknown_F0h);
            writer.Write(this.Unknown_F4h);
            writer.Write(this.Unknown_F8h);
            writer.Write(this.Unknown_FCh);
            writer.WriteBlock(this.CustomEdgeData);
            writer.WriteBlock(this.EdgeData);
            writer.Write(this.Unknown_120h);
            writer.Write(this.Unknown_124h);
            writer.Write(this.Unknown_128h);
            writer.Write(this.Unknown_12Ch);
            writer.Write(this.BehaviorPointer);
            writer.Write(this.Unknown_138h);
            writer.Write(this.Unknown_13Ch);
            writer.Write(this.Unknown_140h_Pointer);
            writer.Write(this.Unknown_148h);
            writer.Write(this.Unknown_14Ch);
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

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Bound != null) list.Add(Bound);
            if (Behavior != null) list.Add(Behavior);
            if (Unknown_140h_Data != null) list.Add(Unknown_140h_Data);
            return list.ToArray();
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x70, Unknown_70h),
                new Tuple<long, IResourceBlock>(0x80, Unknown_80h),
                new Tuple<long, IResourceBlock>(0x100, CustomEdgeData),
                new Tuple<long, IResourceBlock>(0x110, EdgeData)
            };
        }
    }
}
