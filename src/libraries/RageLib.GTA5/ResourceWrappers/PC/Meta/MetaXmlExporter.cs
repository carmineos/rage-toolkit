// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.GTA5.ResourceWrappers.PC.Meta.Types;
using RageLib.Helpers.Xml;
using RageLib.Resources.GTA5.PC.Meta;
using RageLib.Services;
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
        private readonly IJenkinsResolver _jenkinsResolver;

        public MetaXmlExporter(IJenkinsResolver jenkinsResolver)
        {
            _jenkinsResolver = jenkinsResolver ?? JenkinsDictionary.Shared;
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
                                case StructureEntryDataType.UInt8:
                                    WriteInlineArrayContentChar((XmlRageWriter)writer, arrayValue);
                                    break;
                                case StructureEntryDataType.UInt16:
                                    WriteInlineArrayContentShort((XmlRageWriter)writer, arrayValue);
                                    break;
                                case StructureEntryDataType.UInt32:
                                    WriteInlineArrayContentInt((XmlRageWriter)writer, arrayValue);
                                    break;
                                case StructureEntryDataType.Float:
                                    WriteInlineArrayContentFloat((XmlRageWriter)writer, arrayValue);
                                    break;
                                case StructureEntryDataType.Vector3:
                                    WriteInlineArrayContentVector3((XmlRageWriter)writer, arrayValue);
                                    break;
                                case StructureEntryDataType.StringHash:
                                    WriteHashArrayContent(writer, arrayValue);
                                    break;
                                default:
                                    WriteStructureArrayContent(writer, arrayValue);
                                    break;
                            }
                        }
                        break;
                    }

                case MetaBool:
                    writer.WriteAttributeValue(((MetaBool)value).Value);
                    break;

                case MetaSByte:
                    writer.WriteAttributeValue(((MetaSByte)value).Value);
                    break;

                case MetaByte:
                    writer.WriteAttributeValue(((MetaByte)value).Value);
                    break;

                case MetaInt16:
                    writer.WriteAttributeValue(((MetaInt16)value).Value);
                    break;

                case MetaUInt16:
                    writer.WriteAttributeValue(((MetaUInt16)value).Value);
                    break;

                case MetaInt32:
                    writer.WriteAttributeValue(((MetaInt32)value).Value);
                    break;

                case MetaUInt32:
                    writer.WriteAttributeValue(((MetaUInt32)value).Value);
                    break;

                case MetaFloat:
                    writer.WriteAttributeValue(((MetaFloat)value).Value);
                    break;

                case MetaVector3:
                    writer.WriteAttributesXYZ(((MetaVector3)value).Value);
                    break;

                case MetaVector4:
                    writer.WriteAttributesXYZW(((MetaVector4)value).Value);
                    break;

                case MetaEnumInt8:
                    WriteEnumContent(writer, (MetaEnumInt8)value);
                    break;
                case MetaEnumInt16:
                    WriteEnumContent(writer, (MetaEnumInt16)value);
                    break;
                case MetaEnumInt32:
                    WriteEnumContent(writer, (MetaEnumInt32)value);
                    break;
                case MetaFlagsInt16:
                    WriteFlagsContent(writer, (MetaFlagsInt16)value);
                    break;
                case MetaFlagsInt8:
                    WriteFlagsContent(writer, (MetaFlagsInt8)value);
                    break;
                case MetaFlagsInt32:
                    WriteFlagsContent(writer, (MetaFlagsInt32)value);
                    break;
                case MetaString:
                    {
                        var stringValue = value as MetaString;
                        writer.WriteString(stringValue.Value);
                        break;
                    }

                case MetaStringPointer:
                    {
                        var stringValue = value as MetaStringPointer;
                        writer.WriteString(stringValue.Value);
                        break;
                    }
                case MetaStringHash:
                    {
                        var intValue = value as MetaStringHash;
                        if (intValue.Value != 0)
                        {
                            writer.WriteString(GetNameForHash(intValue.Value));
                        }

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

                case MetaArrayLocal:
                    {
                        var xmlRageWriter = (XmlRageWriter)writer;
                        
                        switch (value)
                        {
                            case MetaArrayLocal<byte> bytes:
                                {
                                    xmlRageWriter.WriteInlineArrayContent(bytes.Value);
                                    break;
                                }
                            case MetaArrayLocal<ushort> ushorts:
                                {
                                    xmlRageWriter.WriteInlineArrayContent(ushorts.Value);
                                    break;
                                }
                            case MetaArrayLocal<uint> uints:
                                {
                                    xmlRageWriter.WriteInlineArrayContent(uints.Value);
                                    break;
                                }
                            case MetaArrayLocal<float> floats:
                                {
                                    xmlRageWriter.WriteInlineArrayContent(floats.Value);
                                    break;
                                }
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


        private void WriteEnumContent(XmlWriter writer, MetaEnumInt8 byteValue)
        {
            var thehash = (int)0;
            foreach (var enty in byteValue.info.Entries)
                if (enty.EntryValue == byteValue.Value)
                    thehash = enty.EntryNameHash;
            writer.WriteString(GetEnumNameForHash(thehash));
        }

        private void WriteEnumContent(XmlWriter writer, MetaEnumInt16 intValue)
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

        private void WriteEnumContent(XmlWriter writer, MetaEnumInt32 intValue)
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

        private void WriteFlagsContent(XmlWriter writer, MetaFlagsInt16 shortValue)
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

        private void WriteFlagsContent(XmlWriter writer, MetaFlagsInt8 intValue)
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

        private void WriteFlagsContent(XmlWriter writer, MetaFlagsInt32 intValue)
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

        private void WriteInlineArrayContentChar(XmlRageWriter writer, MetaArray arrayValue)
        {
            writer.WriteAttributeString("content", "char_array");

            writer.WriteString(Environment.NewLine);
            foreach (var k in arrayValue.Entries)
            {
                var value = ((MetaByte)k).Value;
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
                var value = ((MetaUInt16)k).Value;
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
                var value = ((MetaUInt32)k).Value;
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
                var value = ((MetaVector3)k).Value;
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
                var ii = ((MetaStringHash)k).Value;
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
            return _jenkinsResolver.TryGetValue(hash, out string resolved) ? resolved : $"hash_{hash:X8}";
        }

        private string GetEnumNameForHash(int hash)
        {
            return _jenkinsResolver.TryGetValue(hash, out string resolved) ? resolved : $"enum_hash_{hash:X8}";
        }

        private string GetFlagNameForHash(int hash)
        {
            return _jenkinsResolver.TryGetValue(hash, out string resolved) ? resolved : $"flag_hash_{hash:X8}";
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
