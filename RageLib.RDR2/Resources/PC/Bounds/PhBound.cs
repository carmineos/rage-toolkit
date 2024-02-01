// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Numerics;

namespace RageLib.Resources.RDR2.PC.Bounds
{
    // phBoundBase
    // phBound
    public class PhBound : PgBase64, IResourceXXSystemBlock
    {
        public override long BlockLength => 0x70;

        // structure data
        public byte Type;
        public byte Unknown_11h;
        public ushort Unknown_12h;
        public float RadiusAroundCentroid;
        public uint Unknown_18h;
        public uint Unknown_1Ch;
        public Vector3 BoundingBoxMax;
        public float Margin;
        public Vector3 BoundingBoxMin;
        public uint RefCount;
        public Vector3 CentroidOffset;
        public uint MaterialId0;
        public Vector3 CGOffset;
        public uint MaterialId1;
        public Vector4 VolumeDistribution;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            Type = reader.ReadByte();
            Unknown_11h = reader.ReadByte();
            Unknown_12h = reader.ReadUInt16();
            RadiusAroundCentroid = reader.ReadSingle();
            Unknown_18h = reader.ReadUInt32();
            Unknown_1Ch = reader.ReadUInt32();
            BoundingBoxMax = reader.ReadVector3();
            Margin = reader.ReadSingle();
            BoundingBoxMin = reader.ReadVector3();
            RefCount = reader.ReadUInt32();
            CentroidOffset = reader.ReadVector3();
            MaterialId0 = reader.ReadUInt32();
            CGOffset = reader.ReadVector3();
            MaterialId1 = reader.ReadUInt32();
            VolumeDistribution = reader.ReadVector4();
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(Type);
            writer.Write(Unknown_11h);
            writer.Write(Unknown_12h);
            writer.Write(RadiusAroundCentroid);
            writer.Write(Unknown_18h);
            writer.Write(Unknown_1Ch);
            writer.Write(BoundingBoxMax);
            writer.Write(Margin);
            writer.Write(BoundingBoxMin);
            writer.Write(RefCount);
            writer.Write(CentroidOffset);
            writer.Write(MaterialId0);
            writer.Write(CGOffset);
            writer.Write(MaterialId1);
            writer.Write(VolumeDistribution);
        }

        public IResourceSystemBlock GetType(ResourceDataReader reader, params object[] parameters)
        {
            reader.Position += 16;
            var type = reader.ReadByte();
            reader.Position -= 17;

            switch (type)
            {
                case 6: return new PhBoundComposite();
                default: throw new Exception("Unknown bound type");
            }
        }
    }
}
