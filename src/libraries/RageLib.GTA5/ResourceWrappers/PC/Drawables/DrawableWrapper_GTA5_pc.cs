// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using RageLib.Resources.GTA5.PC.Drawables;
using RageLib.ResourceWrappers.Drawables;

namespace RageLib.GTA5.ResourceWrappers.PC.Drawables
{
    public class DrawableListWrapper_GTA5_pc : IDrawableList
    {
        private IList<GtaDrawable> list;

        public DrawableListWrapper_GTA5_pc(IList<GtaDrawable> list)
        {
            this.list = list;
        }


        public IDrawable this[int index]
        {
            get
            {
                return new DrawableWrapper_GTA5_pc(list[index]);
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Add(IDrawable item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(IDrawable item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(IDrawable[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IDrawable> GetEnumerator()
        {
            foreach (var x in list)
                yield return new DrawableWrapper_GTA5_pc(x);
        }

        public int IndexOf(IDrawable item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, IDrawable item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IDrawable item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class DrawableWrapper_GTA5_pc : IDrawable
    {
        private GtaDrawable drawable;

        public IShaderGroup ShaderGroup
        {
            get
            {
                if (drawable.ShaderGroup != null)
                    return new ShaderGroupWrapper_GTA5_pc(drawable.ShaderGroup);
                else
                    return null;
            }
        }

        public string Name
        {
            get
            {
                return (string)drawable.Name;
            }
        }

        public DrawableWrapper_GTA5_pc(GtaDrawable drawable)
        {
            this.drawable = drawable;
        }
    }
}
