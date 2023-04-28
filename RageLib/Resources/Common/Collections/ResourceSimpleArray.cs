// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Collections.Generic;

namespace RageLib.Resources.Common.Collections
{
    /// <summary>
    /// Represents an array of type T.
    /// </summary>
    public class ResourceSimpleArray<T> : ListBase<T> where T : IResourceSystemBlock, new()
    {
        /// <summary>
        /// Gets the length of the data block.
        /// </summary>
        public override long BlockLength => blockLength;

        public ResourceSimpleArray()
        {
            Data = new List<T>();
        }







        /// <summary>
        /// Reads the data block.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            int numElements = Convert.ToInt32(parameters[0]);

            Data = new List<T>(numElements);
            for (int i = 0; i < numElements; i++)
            {
                T item = reader.ReadBlock<T>();
                Add(item);
            }
        }

        /// <summary>
        /// Writes the data block.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            foreach (var f in Data)
                f.Write(writer);
        }




        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            var list = new List<Tuple<long, IResourceBlock>>(Data.Count);

            long length = 0;
            foreach (var x in Data)
            {
                list.Add(new Tuple<long, IResourceBlock>(length, x));
                length += x.BlockLength;
            }


            return list.ToArray();
        }





    }
}