﻿// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Collections.Generic;

namespace RageLib.Resources.Common.Collections
{
    /// <summary>
    /// Represents an array of pointers where each points to an object of type T.
    /// </summary>
    public class ResourcePointerArray64<T> : ResourceSystemBlock, IList<T> where T : IResourceSystemBlock, new()
    {

        public int GetNonEmptyNumber()
        {
            int i = 0;
            foreach (var q in data_items)
                if (q != null)
                    i++;
            return i;
        }

        public override long BlockLength
        {
            get { return 8 * data_items.Count; }
        }


        // structure data
        public List<ulong> data_pointers;

        // reference data
        public List<T> data_items;


        public ResourcePointerArray64()
        {
            data_items = new List<T>();
        }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            int numElements = Convert.ToInt32(parameters[0]);

            // read structure data            
            data_pointers = new List<ulong>(numElements);
            for (int i = 0; i < numElements; i++)
            {
                data_pointers.Add(reader.ReadUInt64());
            }

            foreach (var dp in data_pointers)
            {
                if (dp == 0)
                {

                }
            }

            // read reference data
            data_items = new List<T>(numElements);
            for (int i = 0; i < numElements; i++)
            {
                data_items.Add(
                    reader.ReadBlockAt<T>(data_pointers[i])
                    );
            }
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update...
            data_pointers = new List<ulong>(data_items.Count);
            foreach (var x in data_items)
                if (x != null)
                    data_pointers.Add((uint)x.BlockPosition);
                else
                    data_pointers.Add((uint)0);

            // write...
            foreach (var x in data_pointers)
                writer.Write(x);
        }


        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(data_items.Count);

            foreach (var x in data_items)
                list.Add(x);

            return list.ToArray();          
        }





        public int IndexOf(T item)
        {
            return data_items.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            data_items.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            data_items.RemoveAt(index);
        }

        public T this[int index]
        {
            get
            {
                return data_items[index];
            }
            set
            {
                data_items[index] = value;
            }
        }

        public void Add(T item)
        {
            data_items.Add(item);
        }

        public void Clear()
        {
            data_items.Clear();
        }

        public bool Contains(T item)
        {
            return data_items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            data_items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return data_items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            return data_items.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return data_items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return data_items.GetEnumerator();
        }
    }
}