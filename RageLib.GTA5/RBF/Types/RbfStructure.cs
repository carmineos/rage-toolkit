// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.GTA5.RBF.Types;
using System.Collections.Generic;

namespace RageLib.GTA5.RBF
{
    public class RbfStructure : IRbfType
    {
        public string Name { get; set; }
        public List<IRbfType> Children = new List<IRbfType>();
    }
}
