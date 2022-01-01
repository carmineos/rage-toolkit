// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;

namespace RageLib.Helpers.Xml
{
    // Refactor once https://github.com/dotnet/designs/pull/205 is available
    public static class StringParseHelpers
    {
        public static List<byte> ParseItemsAsUInt8(ReadOnlySpan<char> span)
        {
            List<byte> items = new List<byte>();
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
                        items.Add(byte.Parse(item, provider: NumberFormatInfo.InvariantInfo));
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
                items.Add(byte.Parse(item, provider: NumberFormatInfo.InvariantInfo));
            }

            return items;
        }

        public static List<ushort> ParseItemsAsUInt16(ReadOnlySpan<char> span)
        {
            List<ushort> items = new List<ushort>();
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
                        items.Add(ushort.Parse(item, provider: NumberFormatInfo.InvariantInfo));
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
                items.Add(ushort.Parse(item, provider: NumberFormatInfo.InvariantInfo));
            }

            return items;
        }

        public static List<uint> ParseItemsAsUInt32(ReadOnlySpan<char> span)
        {
            List<uint> items = new List<uint>();
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
                        items.Add(uint.Parse(item, provider: NumberFormatInfo.InvariantInfo));
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
                items.Add(uint.Parse(item, provider: NumberFormatInfo.InvariantInfo));
            }

            return items;
        }

        public static List<float> ParseItemsAsFloat(ReadOnlySpan<char> span)
        {
            List<float> items = new List<float>();
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
                        items.Add(float.Parse(item, provider: NumberFormatInfo.InvariantInfo));
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
                items.Add(float.Parse(item, provider: NumberFormatInfo.InvariantInfo));
            }

            return items;
        }
    }
}
