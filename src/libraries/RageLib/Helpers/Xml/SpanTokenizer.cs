// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace RageLib.Helpers.Xml
{
    // This is a custom version of: https://github.com/windows-toolkit/WindowsCommunityToolkit/blob/master/Microsoft.Toolkit.HighPerformance/Enumerables/SpanTokenizer%7BT%7D.cs

    /// <summary>
    /// A <see langword="ref"/> <see langword="struct"/> that tokenizes a given <see cref="Span{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of items to enumerate.</typeparam>
    public ref struct SpanTokenizer
    {
        /// <summary>
        /// The source <see cref="Span{T}"/> instance.
        /// </summary>
        private readonly ReadOnlySpan<char> span;

        /// <summary>
        /// The current initial offset.
        /// </summary>
        private int start;

        /// <summary>
        /// The current final offset.
        /// </summary>
        private int end;

        /// <summary>
        /// The number of chars of the current chunk.
        /// </summary>
        private int count;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpanTokenizer{T}"/> struct.
        /// </summary>
        /// <param name="span">The source <see cref="Span{T}"/> instance.</param>
        /// <param name="separator">The separator item to use.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SpanTokenizer(ReadOnlySpan<char> span)
        {
            this.span = span;
            this.start = 0;
            this.end = 0;
            this.count = 0;
        }

        /// <summary>
        /// Implements the duck-typed <see cref="IEnumerable{T}.GetEnumerator"/> method.
        /// </summary>
        /// <returns>An <see cref="SpanTokenizer{T}"/> instance targeting the current <see cref="Span{T}"/> value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly SpanTokenizer GetEnumerator() => this;

        /// <summary>
        /// Implements the duck-typed <see cref="System.Collections.IEnumerator.MoveNext"/> method.
        /// </summary>
        /// <returns><see langword="true"/> whether a new element is available, <see langword="false"/> otherwise</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            while (end < span.Length)
            {
                var c = span[end];

                if (char.IsWhiteSpace(c))
                {
                    if (count > 0)
                    {
                        count = 0;
                        return true;
                    }

                    start = end + 1;
                }
                else
                {
                    count++;
                }

                end++;
            }

            if (count > 0)
            {
                count = 0;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the duck-typed <see cref="IEnumerator{T}.Current"/> property.
        /// </summary>
        public readonly ReadOnlySpan<char> Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.span.Slice(this.start, this.end - this.start);
        }
    }
}
