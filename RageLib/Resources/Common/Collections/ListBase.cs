// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Collections;
using System.Collections.Generic;

namespace RageLib.Resources.Common.Collections
{
    public abstract class ListBase<T> : ResourceSystemBlock, IList<T> where T : IResourceSystemBlock, new()
    {
        protected long blockLength;

        // this is the data...
        public List<T> Data { get; set; }





        public T this[int index]
        {
            get
            {
                return Data[index];
            }
            set
            {
                Insert(index, value);
            }
        }

        public int Count
        {
            get
            {
                return Data.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }






        public ListBase()
        {
            Data = new List<T>();
        }





        public void Add(T item)
        {
            Data.Add(item);
            blockLength += item.BlockLength;
        }

        public void Clear()
        {
            Data.Clear();
            blockLength = 0;
        }

        public bool Contains(T item)
        {
            return Data.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Data.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return Data.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            if (index >= 0 && index < Count)
            {
                RemoveAt(index);
                Data.Insert(index, item);
                blockLength += item.BlockLength;
            }
        }

        public bool Remove(T item)
        {
            if (Data.Remove(item))
            {
                blockLength -= item.BlockLength;
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if(index >= 0 && index < Count)
            {
                var item = Data[index];
                blockLength -= item.BlockLength;
                Data.RemoveAt(index);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Data.GetEnumerator();
        }
    }
}
