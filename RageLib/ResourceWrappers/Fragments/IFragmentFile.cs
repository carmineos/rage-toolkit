// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.ResourceWrappers.Fragments
{
    public interface IFragmentFile : IResourceFile
    {
        IFragType FragType { get; }
    }
}