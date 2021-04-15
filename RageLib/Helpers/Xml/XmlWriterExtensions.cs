using System;
using System.Globalization;
using System.Numerics;
using System.Xml;

namespace RageLib.Helpers.Xml
{
    public static class XmlWriterExtensions
    {
        private static readonly NumberFormatInfo numberFormatInfo = NumberFormatInfo.InvariantInfo;
        private const string floatFormat = "0.000000";

        public static void WriteAttributeValue(this XmlWriter writer, bool value)
        {
            writer.WriteAttributeString("value", value ? "true" : "false");
        }

        public static void WriteAttributeValue(this XmlWriter writer, byte value)
        {
            writer.WriteAttributeString("value", value.ToString(numberFormatInfo));
        }

        public static void WriteAttributeValue(this XmlWriter writer, sbyte value)
        {
            writer.WriteAttributeString("value", value.ToString(numberFormatInfo));
        }

        public static void WriteAttributeValue(this XmlWriter writer, short value)
        {
            writer.WriteAttributeString("value", value.ToString(numberFormatInfo));
        }

        public static void WriteAttributeValue(this XmlWriter writer, ushort value)
        {
            writer.WriteAttributeString("value", value.ToString(numberFormatInfo));
        }

        public static void WriteAttributeValue(this XmlWriter writer, int value)
        {
            writer.WriteAttributeString("value", value.ToString(numberFormatInfo));
        }

        public static void WriteAttributeValue(this XmlWriter writer, uint value)
        {
            writer.WriteAttributeString("value", value.ToString(numberFormatInfo));
        }

        public static void WriteAttributeValue(this XmlWriter writer, Half value)
        {
            writer.WriteAttributeString("value", value.ToString(floatFormat, numberFormatInfo));
        }

        public static void WriteAttributeValue(this XmlWriter writer, float value)
        {
            writer.WriteAttributeString("value", value.ToString(floatFormat, numberFormatInfo));
        }

        public static void WriteAttributeValue(this XmlWriter writer, double value)
        {
            writer.WriteAttributeString("value", value.ToString(floatFormat, numberFormatInfo));
        }

        public static void WriteAttributesXY(this XmlWriter writer, Vector2 value)
        {
            writer.WriteAttributeString("x", value.X.ToString(floatFormat, numberFormatInfo));
            writer.WriteAttributeString("y", value.Y.ToString(floatFormat, numberFormatInfo));
        }

        public static void WriteAttributesXYZ(this XmlWriter writer, Vector3 value)
        {
            writer.WriteAttributeString("x", value.X.ToString(floatFormat, numberFormatInfo));
            writer.WriteAttributeString("y", value.Y.ToString(floatFormat, numberFormatInfo));
            writer.WriteAttributeString("z", value.Z.ToString(floatFormat, numberFormatInfo));
        }

        public static void WriteAttributesXYZW(this XmlWriter writer, Vector4 value)
        {
            writer.WriteAttributeString("x", value.X.ToString(floatFormat, numberFormatInfo));
            writer.WriteAttributeString("y", value.Y.ToString(floatFormat, numberFormatInfo));
            writer.WriteAttributeString("z", value.Z.ToString(floatFormat, numberFormatInfo));
            writer.WriteAttributeString("w", value.W.ToString(floatFormat, numberFormatInfo));
        }

        public static void WriteAttributesXYZW(this XmlWriter writer, Quaternion value)
        {
            writer.WriteAttributeString("x", value.X.ToString(floatFormat, numberFormatInfo));
            writer.WriteAttributeString("y", value.Y.ToString(floatFormat, numberFormatInfo));
            writer.WriteAttributeString("z", value.Z.ToString(floatFormat, numberFormatInfo));
            writer.WriteAttributeString("w", value.W.ToString(floatFormat, numberFormatInfo));
        }

        public static void WriteAttributeValueAsHex(this XmlWriter writer, uint value)
        {
            writer.WriteAttributeString("value", $"0x{value:X8}");
        }
    }
}
