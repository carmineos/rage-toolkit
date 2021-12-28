// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Numerics;

namespace RageLib.Resources.GTA4.PC.Bounds
{
    public enum BoundType : byte
    {
        BoundSphere = 0,
        BoundCapsule = 1,
        BoundBox = 3,
        BoundGeometry = 4,
        BoundBVH = 10,
        BoundComposite = 12
    }

    public class PhBound : DatBase32, IResourceXXSystemBlock
    {
        public override long BlockLength => 0x80;

        // structure data
        public BoundType Type;
        public byte Unknown_06h;
        public ushort Unknown_07h;
        public float Radius;
        public float WorldRadius;
        public Vector4 BoundingBoxMax;
        public Vector4 BoundingBoxMin; 
        public Vector4 CentroidOffset;
        public Vector4 Unknown_40h;
        public Vector4 CGOffset;
        public Vector4 Unknown_60h;
        public Vector3 Margin;
        public uint RefCount;

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            Type = (BoundType)reader.ReadByte();
            Unknown_06h = reader.ReadByte();
            Unknown_07h = reader.ReadUInt16();
            Radius = reader.ReadSingle();
            WorldRadius = reader.ReadSingle();
            BoundingBoxMax = reader.ReadVector4();
            BoundingBoxMin = reader.ReadVector4();
            CentroidOffset = reader.ReadVector4();
            Unknown_40h = reader.ReadVector4();
            CGOffset = reader.ReadVector4();
            Unknown_60h = reader.ReadVector4();
            Margin = reader.ReadVector3();
            RefCount = reader.ReadUInt32();
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write((byte)Type);
            writer.Write(Unknown_06h);
            writer.Write(Unknown_07h);
            writer.Write(Radius);
            writer.Write(WorldRadius);
            writer.Write(BoundingBoxMax);
            writer.Write(BoundingBoxMin);
            writer.Write(CentroidOffset);
            writer.Write(Unknown_40h);
            writer.Write(CGOffset);
            writer.Write(Unknown_60h);
            writer.Write(Margin);
            writer.Write(RefCount);
        }

        public IResourceSystemBlock GetType(ResourceDataReader reader, params object[] parameters)
        {
            reader.Position += 4;
            var type = (BoundType)reader.ReadByte();
            reader.Position -= 5;

            return type switch
            {
                BoundType.BoundSphere => new PhBoundSphere(),
                BoundType.BoundCapsule => new PhBoundCapsule(),
                BoundType.BoundBox => new PhBoundBox(),
                BoundType.BoundGeometry => new PhBoundGeometry(),
                BoundType.BoundBVH => new PhBoundBVH(),
                BoundType.BoundComposite => new PhBoundComposite(),
                _ => throw new Exception("Unknown bound type"),
            };
        }
    }
}
