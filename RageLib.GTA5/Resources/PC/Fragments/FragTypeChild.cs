/*
    Copyright(c) 2017 Neodymium

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Fragments
{
    // datBase
    // fragTypeChild
    public class FragTypeChild : DatBase64
    {
        public override long BlockLength => 0x100;

        // structure data
        public float PristineMass;
        public float DamagedMass;
        public ushort GroupIndex;
        public ushort BoneId;
        public uint Unknown_14h; // 0x00000000
        public ulong Unknown_18h; // 0x0000000000000000
        public ulong Unknown_20h; // 0x0000000000000000
        public ulong Unknown_28h; // 0x0000000000000000
        public ulong Unknown_30h; // 0x0000000000000000
        public ulong Unknown_38h; // 0x0000000000000000
        public ulong Unknown_40h; // 0x0000000000000000
        public ulong Unknown_48h; // 0x0000000000000000
        public ulong Unknown_50h; // 0x0000000000000000
        public ulong Unknown_58h; // 0x0000000000000000
        public ulong Unknown_60h; // 0x0000000000000000
        public ulong Unknown_68h; // 0x0000000000000000
        public ulong Unknown_70h; // 0x0000000000000000
        public ulong Unknown_78h; // 0x0000000000000000
        public ulong Unknown_80h; // 0x0000000000000000
        public ulong Unknown_88h; // 0x0000000000000000
        public ulong Unknown_90h; // 0x0000000000000000
        public ulong Unknown_98h; // 0x0000000000000000
        public ulong Drawable1Pointer;
        public ulong Drawable2Pointer;
        public ulong EvtSetPointer;
        public ulong Unknown_B8h; // 0x0000000000000000
        public ulong Unknown_C0h; // 0x0000000000000000
        public ulong Unknown_C8h; // 0x0000000000000000
        public ulong Unknown_D0h; // 0x0000000000000000
        public ulong Unknown_D8h; // 0x0000000000000000
        public ulong Unknown_E0h; // 0x0000000000000000
        public ulong Unknown_E8h; // 0x0000000000000000
        public ulong Unknown_F0h; // 0x0000000000000000
        public ulong Unknown_F8h; // 0x0000000000000000

        // reference data
        public FragDrawable Drawable1;
        public FragDrawable Drawable2;
        public EvtSet EvtSet;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.PristineMass = reader.ReadSingle();
            this.DamagedMass = reader.ReadSingle();
            this.GroupIndex = reader.ReadUInt16();
            this.BoneId = reader.ReadUInt16();
            this.Unknown_14h = reader.ReadUInt32();
            this.Unknown_18h = reader.ReadUInt64();
            this.Unknown_20h = reader.ReadUInt64();
            this.Unknown_28h = reader.ReadUInt64();
            this.Unknown_30h = reader.ReadUInt64();
            this.Unknown_38h = reader.ReadUInt64();
            this.Unknown_40h = reader.ReadUInt64();
            this.Unknown_48h = reader.ReadUInt64();
            this.Unknown_50h = reader.ReadUInt64();
            this.Unknown_58h = reader.ReadUInt64();
            this.Unknown_60h = reader.ReadUInt64();
            this.Unknown_68h = reader.ReadUInt64();
            this.Unknown_70h = reader.ReadUInt64();
            this.Unknown_78h = reader.ReadUInt64();
            this.Unknown_80h = reader.ReadUInt64();
            this.Unknown_88h = reader.ReadUInt64();
            this.Unknown_90h = reader.ReadUInt64();
            this.Unknown_98h = reader.ReadUInt64();
            this.Drawable1Pointer = reader.ReadUInt64();
            this.Drawable2Pointer = reader.ReadUInt64();
            this.EvtSetPointer = reader.ReadUInt64();
            this.Unknown_B8h = reader.ReadUInt64();
            this.Unknown_C0h = reader.ReadUInt64();
            this.Unknown_C8h = reader.ReadUInt64();
            this.Unknown_D0h = reader.ReadUInt64();
            this.Unknown_D8h = reader.ReadUInt64();
            this.Unknown_E0h = reader.ReadUInt64();
            this.Unknown_E8h = reader.ReadUInt64();
            this.Unknown_F0h = reader.ReadUInt64();
            this.Unknown_F8h = reader.ReadUInt64();

            // read reference data
            this.Drawable1 = reader.ReadBlockAt<FragDrawable>(
                this.Drawable1Pointer // offset
            );
            this.Drawable2 = reader.ReadBlockAt<FragDrawable>(
                this.Drawable2Pointer // offset
            );
            this.EvtSet = reader.ReadBlockAt<EvtSet>(
                this.EvtSetPointer // offset
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.Drawable1Pointer = (ulong)(this.Drawable1 != null ? this.Drawable1.BlockPosition : 0);
            this.Drawable2Pointer = (ulong)(this.Drawable2 != null ? this.Drawable2.BlockPosition : 0);
            this.EvtSetPointer = (ulong)(this.EvtSet != null ? this.EvtSet.BlockPosition : 0);

            // write structure data
            writer.Write(this.PristineMass);
            writer.Write(this.DamagedMass);
            writer.Write(this.GroupIndex);
            writer.Write(this.BoneId);
            writer.Write(this.Unknown_14h);
            writer.Write(this.Unknown_18h);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_28h);
            writer.Write(this.Unknown_30h);
            writer.Write(this.Unknown_38h);
            writer.Write(this.Unknown_40h);
            writer.Write(this.Unknown_48h);
            writer.Write(this.Unknown_50h);
            writer.Write(this.Unknown_58h);
            writer.Write(this.Unknown_60h);
            writer.Write(this.Unknown_68h);
            writer.Write(this.Unknown_70h);
            writer.Write(this.Unknown_78h);
            writer.Write(this.Unknown_80h);
            writer.Write(this.Unknown_88h);
            writer.Write(this.Unknown_90h);
            writer.Write(this.Unknown_98h);
            writer.Write(this.Drawable1Pointer);
            writer.Write(this.Drawable2Pointer);
            writer.Write(this.EvtSetPointer);
            writer.Write(this.Unknown_B8h);
            writer.Write(this.Unknown_C0h);
            writer.Write(this.Unknown_C8h);
            writer.Write(this.Unknown_D0h);
            writer.Write(this.Unknown_D8h);
            writer.Write(this.Unknown_E0h);
            writer.Write(this.Unknown_E8h);
            writer.Write(this.Unknown_F0h);
            writer.Write(this.Unknown_F8h);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (Drawable1 != null) list.Add(Drawable1);
            if (Drawable2 != null) list.Add(Drawable2);
            if (EvtSet != null) list.Add(EvtSet);
            return list.ToArray();
        }
    }
}
