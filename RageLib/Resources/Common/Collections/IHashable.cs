// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Numerics;

namespace RageLib.Resources.Common
{
    public interface IHashable
    {
        public JoaatHash GetJoaatHash();
    }
}
