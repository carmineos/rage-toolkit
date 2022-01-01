// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Collections.Generic;

namespace RageLib.Resources.Common
{
    /// <summary>
    /// Represents an array of type T.
    /// </summary>
    public class SimpleArrayArray64<T> : ListBase<SimpleArray<T>> where T : unmanaged
    {
        /// <summary>
        /// Gets the length of the data block.
        /// </summary>
        public override long BlockLength
        {
            get
            {
                long len = 8 * Data.Count;
                foreach (var f in Data)
                    len += f.BlockLength;
                return len;
            }
        }


        public SimpleArrayArray64()
        {
            Data = new List<SimpleArray<T>>();
        }




        public List<ulong> ptr_list;



        /// <summary>
        /// Reads the data block.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {

            var numEl = (SimpleArray<uint>)parameters[1];

            ptr_list = new List<ulong>(numEl.Count);
            for (int i = 0; i < numEl.Count; i++)
                ptr_list.Add(reader.ReadUInt64());

            for (int i = 0; i < numEl.Count; i++)
            {
                var xarr = reader.ReadBlockAt<SimpleArray<T>>(ptr_list[i], numEl[i]);
                Data.Add(xarr);
            }

            //int numElements = Convert.ToInt32(parameters[0]);

                //Data = new List<T>();
                //for (int i = 0; i < numElements; i++)
                //{
                //    T item = reader.ReadBlock<T>();
                //    Data.Add(item);
                //}
        }

        /// <summary>
        /// Writes the data block.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            //foreach (var f in Data)
            //    f.Write(writer);


            ptr_list = new List<ulong>(Data.Count);
            foreach (var x in Data)
                ptr_list.Add((ulong)x.BlockPosition);

            foreach (var x in ptr_list)
                writer.Write(x);
            foreach (var x in Data)
                x.Write(writer);

        }
        
        
        
        public override IResourceBlock[] GetReferences()
        {
            var children = new List<IResourceBlock>();

            //if (Data != null) children.AddRange(Data);

            return children.ToArray();
        }

        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            var children = new List<Tuple<long, IResourceBlock>>(Data != null ? Data.Count : 0);

            if (Data != null)
            {
                long len = 8 * Data.Count;
                foreach (var f in Data)
                {
                    children.Add(new Tuple<long, IResourceBlock>(len, f));
                    len += f.BlockLength;
                }
            }

            return children.ToArray();
        }

    }
}