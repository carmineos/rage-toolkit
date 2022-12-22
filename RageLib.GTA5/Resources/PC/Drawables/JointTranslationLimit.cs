// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    public class JointTranslationLimit : ResourceSystemBlock
    {
        public override long BlockLength => 0x40;

        // structure data
        public ulong Unknown_0h; // 0x0000000000000000
        public uint BoneId;
        public uint Unknown_Ch; // 0x00000000
        public ulong Unknown_10h; // 0x0000000000000000
        public ulong Unknown_18h; // 0x0000000000000000
        public Vector3 LimitMin;
        public uint Unknown_2Ch; // 0x00000000
        public Vector3 LimitMax;
        public uint Unknown_3Ch; // 0x00000000

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.Unknown_0h = reader.ReadUInt64();
            this.BoneId = reader.ReadUInt32();
            this.Unknown_Ch = reader.ReadUInt32();
            this.Unknown_10h = reader.ReadUInt64();
            this.Unknown_18h = reader.ReadUInt64();
            this.LimitMin = reader.ReadVector3();
            this.Unknown_2Ch = reader.ReadUInt32();
            this.LimitMax = reader.ReadVector3();
            this.Unknown_3Ch = reader.ReadUInt32();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // write structure data
            writer.Write(this.Unknown_0h);
            writer.Write(this.BoneId);
            writer.Write(this.Unknown_Ch);
            writer.Write(this.Unknown_10h);
            writer.Write(this.Unknown_18h);
            writer.Write(this.LimitMin);
            writer.Write(this.Unknown_2Ch);
            writer.Write(this.LimitMax);
            writer.Write(this.Unknown_3Ch);
        }
    }
}
