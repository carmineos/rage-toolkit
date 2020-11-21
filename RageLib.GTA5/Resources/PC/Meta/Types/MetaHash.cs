using System;
using System.Runtime.CompilerServices;

namespace RageLib.Resources.GTA5.PC.Meta.Types
{
    public readonly struct MetaHash : IEquatable<MetaHash>
    {
        private readonly uint value;

        public MetaHash(uint hash)
        {
            value = hash;
        }

        public bool Equals(MetaHash other) => this == other;

        public override bool Equals(object obj) => obj is MetaHash other && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(MetaHash left, MetaHash right) => left.value == right.value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(MetaHash left, MetaHash right) => left.value != right.value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator uint(MetaHash hash) => hash.value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator MetaHash(uint hash) => new MetaHash(hash);

        public override int GetHashCode() => (int)value;
    }
}
