// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.Resources.Common;

public struct ArrayInfo64 : IResourceStruct<ArrayInfo64>
{
    public ulong EntriesPointer;
    public ushort EntriesCount;
    public ushort EntriesCapacity;
    private uint Pad;

    public ArrayInfo64 ReverseEndianness()
    {
        return new ArrayInfo64()
        {
            EntriesPointer = EndiannessExtensions.ReverseEndianness(EntriesPointer),
            EntriesCount = EndiannessExtensions.ReverseEndianness(EntriesCount),
            EntriesCapacity = EndiannessExtensions.ReverseEndianness(EntriesCapacity),
        };
    }
}
