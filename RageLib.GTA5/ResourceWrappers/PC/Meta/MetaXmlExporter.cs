/*
    Copyright(c) 2016 Neodymium

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

using RageLib.GTA5.ResourceWrappers.PC.Meta.Types;
using RageLib.Helpers.Xml;
using RageLib.Resources.GTA5.PC.Meta;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta
{
    public class MetaXmlExporter
    {
        public Dictionary<int, string> HashMapping { get; set; }

        public MetaXmlExporter()
        {
            HashMapping = new Dictionary<int, string>();
        }

        public void Export(IMetaValue value, string xmlFileName)
        {
            using (var xmlFileStream = new FileStream(xmlFileName, FileMode.Create))
            {
                Export(value, xmlFileStream);
            }
        }

        public void Export(IMetaValue value, Stream xmlFileStream)
        {
            var strctureValue = (MetaStructure)value;

            var writer = new XmlRageWriter(XmlWriter.Create(xmlFileStream, new XmlWriterSettings() { Indent = true, Encoding = new UTF8Encoding(false) }));
            writer.WriteStartDocument();
            writer.WriteStartElement(GetNameForHash(strctureValue.info.StructureNameHash));
            WriteStructureContentXml(strctureValue, writer);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
        }

        private void WriteStructureContentXml(MetaStructure value, XmlWriter writer)
        {
            foreach (var field in value.Values)
            {
                var fieldNameHash = field.Key;
                var fieldValue = field.Value;
                writer.WriteStartElement(GetNameForHash(fieldNameHash));
                WriteStructureElementContentXml(fieldValue, writer);
                writer.WriteEndElement();
            }
        }

        private void WriteStructureElementContentXml(IMetaValue value, XmlWriter writer)
        {
            switch (value)
            {
                case MetaArray:
                    {
                        var arrayValue = value as MetaArray;
                        if (arrayValue.Entries != null)
                        {
                            switch (arrayValue.info.DataType)
                            {
                                case StructureEntryDataType.UnsignedByte:
                                    WriteInlineArrayContentChar((XmlRageWriter)writer, arrayValue);
                                    break;
                                case StructureEntryDataType.UnsignedShort:
                                    WriteInlineArrayContentShort((XmlRageWriter)writer, arrayValue);
                                    break;
                                case StructureEntryDataType.UnsignedInt:
                                    WriteInlineArrayContentInt((XmlRageWriter)writer, arrayValue);
                                    break;
                                case StructureEntryDataType.Float:
                                    WriteInlineArrayContentFloat((XmlRageWriter)writer, arrayValue);
                                    break;
                                case StructureEntryDataType.Float_XYZ:
                                    WriteInlineArrayContentVector3((XmlRageWriter)writer, arrayValue);
                                    break;
                                case StructureEntryDataType.Hash:
                                    WriteHashArrayContent(writer, arrayValue);
                                    break;
                                default:
                                    WriteStructureArrayContent(writer, arrayValue);
                                    break;
                            }
                        }
                        break;
                    }

                case MetaBoolean:
                    writer.WriteAttributeValue(((MetaBoolean)value).Value);
                    break;

                case MetaByte_A:
                    writer.WriteAttributeValue(((MetaByte_A)value).Value);
                    break;

                case MetaByte_B:
                    writer.WriteAttributeValue(((MetaByte_B)value).Value);
                    break;

                case MetaInt16_A:
                    writer.WriteAttributeValue(((MetaInt16_A)value).Value);
                    break;

                case MetaInt16_B:
                    writer.WriteAttributeValue(((MetaInt16_B)value).Value);
                    break;

                case MetaInt32_A:
                    writer.WriteAttributeValue(((MetaInt32_A)value).Value);
                    break;

                case MetaInt32_B:
                    writer.WriteAttributeValue(((MetaInt32_B)value).Value);
                    break;

                case MetaFloat:
                    writer.WriteAttributeValue(((MetaFloat)value).Value);
                    break;

                case MetaFloat4_XYZ:
                    var xyzValue = (MetaFloat4_XYZ)value;
                    writer.WriteAttributesXYZ(xyzValue.X, xyzValue.Y, xyzValue.Z);
                    break;

                case MetaFloat4_XYZW:
                    var xyzwValue = (MetaFloat4_XYZW)value;
                    writer.WriteAttributesXYZW(xyzwValue.X, xyzwValue.Y, xyzwValue.Z, xyzwValue.W);
                    break;

                case MetaByte_Enum:
                    WriteByteEnumContent(writer, (MetaByte_Enum)value);
                    break;
                case MetaInt32_Enum1:
                    WriteIntEnumContent(writer, (MetaInt32_Enum1)value);
                    break;
                case MetaInt16_Enum:
                    WriteShortFlagsContent(writer, (MetaInt16_Enum)value);
                    break;
                case MetaInt32_Enum2:
                    WriteIntFlags1Content(writer, (MetaInt32_Enum2)value);
                    break;
                case MetaInt32_Enum3:
                    WriteIntFlags2Content(writer, (MetaInt32_Enum3)value);
                    break;
                case MetaArrayOfChars:
                    {
                        var stringValue = value as MetaArrayOfChars;
                        writer.WriteString(stringValue.Value);
                        break;
                    }

                case MetaCharPointer:
                    {
                        var stringValue = value as MetaCharPointer;
                        writer.WriteString(stringValue.Value);
                        break;
                    }

                case MetaGeneric:
                    {
                        var genericValue = value as MetaGeneric;
                        var val = (MetaStructure)genericValue.Value;
                        if (val != null)
                        {
                            var vbstrdata = val;
                            writer.WriteAttributeString("type", GetNameForHash(vbstrdata.info.StructureNameHash));
                            WriteStructureContentXml(vbstrdata, writer);
                        }
                        else
                        {
                            writer.WriteAttributeString("type", "NULL");
                        }

                        break;
                    }

                case MetaArrayOfBytes:
                    {
                        WriteInlineArrayBytes((XmlRageWriter)writer, (MetaArrayOfBytes)value);
                        break;
                    }

                case MetaInt32_Hash:
                    {
                        var intValue = value as MetaInt32_Hash;
                        if (intValue.Value != 0)
                        {
                            writer.WriteString(GetNameForHash(intValue.Value));
                        }

                        break;
                    }

                case MetaDataBlockPointer:
                    {
                        var longValue = value as MetaDataBlockPointer;
                        if (longValue.Data != null)
                        {
                            writer.WriteString(ByteArrayToString(longValue.Data));
                        }

                        break;
                    }

                case MetaStructure:
                    {
                        var structureValue = value as MetaStructure;
                        WriteStructureContentXml(structureValue, writer);
                        break;
                    }

                default:
                    break;
            }
        }


        private void WriteByteEnumContent(XmlWriter writer, MetaByte_Enum byteValue)
        {
            var thehash = (int)0;
            foreach (var enty in byteValue.info.Entries)
                if (enty.EntryValue == byteValue.Value)
                    thehash = enty.EntryNameHash;
            writer.WriteString(GetEnumNameForHash(thehash));
        }

        private void WriteIntEnumContent(XmlWriter writer, MetaInt32_Enum1 intValue)
        {
            if (intValue.Value != -1)
            {
                var thehash = (int)0;
                foreach (var enty in intValue.info.Entries)
                    if (enty.EntryValue == intValue.Value)
                        thehash = enty.EntryNameHash;
                writer.WriteString(GetEnumNameForHash(thehash));
            }
            else
            {
                writer.WriteString("enum_NONE");
            }
        }

        private void WriteShortFlagsContent(XmlWriter writer, MetaInt16_Enum shortValue)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 16; i++)
            {
                if ((shortValue.Value & (1 << i)) != 0)
                {
                    foreach (var xy in shortValue.info.Entries)
                    {
                        if (xy.EntryValue == i)
                        {
                            sb.Append(' ');
                            sb.Append(GetFlagNameForHash(xy.EntryNameHash));
                        }
                    }
                }
            }
            writer.WriteString(sb.ToString().Trim());
        }

        private void WriteIntFlags1Content(XmlWriter writer, MetaInt32_Enum2 intValue)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 32; i++)
            {
                if ((intValue.Value & (1 << i)) != 0)
                {
                    foreach (var xy in intValue.info.Entries)
                    {
                        if (xy.EntryValue == i)
                        {
                            sb.Append(' ');
                            sb.Append(GetFlagNameForHash(xy.EntryNameHash));
                        }
                    }
                }
            }
            writer.WriteString(sb.ToString().Trim());
        }

        private void WriteIntFlags2Content(XmlWriter writer, MetaInt32_Enum3 intValue)
        {
            if (intValue.Value != 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 32; i++)
                {
                    if ((intValue.Value & (1 << i)) != 0)
                    {

                        if (intValue.info != null)
                        {
                            foreach (var xy in intValue.info.Entries)
                            {
                                if (xy.EntryValue == i)
                                {
                                    sb.Append(' ');
                                    sb.Append(GetFlagNameForHash(xy.EntryNameHash));
                                }
                            }
                        }
                        else
                        {
                            sb.Append(" flag_index_");
                            sb.Append(i);
                        }
                    }
                }
                writer.WriteString(sb.ToString().Trim());
            }
        }

        private void WriteInlineArrayBytes(XmlRageWriter writer, MetaArrayOfBytes arrayValue)
        {
            writer.WriteString(Environment.NewLine);
            for (int i = 0; i < arrayValue.Value.Length; i++)
            {
                var value = arrayValue.Value[i];
                writer.WriteInnerIndent();
                writer.WriteString(value.ToString());
                writer.WriteString(Environment.NewLine);
            }
            writer.WriteOuterIndent();
        }

        private void WriteInlineArrayContentChar(XmlRageWriter writer, MetaArray arrayValue)
        {
            writer.WriteAttributeString("content", "char_array");

            writer.WriteString(Environment.NewLine);
            foreach (var k in arrayValue.Entries)
            {
                var value = ((MetaByte_B)k).Value;
                writer.WriteInnerIndent();
                writer.WriteString(value.ToString());
                writer.WriteString(Environment.NewLine);
            }
            writer.WriteOuterIndent();
        }

        private void WriteInlineArrayContentShort(XmlRageWriter writer, MetaArray arrayValue)
        {
            writer.WriteAttributeString("content", "short_array");

            writer.WriteString(Environment.NewLine);
            foreach (var k in arrayValue.Entries)
            {
                var value = ((MetaInt16_B)k).Value;
                writer.WriteInnerIndent();
                writer.WriteString(value.ToString());
                writer.WriteString(Environment.NewLine);
            }
            writer.WriteOuterIndent();
        }

        private void WriteInlineArrayContentInt(XmlRageWriter writer, MetaArray arrayValue)
        {
            writer.WriteAttributeString("content", "int_array");

            writer.WriteString(Environment.NewLine);
            foreach (var k in arrayValue.Entries)
            {
                var value = ((MetaInt32_B)k).Value;
                writer.WriteInnerIndent();
                writer.WriteString(value.ToString());
                writer.WriteString(Environment.NewLine);
            }
            writer.WriteOuterIndent();
        }

        private void WriteInlineArrayContentFloat(XmlRageWriter writer, MetaArray arrayValue)
        {
            writer.WriteAttributeString("content", "float_array");

            writer.WriteString(Environment.NewLine);
            foreach (var k in arrayValue.Entries)
            {
                var value = ((MetaFloat)k).Value;
                writer.WriteInnerIndent();
                writer.WriteString(value.ToString("0.000000", NumberFormatInfo.InvariantInfo));
                writer.WriteString(Environment.NewLine);
            }
            writer.WriteOuterIndent();
        }

        private void WriteInlineArrayContentVector3(XmlRageWriter writer, MetaArray arrayValue)
        {
            writer.WriteAttributeString("content", "vector3_array");

            writer.WriteString(Environment.NewLine);
            foreach (var k in arrayValue.Entries)
            {
                var value = (MetaFloat4_XYZ)k;
                writer.WriteInnerIndent();
                writer.WriteString(value.X.ToString("0.000000\t", NumberFormatInfo.InvariantInfo));
                writer.WriteString(value.Y.ToString("0.000000\t", NumberFormatInfo.InvariantInfo));
                writer.WriteString(value.Z.ToString("0.000000\t", NumberFormatInfo.InvariantInfo));
                writer.WriteString(Environment.NewLine);
            }
            writer.WriteOuterIndent();
        }

        private void WriteHashArrayContent(XmlWriter writer, MetaArray arrayValue)
        {
            foreach (var k in arrayValue.Entries)
            {
                writer.WriteStartElement("Item");
                var ii = ((MetaInt32_Hash)k).Value;
                if (ii != 0)
                {
                    var ss = GetNameForHash(ii);
                    writer.WriteString(ss);
                }
                writer.WriteEndElement();
            }
        }

        private void WriteStructureArrayContent(XmlWriter writer, MetaArray arrayValue)
        {
            foreach (var k in arrayValue.Entries)
            {
                writer.WriteStartElement("Item");
                if (k is MetaStructure)
                {
                    WriteStructureContentXml(k as MetaStructure, writer);
                }
                else
                {
                    WriteStructureElementContentXml(k, writer);
                }
                writer.WriteEndElement();
            }
        }







        private string GetNameForHash(int hash)
        {
            return HashMapping.TryGetValue(hash, out string resolved) ? resolved : $"hash_{hash:X8}";
        }

        private string GetEnumNameForHash(int hash)
        {
            return HashMapping.TryGetValue(hash, out string resolved) ? resolved : $"enum_hash_{hash:X8}";
        }

        private string GetFlagNameForHash(int hash)
        {
            return HashMapping.TryGetValue(hash, out string resolved) ? resolved : $"flag_hash_{hash:X8}";
        }

        public string ByteArrayToString(byte[] b)
        {
            var result = new StringBuilder();
            for (int i = 0; i < b.Length; i++)
            {
                //result.Append("0x");
                result.Append(b[i]);
                if (i != b.Length - 1)
                {
                    result.Append(' ');
                }
            }
            return result.ToString();
        }
    }
}
