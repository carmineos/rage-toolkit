// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Types
{
    public interface IMetaValue
    {
        void Read(DataReader reader);
        void Write(DataWriter writer);
    }

    public delegate IMetaValue CreateMetaValueDelegate();
}
