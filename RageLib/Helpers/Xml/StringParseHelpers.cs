// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;

namespace RageLib.Helpers.Xml
{
    public static class StringParseHelpers
    {
        public static List<T> ParseItems<T>(ReadOnlySpan<char> span, IFormatProvider? formatProvider = null) where T : ISpanParsable<T>
        {
            formatProvider ??= NumberFormatInfo.InvariantInfo;

            List<T> items = new List<T>();
            int start = 0;
            int count = 0;

            for (int i = 0; i < span.Length; i++)
            {
                var c = span[i];

                if (char.IsWhiteSpace(c))
                {
                    if (count > 0)
                    {
                        var item = span.Slice(start, count);
                        items.Add(T.Parse(item, provider: formatProvider));
                        count = 0;
                    }

                    start = i + 1;
                }
                else
                {
                    count++;
                }
            }

            // Collect last item
            if (count > 0)
            {
                var item = span.Slice(start, count);
                items.Add(T.Parse(item, provider: formatProvider));
            }

            return items;
        }
    }
}
