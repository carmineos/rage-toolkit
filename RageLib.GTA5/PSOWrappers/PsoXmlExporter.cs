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

using RageLib.GTA5.PSO;
using RageLib.GTA5.PSOWrappers.Types;
using RageLib.Helpers.Xml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace RageLib.GTA5.PSOWrappers
{
    public class PsoXmlExporter
    {
        public Dictionary<int, string> HashMapping { get; set; }

        public PsoXmlExporter()
        {
            HashMapping = new Dictionary<int, string>();
        }

        public void Export(IPsoValue value, string xmlFileName)
        {
            using (var xmlFileStream = new FileStream(xmlFileName, FileMode.Create))
            {
                Export(value, xmlFileStream);
            }
        }

        public void Export(IPsoValue value, Stream xmlFileStream)
        {
            var strctureValue = (PsoStructure)value;

            var writer = new XmlRageWriter(XmlWriter.Create(xmlFileStream, new XmlWriterSettings() { Indent = true, Encoding = new UTF8Encoding(false), }));
            writer.WriteStartDocument();
            writer.WriteStartElement(GetNameForHash(strctureValue.entryIndexInfo.NameHash));
            WriteStructureContentXml(writer, strctureValue);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
        }

        private void WriteStructureContentXml(XmlWriter writer, PsoStructure value)
        {
            foreach (var field in value.Values)
            {
                var fieldNameHash = field.Key;
                var fieldValue = field.Value;
                var fixedName = GetNameForHash(fieldNameHash);

                writer.WriteStartElement(fixedName);
                WriteStructureElementContentXml(writer, fieldValue);
                writer.WriteEndElement();
            }
        }

        private void WriteStructureContentXml(XmlWriter writer, PsoStructure3 value)
        {
            if (value.Value is null)
            {
                writer.WriteAttributeString("type", "NULL");
            }
            else
            {
                writer.WriteAttributeString("type", GetNameForHash(value.Value.entryIndexInfo.NameHash));
                WriteStructureContentXml(writer, value.Value);
            }
        }

        private void WriteStructureElementContentXml(XmlWriter writer, IPsoValue value)
        {
            switch (value)
            {
                case PsoArray0:
                    WriteStructuredArray(writer, ((PsoArray0)value).Entries);
                    break;
                case PsoArray1 array1:
                    WriteArray(writer, array1.Entries, array1.entryInfo);
                    break;
                case PsoArray2 array2:
                    WriteArray(writer, array2.Entries, array2.entryInfo);
                    break;
                case PsoArray4 array4:
                    WriteArray(writer, array4.Entries, array4.entryInfo);
                    break;
                case PsoArray129 array129:
                    WriteArray(writer, array129.Entries, array129.entryInfo);
                    break;
                case PsoUInt32:
                    writer.WriteAttributeValue(((PsoUInt32)value).Value);
                    break;
                case PsoUInt32Hex:
                    writer.WriteAttributeValueAsHex(((PsoUInt32Hex)value).Value);
                    break;
                case PsoBoolean:
                    writer.WriteAttributeValue(((PsoBoolean)value).Value);
                    break;
                case PsoByte:
                    writer.WriteAttributeValue(((PsoByte)value).Value);
                    break;
                case PsoSByte:
                    writer.WriteAttributeValue(((PsoSByte)value).Value);
                    break;
                case PsoInt16:
                    writer.WriteAttributeValue(((PsoInt16)value).Value);
                    break;
                case PsoUInt16:
                    writer.WriteAttributeValue(((PsoUInt16)value).Value);
                    break;
                case PsoInt32:
                    writer.WriteAttributeValue(((PsoInt32)value).Value);
                    break;
                case PsoEnumByte:
                    WriteEnumContent(writer, (PsoEnumByte)value);
                    break;
                case PsoEnumInt:
                    WriteEnumContent(writer, (PsoEnumInt)value);
                    break;
                case PsoFlagsByte:
                    WriteFlagsContent(writer, (PsoFlagsByte)value);
                    break;
                case PsoFlagsShort:
                    WriteFlagsContent(writer, (PsoFlagsShort)value);
                    break;
                case PsoFlagsInt:
                    WriteFlagsContent(writer, (PsoFlagsInt)value);
                    break;
                case PsoFloat:
                    writer.WriteAttributeValue(((PsoFloat)value).Value);
                    break;
                case PsoVector2:
                    writer.WriteAttributesXY(((PsoVector2)value).Value);
                    break;
                case PsoVec3V:
                    writer.WriteAttributesXYZ(((PsoVec3V)value).Value);
                    break;
                case PsoVec4V:
                    writer.WriteAttributesXYZW(((PsoVec4V)value).Value);
                    break;
                case PsoVector4:
                    writer.WriteAttributesXYZW(((PsoVector4)value).Value);
                    break;
                case PsoMap:
                    WriteMapContent(writer, (PsoMap)value);
                    break;
                case PsoString0:
                    WriteStringContent(writer, ((PsoString0)value).Value);
                    break;
                case PsoString1:
                    WriteStringContent(writer, ((PsoString1)value).Value);
                    break;
                case PsoString2:
                    WriteStringContent(writer, ((PsoString2)value).Value);
                    break;
                case PsoString3:
                    WriteStringContent(writer, ((PsoString3)value).Value);
                    break;
                case PsoString7:
                    WriteStringContent(writer, ((PsoString7)value).Value);
                    break;
                case PsoString8:
                    WriteStringContent(writer, ((PsoString8)value).Value);
                    break;
                
                case PsoStructure:
                    WriteStructureContentXml(writer, (PsoStructure)value);
                    break;
                case PsoStructure3:
                    WriteStructureContentXml(writer, (PsoStructure3)value);
                    break;
                case PsoFloat16:
                    writer.WriteAttributeValue(((PsoFloat16)value).Value);
                    break;
                case PsoVector3:
                    writer.WriteAttributesXYZ(((PsoVector3)value).Value);
                    break;
                case PsoUInt64:
                    writer.WriteAttributeValue(((PsoUInt64)value).Value);
                    break;
                case PsoInt64:
                    writer.WriteAttributeValue(((PsoInt64)value).Value);
                    break;
                default:
                    throw new Exception("Unknown type");
            }
        }

        private void WriteArray(XmlWriter writer, List<IPsoValue> Entries, PsoStructureEntryInfo entryInfo)
        {
            switch (entryInfo.Type)
            {
                case ParMemberType.UCHAR:
                case ParMemberType.CHAR:
                    WriteInlineArrayContentChar((XmlRageWriter)writer, Entries);
                    break;
                case ParMemberType.USHORT:
                case ParMemberType.SHORT:
                    WriteInlineArrayContentShort((XmlRageWriter)writer, Entries);
                    break;
                case ParMemberType.UINT:
                case ParMemberType.INT:
                    WriteInlineArrayContentInt((XmlRageWriter)writer, Entries);
                    break;
                case ParMemberType.FLOAT:
                    WriteInlineArrayContentFloat((XmlRageWriter)writer, Entries);
                    break;
                case ParMemberType.VECTOR2:
                    WriteInlineArrayContentVector2((XmlRageWriter)writer, Entries);
                    break;
                case ParMemberType.VECTOR3:
                    WriteInlineArrayContentVector3((XmlRageWriter)writer, Entries);
                    break;
                default:
                    WriteStructuredArray(writer, Entries);
                    break;
            }
        }

        // TODO:    Fix broken indentation
        //          move these methods to XmlWriterExtentions to avoid duplicated code
        private void WriteInlineArrayContentChar(XmlRageWriter writer, List<IPsoValue> entries)
        {
            writer.WriteAttributeString("content", "char_array");

            writer.WriteString(Environment.NewLine);
            foreach (var arrayEntry in entries)
            {
                writer.WriteInnerIndent();
                if (arrayEntry is PsoSByte)
                {
                    var value = ((PsoSByte)arrayEntry).Value;
                    writer.WriteString(value.ToString());
                }
                else if(arrayEntry is PsoByte)
                {
                    var value = ((PsoByte)arrayEntry).Value;
                    writer.WriteString(value.ToString());
                }
                writer.WriteString(Environment.NewLine);
            }
            writer.WriteOuterIndent();
        }

        private void WriteInlineArrayContentShort(XmlRageWriter writer, List<IPsoValue> entries)
        {
            writer.WriteAttributeString("content", "short_array");

            writer.WriteString(Environment.NewLine);
            foreach (var arrayEntry in entries)
            {
                writer.WriteInnerIndent();
                if (arrayEntry is PsoInt16)
                {
                    var value = ((PsoInt16)arrayEntry).Value;
                    writer.WriteString(value.ToString());
                }
                else if (arrayEntry is PsoUInt16)
                {
                    var value = ((PsoUInt16)arrayEntry).Value;
                    writer.WriteString(value.ToString());
                }
                writer.WriteString(Environment.NewLine);
            }
            writer.WriteOuterIndent();
        }

        private void WriteInlineArrayContentInt(XmlRageWriter writer, List<IPsoValue> entries)
        {
            writer.WriteAttributeString("content", "int_array");

            writer.WriteString(Environment.NewLine);
            foreach (var arrayEntry in entries)
            {
                writer.WriteInnerIndent();
                if (arrayEntry is PsoInt32)
                {
                    var value = ((PsoInt32)arrayEntry).Value;
                    writer.WriteString(value.ToString());
                }
                else if (arrayEntry is PsoUInt32)
                {
                    var value = ((PsoUInt32)arrayEntry).Value;
                    writer.WriteString(value.ToString());
                }
                writer.WriteString(Environment.NewLine);
            }
            writer.WriteOuterIndent();
        }

        private void WriteInlineArrayContentFloat(XmlRageWriter writer, List<IPsoValue> entries)
        {
            writer.WriteAttributeString("content", "float_array");

            writer.WriteString(Environment.NewLine);
            foreach (var arrayEntry in entries)
            {
                writer.WriteInnerIndent();
                var value = ((PsoFloat)arrayEntry).Value;
                writer.WriteString(value.ToString("0.000000", NumberFormatInfo.InvariantInfo));
                writer.WriteString(Environment.NewLine);
            }
            writer.WriteOuterIndent();
        }

        private void WriteInlineArrayContentVector2(XmlRageWriter writer, List<IPsoValue> entries)
        {
            writer.WriteAttributeString("content", "vector2_array");

            writer.WriteString(Environment.NewLine);
            foreach (var arrayEntry in entries)
            {
                writer.WriteInnerIndent();
                var value = ((PsoVector2)arrayEntry).Value;
                writer.WriteString(value.X.ToString("0.000000\t", NumberFormatInfo.InvariantInfo));
                writer.WriteString(value.Y.ToString("0.000000\t", NumberFormatInfo.InvariantInfo));
                writer.WriteString(Environment.NewLine);
            }
            writer.WriteOuterIndent();
        }

        private void WriteInlineArrayContentVector3(XmlRageWriter writer, List<IPsoValue> entries)
        {
            writer.WriteAttributeString("content", "vector3_array");

            writer.WriteString(Environment.NewLine);
            foreach (var arrayEntry in entries)
            {
                writer.WriteInnerIndent();
                var value = ((PsoVector3)arrayEntry).Value;
                writer.WriteString(value.X.ToString("0.000000\t", NumberFormatInfo.InvariantInfo));
                writer.WriteString(value.Y.ToString("0.000000\t", NumberFormatInfo.InvariantInfo));
                writer.WriteString(value.Z.ToString("0.000000\t", NumberFormatInfo.InvariantInfo));
                writer.WriteString(Environment.NewLine);
            }
            writer.WriteOuterIndent();
        }

        private void WriteStructuredArray(XmlWriter writer, List<IPsoValue> entries)
        {
            if (entries is null)
                return;

            foreach (var arrayEntry in entries)
            {
                writer.WriteStartElement("Item");
                
                switch (arrayEntry)
                {
                    case PsoStructure:
                        WriteStructureContentXml(writer, (PsoStructure)arrayEntry);
                        break;
                    case PsoStructure3:
                        WriteStructureContentXml(writer, (PsoStructure3)arrayEntry);
                        break;
                    default:
                        WriteStructureElementContentXml(writer, arrayEntry);
                        break;
                }

                writer.WriteEndElement();
            }
        }
        
        private void WriteEnumContent(XmlWriter writer, PsoEnumByte value)
        {
            var matchingEnumEntry = (PsoEnumEntryInfo)null;
            foreach (var enumEntry in value.TypeInfo.Entries)
            {
                if (enumEntry.EntryKey == value.Value)
                    matchingEnumEntry = enumEntry;
            }

            if (matchingEnumEntry != null)
            {
                var matchingEntryName = GetNameForHash(matchingEnumEntry.EntryNameHash);
                writer.WriteString(matchingEntryName);
            }
        }

        private void WriteEnumContent(XmlWriter writer, PsoEnumInt value)
        {
            var matchingEnumEntry = (PsoEnumEntryInfo)null;
            foreach (var enumEntry in value.TypeInfo.Entries)
            {
                if (enumEntry.EntryKey == value.Value)
                    matchingEnumEntry = enumEntry;
            }

            if (matchingEnumEntry != null)
            {
                var matchingEntryName = GetNameForHash(matchingEnumEntry.EntryNameHash);
                writer.WriteString(matchingEntryName);
            }
        }

        private void WriteFlagsContent(XmlWriter writer, PsoFlagsByte value)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                if ((value.Value & (1 << i)) != 0)
                {
                    var machingFlagEntry = (PsoEnumEntryInfo)null;
                    foreach (var flagEntry in value.TypeInfo.Entries)
                    {
                        if (flagEntry.EntryKey == i)
                            machingFlagEntry = flagEntry;
                    }

                    var matchingFlagName = GetNameForHash(machingFlagEntry.EntryNameHash);
                    sb.Append(matchingFlagName);
                    sb.Append(' ');
                }
            }

            var flagsString = sb.ToString().Trim();
            writer.WriteString(flagsString);
        }

        private void WriteFlagsContent(XmlWriter writer, PsoFlagsShort value)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 16; i++)
            {
                if ((value.Value & (1 << i)) != 0)
                {
                    var machingFlagEntry = (PsoEnumEntryInfo)null;
                    foreach (var flagEntry in value.TypeInfo.Entries)
                    {
                        if (flagEntry.EntryKey == i)
                            machingFlagEntry = flagEntry;
                    }

                    var matchingFlagName = GetNameForHash(machingFlagEntry.EntryNameHash);
                    sb.Append(matchingFlagName);
                    sb.Append(' ');
                }
            }

            var flagsString = sb.ToString().Trim();
            writer.WriteString(flagsString);
        }

        private void WriteFlagsContent(XmlWriter writer, PsoFlagsInt value)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 32; i++)
            {
                if ((value.Value & (1 << i)) != 0)
                {
                    var machingFlagEntry = (PsoEnumEntryInfo)null;
                    foreach (var flagEntry in value.TypeInfo.Entries)
                    {
                        if (flagEntry.EntryKey == i)
                            machingFlagEntry = flagEntry;
                    }

                    var matchingFlagName = GetNameForHash(machingFlagEntry.EntryNameHash);
                    sb.Append(matchingFlagName);
                    sb.Append(' ');
                }
            }

            var flagsString = sb.ToString().Trim();
            writer.WriteString(flagsString);
        }

        

        private void WriteMapContent(XmlWriter writer, PsoMap value)
        {
            if (value.Entries is null)
                return;

            foreach (var arrayEntry in value.Entries)
            {
                writer.WriteStartElement("Item");

                var strKey = (PsoString7)arrayEntry.Values[0x6098a50e];
                writer.WriteAttributeString("key", GetNameForHash(strKey.Value));

                var kk = arrayEntry.Values[0x063fa3f2];

                switch (kk)
                {
                    case PsoStructure:
                        WriteStructureContentXml(writer, (PsoStructure)kk);
                        break;
                    case PsoStructure3:
                        WriteStructureContentXml(writer, (PsoStructure3)kk);
                        break;
                    default:
                        WriteStructureElementContentXml(writer, kk);
                        break;
                }

                writer.WriteEndElement();
            }
        }

        private void WriteStringContent(XmlWriter writer, string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            writer.WriteString(value.Replace("\0", ""));
        }

        private void WriteStringContent(XmlWriter writer, int value)
        {
            if (value == 0)
                return;
            
            writer.WriteString(GetNameForHash(value));
        }

        private string GetNameForHash(int hash)
        {
            if (HashMapping.ContainsKey(hash))
            {
                var ss = HashMapping[hash];
                return ss;
            }
            else
            {
                //return $"Unknown_Hash_0x{hash:X8}";
                throw new Exception($"Hash 0x{hash:X8} can't be resolved");
            }
        }
    }
}
