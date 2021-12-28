// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Numerics;
using RageLib.Resources.Common;
using System.Collections.Generic;
using System.Numerics;

namespace RageLib.Resources.GTA4.PC.Bounds
{
    public class PhBoundComposite : PhBound 
    {
        public override long BlockLength => 0x90;

        // structure data
        private uint BoundsPointer;
        private uint CurrentMatricesPointer;
        private uint LastMatricesPointer;
        private uint ChildBoundingBoxesPointer;
        public ushort MaxNumBounds;
        public ushort NumBounds;
        private uint Unknown_84h; // 0x00000000
        private uint Unknown_88h; // 0x00000000
        private uint Unknown_8Ch; // 0x00000000

        // reference data
        public ResourcePointerArray32<PhBound>? Bounds { get; set; }
        public SimpleArray<Matrix4x4>? CurrentMatrices { get; set; }
        public SimpleArray<Matrix4x4>? LastMatrices { get; set; }
        public SimpleArray<Aabb>? ChildBoundingBoxes { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            BoundsPointer = reader.ReadUInt32();
            CurrentMatricesPointer = reader.ReadUInt32();
            LastMatricesPointer = reader.ReadUInt32();
            ChildBoundingBoxesPointer = reader.ReadUInt32();
            MaxNumBounds = reader.ReadUInt16();
            NumBounds = reader.ReadUInt16();
            Unknown_84h = reader.ReadUInt32();
            Unknown_88h = reader.ReadUInt32();
            Unknown_8Ch = reader.ReadUInt32();

            // read reference data
            Bounds = reader.ReadBlockAt<ResourcePointerArray32<PhBound>>(BoundsPointer, MaxNumBounds);
            CurrentMatrices = reader.ReadBlockAt<SimpleArray<Matrix4x4>>(CurrentMatricesPointer, MaxNumBounds);
            LastMatrices = reader.ReadBlockAt<SimpleArray<Matrix4x4>>(LastMatricesPointer, MaxNumBounds);
            ChildBoundingBoxes = reader.ReadBlockAt<SimpleArray<Aabb>>(ChildBoundingBoxesPointer, MaxNumBounds);
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            BoundsPointer = (uint)(Bounds?.BlockPosition ?? 0);
            CurrentMatricesPointer = (uint)(CurrentMatrices?.BlockPosition ?? 0);
            LastMatricesPointer = (uint)(LastMatrices?.BlockPosition ?? 0);
            ChildBoundingBoxesPointer = (uint)(ChildBoundingBoxes?.BlockPosition ?? 0);
            MaxNumBounds = (ushort)(Bounds?.Count ?? 0);
            NumBounds = (ushort)(Bounds?.Count ?? 0);

            // write structure data
            writer.Write(BoundsPointer);
            writer.Write(CurrentMatricesPointer);
            writer.Write(LastMatricesPointer);
            writer.Write(ChildBoundingBoxesPointer);
            writer.Write(MaxNumBounds);
            writer.Write(NumBounds);
            writer.Write(Unknown_84h);
            writer.Write(Unknown_88h);
            writer.Write(Unknown_8Ch);
        }

        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Bounds is not null) list.Add(Bounds);
            if (CurrentMatrices is not null) list.Add(CurrentMatrices);
            if (LastMatrices is not null) list.Add(LastMatrices);
            if (ChildBoundingBoxes is not null) list.Add(ChildBoundingBoxes);
            return list.ToArray();
        }
    }
}
