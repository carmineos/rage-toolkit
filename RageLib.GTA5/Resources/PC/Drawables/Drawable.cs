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

using RageLib.Resources.Common;
using System;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    // rmcDrawable
    public class Drawable : DrawableBase
    {
        public override long BlockLength => 0xA8;

        // structure data
        public Ref<SkeletonData> Skeleton;
        public LodGroup LodGroup;
        public Ref<Joints> Joints;
        public ushort Unknown_98h;
        public ushort Unknown_9Ah;
        public uint Unknown_9Ch; // 0x00000000
        public Ref<ResourcePointerList64<DrawableModel>> PrimaryDrawableModels;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.Skeleton = reader.ReadUInt64();
            this.LodGroup = reader.ReadBlock<LodGroup>();
            this.Joints = reader.ReadUInt64();
            this.Unknown_98h = reader.ReadUInt16();
            this.Unknown_9Ah = reader.ReadUInt16();
            this.Unknown_9Ch = reader.ReadUInt32();
            this.PrimaryDrawableModels = reader.ReadUInt64();

            // read reference data
            this.Skeleton.ReadBlock(reader);
            this.Joints.ReadBlock(reader);
            this.PrimaryDrawableModels.ReadBlock(reader);
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // write structure data
            writer.Write(this.Skeleton);
            writer.WriteBlock(this.LodGroup);
            writer.Write(this.Joints);
            writer.Write(this.Unknown_98h);
            writer.Write(this.Unknown_9Ah);
            writer.Write(this.Unknown_9Ch);
            writer.Write(this.PrimaryDrawableModels);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Skeleton.Data != null) list.Add(Skeleton.Data);
            if (Joints.Data != null) list.Add(Joints.Data);
            if (PrimaryDrawableModels.Data != null) list.Add(PrimaryDrawableModels.Data);
            return list.ToArray();
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            return new Tuple<long, IResourceBlock>[] {
                new Tuple<long, IResourceBlock>(0x20, LodGroup)
            };
        }
    }
}
