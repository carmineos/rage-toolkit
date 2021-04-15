using System;
using System.Globalization;
using System.Numerics;
using System.Xml;

namespace RageLib.Helpers.Xml
{
    public static class XmlReaderExtensions
    {
        public static bool GetAttributeValueAsBool(this XmlReader reader)
        {
            return bool.Parse(reader.GetAttribute("value"));
        }

        public static byte GetAttributeValueAsByte(this XmlReader reader)
        {
            return byte.Parse(reader.GetAttribute("value"), NumberFormatInfo.InvariantInfo);
        }

        public static sbyte GetAttributeValueAsSByte(this XmlReader reader)
        {
            return sbyte.Parse(reader.GetAttribute("value"), NumberFormatInfo.InvariantInfo);
        }

        public static short GetAttributeValueAsInt16(this XmlReader reader)
        {
            return short.Parse(reader.GetAttribute("value"), NumberFormatInfo.InvariantInfo);
        }

        public static ushort GetAttributeValueAsUInt16(this XmlReader reader)
        {
            return ushort.Parse(reader.GetAttribute("value"), NumberFormatInfo.InvariantInfo);
        }

        public static int GetAttributeValueAsInt32(this XmlReader reader)
        {
            return int.Parse(reader.GetAttribute("value"), NumberFormatInfo.InvariantInfo);
        }

        public static uint GetAttributeValueAsUInt32(this XmlReader reader)
        {
            return uint.Parse(reader.GetAttribute("value"), NumberFormatInfo.InvariantInfo);
        }

        public static Half GetAttributeValueAsHalf(this XmlReader reader)
        {
            return Half.Parse(reader.GetAttribute("value"), NumberFormatInfo.InvariantInfo);
        }

        public static float GetAttributeValueAsFloat(this XmlReader reader)
        {
            return float.Parse(reader.GetAttribute("value"), NumberFormatInfo.InvariantInfo);
        }

        public static double GetAttributeValueAsDouble(this XmlReader reader)
        {
            return double.Parse(reader.GetAttribute("value"), NumberFormatInfo.InvariantInfo);
        }

        public static Vector2 GetAttributesXYAsVector2(this XmlReader reader)
        {
            float x = float.Parse(reader.GetAttribute("x"), NumberFormatInfo.InvariantInfo);
            float y = float.Parse(reader.GetAttribute("y"), NumberFormatInfo.InvariantInfo);
            return new Vector2(x, y);
        }

        public static Vector3 GetAttributesXYZAsVector3(this XmlReader reader)
        {
            float x = float.Parse(reader.GetAttribute("x"), NumberFormatInfo.InvariantInfo);
            float y = float.Parse(reader.GetAttribute("y"), NumberFormatInfo.InvariantInfo);
            float z = float.Parse(reader.GetAttribute("z"), NumberFormatInfo.InvariantInfo);
            return new Vector3(x, y, z);
        }

        public static Vector4 GetAttributesXYZWAsVector4(this XmlReader reader)
        {
            float x = float.Parse(reader.GetAttribute("x"), NumberFormatInfo.InvariantInfo);
            float y = float.Parse(reader.GetAttribute("y"), NumberFormatInfo.InvariantInfo);
            float z = float.Parse(reader.GetAttribute("z"), NumberFormatInfo.InvariantInfo);
            float w = float.Parse(reader.GetAttribute("w"), NumberFormatInfo.InvariantInfo);
            return new Vector4(x, y, z, w);
        }

        public static Quaternion GetAttributesXYZWAsQuaternion(this XmlReader reader)
        {
            float x = float.Parse(reader.GetAttribute("x"), NumberFormatInfo.InvariantInfo);
            float y = float.Parse(reader.GetAttribute("y"), NumberFormatInfo.InvariantInfo);
            float z = float.Parse(reader.GetAttribute("z"), NumberFormatInfo.InvariantInfo);
            float w = float.Parse(reader.GetAttribute("w"), NumberFormatInfo.InvariantInfo);
            return new Quaternion(x, y, z, w);
        }

        public static uint GetAttributeValueAsUInt32FromHex(this XmlReader reader)
        {
            return Convert.ToUInt32(reader.GetAttribute("value"), 16);
        }
    }
}
