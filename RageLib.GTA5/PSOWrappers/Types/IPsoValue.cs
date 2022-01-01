// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.GTA5.PSOWrappers.Data;

namespace RageLib.GTA5.PSOWrappers.Types
{
    public interface IPsoValue
    {
        void Read(PsoDataReader reader);
        void Write(DataWriter writer);
    }
}
