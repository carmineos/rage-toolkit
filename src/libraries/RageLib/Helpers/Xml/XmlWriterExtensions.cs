﻿// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Xml;

namespace RageLib.Helpers.Xml
{
    public static class XmlWriterExtensions
    {
        private const string floatFormat = "0.000000";

        public static void WriteAttributeValue(this XmlWriter writer, bool value)
        {
            writer.WriteAttributeString("value", value ? "true" : "false");
        }

        public static void WriteAttributeValue(this XmlWriter writer, byte value)
        {
            writer.WriteAttributeString("value", value.ToString(NumberFormatInfo.InvariantInfo));
        }

        public static void WriteAttributeValue(this XmlWriter writer, sbyte value)
        {
            writer.WriteAttributeString("value", value.ToString(NumberFormatInfo.InvariantInfo));
        }

        public static void WriteAttributeValue(this XmlWriter writer, short value)
        {
            writer.WriteAttributeString("value", value.ToString(NumberFormatInfo.InvariantInfo));
        }

        public static void WriteAttributeValue(this XmlWriter writer, ushort value)
        {
            writer.WriteAttributeString("value", value.ToString(NumberFormatInfo.InvariantInfo));
        }

        public static void WriteAttributeValue(this XmlWriter writer, int value)
        {
            writer.WriteAttributeString("value", value.ToString(NumberFormatInfo.InvariantInfo));
        }

        public static void WriteAttributeValue(this XmlWriter writer, uint value)
        {
            writer.WriteAttributeString("value", value.ToString(NumberFormatInfo.InvariantInfo));
        }

        public static void WriteAttributeValue(this XmlWriter writer, Half value)
        {
            writer.WriteAttributeString("value", value.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
        }

        public static void WriteAttributeValue(this XmlWriter writer, float value)
        {
            writer.WriteAttributeString("value", value.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
        }

        public static void WriteAttributeValue(this XmlWriter writer, double value)
        {
            writer.WriteAttributeString("value", value.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
        }

        public static void WriteAttributesXY(this XmlWriter writer, Vector2 value)
        {
            writer.WriteAttributeString("x", value.X.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
            writer.WriteAttributeString("y", value.Y.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
        }

        public static void WriteAttributesXYZ(this XmlWriter writer, Vector3 value)
        {
            writer.WriteAttributeString("x", value.X.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
            writer.WriteAttributeString("y", value.Y.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
            writer.WriteAttributeString("z", value.Z.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
        }

        public static void WriteAttributesXYZW(this XmlWriter writer, Vector4 value)
        {
            writer.WriteAttributeString("x", value.X.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
            writer.WriteAttributeString("y", value.Y.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
            writer.WriteAttributeString("z", value.Z.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
            writer.WriteAttributeString("w", value.W.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
        }

        public static void WriteAttributesXYZW(this XmlWriter writer, Quaternion value)
        {
            writer.WriteAttributeString("x", value.X.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
            writer.WriteAttributeString("y", value.Y.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
            writer.WriteAttributeString("z", value.Z.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
            writer.WriteAttributeString("w", value.W.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
        }

        public static void WriteAttributesXY(this XmlWriter writer, float x, float y)
        {
            writer.WriteAttributeString("x", x.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
            writer.WriteAttributeString("y", y.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
        }

        public static void WriteAttributesXYZ(this XmlWriter writer, float x, float y, float z)
        {
            writer.WriteAttributeString("x", x.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
            writer.WriteAttributeString("y", y.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
            writer.WriteAttributeString("z", z.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
        }

        public static void WriteAttributesXYZW(this XmlWriter writer, float x, float y, float z, float w)
        {
            writer.WriteAttributeString("x", x.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
            writer.WriteAttributeString("y", y.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
            writer.WriteAttributeString("z", z.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
            writer.WriteAttributeString("w", w.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
        }

        public static void WriteAttributeValueAsHex(this XmlWriter writer, uint value)
        {
            writer.WriteAttributeString("value", $"0x{value:X8}");
        }

        public static void WriteInlineArrayContent(this XmlRageWriter writer, IEnumerable<byte> array)
        {
            writer.WriteAttributeString("content", "char_array");

            writer.WriteString(Environment.NewLine);
            foreach (var value in array)
            {
                writer.WriteInnerIndent();
                writer.WriteString(value.ToString(NumberFormatInfo.InvariantInfo));
                writer.WriteString(Environment.NewLine);
            }
            writer.WriteOuterIndent();
        }

        public static void WriteInlineArrayContent(this XmlRageWriter writer, IEnumerable<ushort> array)
        {
            writer.WriteAttributeString("content", "short_array");

            writer.WriteString(Environment.NewLine);
            foreach (var value in array)
            {
                writer.WriteInnerIndent();
                writer.WriteString(value.ToString(NumberFormatInfo.InvariantInfo));
                writer.WriteString(Environment.NewLine);
            }
            writer.WriteOuterIndent();
        }

        public static void WriteInlineArrayContent(this XmlRageWriter writer, IEnumerable<uint> array)
        {
            writer.WriteAttributeString("content", "int_array");

            writer.WriteString(Environment.NewLine);
            foreach (var value in array)
            {
                writer.WriteInnerIndent();
                writer.WriteString(value.ToString(NumberFormatInfo.InvariantInfo));
                writer.WriteString(Environment.NewLine);
            }
            writer.WriteOuterIndent();
        }

        public static void WriteInlineArrayContent(this XmlRageWriter writer, IEnumerable<float> array)
        {
            writer.WriteAttributeString("content", "float_array");

            writer.WriteString(Environment.NewLine);
            foreach (var value in array)
            {
                writer.WriteInnerIndent();
                writer.WriteString(value.ToString(floatFormat, NumberFormatInfo.InvariantInfo));
                writer.WriteString(Environment.NewLine);
            }
            writer.WriteOuterIndent();
        }

        public static void WriteInlineArrayContent(this XmlRageWriter writer, IEnumerable<Vector3> array)
        {
            writer.WriteAttributeString("content", "vector3_array");

            writer.WriteString(Environment.NewLine);
            foreach (var value in array)
            {
                writer.WriteInnerIndent();
                writer.WriteString(value.X.ToString($"{floatFormat}\t", NumberFormatInfo.InvariantInfo));
                writer.WriteString(value.Y.ToString($"{floatFormat}\t", NumberFormatInfo.InvariantInfo));
                writer.WriteString(value.Z.ToString($"{floatFormat}\t", NumberFormatInfo.InvariantInfo));
                writer.WriteString(Environment.NewLine);
            }
            writer.WriteOuterIndent();
        }
    }
}
