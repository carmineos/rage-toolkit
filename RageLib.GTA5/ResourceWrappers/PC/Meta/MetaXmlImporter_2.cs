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

using RageLib.GTA5.ResourceWrappers.PC.Meta.Descriptions;
using RageLib.GTA5.ResourceWrappers.PC.Meta.Types;
using RageLib.Hash;
using RageLib.Helpers.Xml;
using RageLib.Resources.Common;
using RageLib.Resources.GTA5.PC.Meta;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Xml;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta
{
    public class MetaXmlImporter2
    {
        private MetaInformationXml xmlInfos;
        private ResourceSimpleArray<StructureInfo> strList;

        public MetaStructure Import(string xmlFileName)
        {
            using (var xmlFileStream = new FileStream(xmlFileName, FileMode.Open))
            {
                return Import(xmlFileStream);
            }
        }

        public MetaXmlImporter2(MetaInformationXml xmlinfos)
        {
            this.xmlInfos = xmlinfos;
            MetaBuildStructureInfos(xmlinfos);
        }

        public MetaStructure Import(Stream xmlFileStream)
        {
            
            var reader = XmlReader.Create(xmlFileStream, new XmlReaderSettings() { IgnoreWhitespace = true });

            // Skip declaration
            reader.MoveToContent();

            var rootInfo = FindAndCheckStructure(reader.Name);

            var res = ParseStructure(reader, rootInfo);
            return res;
        }

        // TODO: Edit MetaInformationXml to store a dictionary for fast lookup by hash
        public MetaStructureXml FindAndCheckStructure(string name)
        {
            int hash = GetHashForName(name);
            foreach (var structure in xmlInfos.Structures)
            {
                if (structure.NameHash == hash)
                    return structure;
            }

            return null;
        }

        public StructureEntryInfo GetStructureEntryInfo(StructureInfo structureInfo, int entryInfoNameHash)
        {
            foreach (var entryInfo in structureInfo.Entries)
                if (entryInfo.EntryNameHash == entryInfoNameHash)
                    return entryInfo;

            return null;
        }

        public MetaStructureXml FindAndCheckStructure(int hash)
        {
            foreach (var structure in xmlInfos.Structures)
            {
                if (structure.NameHash == hash)
                    return structure;
            }

            return null;
        }

        public MetaStructure ParseStructure(XmlReader reader, MetaStructureXml info)
        {
            MetaStructure resultStructure = GetMetaStructure(info);
            resultStructure.Values = new Dictionary<int, IMetaValue>();

            reader.ReadStartElement();

            // TODO: Invert loop, loop on xml nodes and fill struct not the opposite!
            for (int i = 0; i < info.Entries.Count; i++)
            {
                var xmlEntry = info.Entries[i];

                var type = (StructureEntryDataType)xmlEntry.Type;

                var entryInfo = GetStructureEntryInfo(resultStructure.info, xmlEntry.NameHash);
                var name = reader.Name;

                var hash = GetHashForName(name);

                if (xmlEntry.NameHash != hash)
                    throw new Exception($"Expected:{xmlEntry.NameHash}, Current: {hash}({reader.Name})");

                switch (type)
                {
                    case StructureEntryDataType.StringLocal:
                        {
                            var metaValue = new MetaString(entryInfo);
                            metaValue.Value = null;

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadStartElement();
                                var content = reader.ReadContentAsString();
                                metaValue.Value = content;
                                reader.ReadEndElement();
                            }

                            resultStructure.Values.Add(xmlEntry.NameHash, metaValue);
                            break;
                        }
                    case StructureEntryDataType.StringHash:
                        {
                            var metaValue = new MetaStringHash();
                            metaValue.Value = 0;

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadStartElement();
                                var content = reader.ReadContentAsString();
                                metaValue.Value = GetHashForName(content);
                                reader.ReadEndElement();
                            }

                            resultStructure.Values.Add(xmlEntry.NameHash, metaValue);
                            break;
                        }
                    case StructureEntryDataType.StringPointer:
                        {
                            var metaValue = new MetaStringPointer();
                            metaValue.Value = null;

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadStartElement();
                                var content = reader.ReadContentAsString();
                                metaValue.Value = content;
                                reader.ReadEndElement();
                            }

                            resultStructure.Values.Add(xmlEntry.NameHash, metaValue);
                            break;
                        }
                    case StructureEntryDataType.Bool:
                        {
                            var metaValue = new MetaBool();
                            metaValue.Value = reader.GetAttributeValueAsBool();

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadEndElement();
                            }

                            resultStructure.Values.Add(xmlEntry.NameHash, metaValue);
                            break;
                        }
                    case StructureEntryDataType.UInt8:
                        {
                            var metaValue = new MetaByte();
                            metaValue.Value = reader.GetAttributeValueAsByte();

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadEndElement();
                            }

                            resultStructure.Values.Add(xmlEntry.NameHash, metaValue);
                            break;
                        }
                    case StructureEntryDataType.Int8:
                        {
                            var metaValue = new MetaSByte();
                            metaValue.Value = reader.GetAttributeValueAsSByte();

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadEndElement();
                            }

                            resultStructure.Values.Add(xmlEntry.NameHash, metaValue);
                            break;
                        }
                    case StructureEntryDataType.UInt16:
                        {
                            var metaValue = new MetaUInt16();
                            metaValue.Value = reader.GetAttributeValueAsUInt16();

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadEndElement();
                            }

                            resultStructure.Values.Add(xmlEntry.NameHash, metaValue);
                            break;
                        }
                    case StructureEntryDataType.Int16:
                        {
                            var metaValue = new MetaInt16();
                            metaValue.Value = reader.GetAttributeValueAsInt16();

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadEndElement();
                            }

                            resultStructure.Values.Add(xmlEntry.NameHash, metaValue);
                            break;
                        }
                    case StructureEntryDataType.UInt32:
                        {
                            var metaValue = new MetaUInt32();
                            metaValue.Value = reader.GetAttributeValueAsUInt32();

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadEndElement();
                            }

                            resultStructure.Values.Add(xmlEntry.NameHash, metaValue);
                            break;
                        }
                    case StructureEntryDataType.Int32:
                        {
                            var metaValue = new MetaInt32();
                            metaValue.Value = reader.GetAttributeValueAsInt32();

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadEndElement();
                            }

                            resultStructure.Values.Add(xmlEntry.NameHash, metaValue);
                            break;
                        }
                    case StructureEntryDataType.Float:
                        {
                            var metaValue = new MetaFloat();
                            metaValue.Value = reader.GetAttributeValueAsFloat();

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadEndElement();
                            }

                            resultStructure.Values.Add(xmlEntry.NameHash, metaValue);
                            break;
                        }
                    case StructureEntryDataType.Vector3:
                        {
                            var metaValue = new MetaVector3();
                            metaValue.Value = reader.GetAttributesXYZAsVector3();

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadEndElement();
                            }

                            resultStructure.Values.Add(xmlEntry.NameHash, metaValue);
                            break;
                        }
                    case StructureEntryDataType.Vector4:
                        {
                            var metaValue = new MetaVector4();
                            metaValue.Value = reader.GetAttributesXYZWAsVector4();

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadEndElement();
                            }

                            resultStructure.Values.Add(xmlEntry.NameHash, metaValue);
                            break;
                        }
                    case StructureEntryDataType.EnumInt8:
                        {
                            var metaValue = new MetaEnumInt8();

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                ReadEnumInt8(reader, metaValue, entryInfo.ReferenceKey);
                                reader.ReadEndElement();
                            }

                            resultStructure.Values.Add(xmlEntry.NameHash, metaValue);
                            break;
                        }
                    case StructureEntryDataType.EnumInt16:
                        {
                            var metaValue = new MetaEnumInt16();

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                ReadEnumInt16(reader, metaValue, entryInfo.ReferenceKey);
                                reader.ReadEndElement();
                            }

                            resultStructure.Values.Add(xmlEntry.NameHash, metaValue);
                            break;
                        }
                    case StructureEntryDataType.EnumInt32:
                        {
                            var metaValue = new MetaEnumInt32();

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                ReadEnumInt32(reader, metaValue, entryInfo.ReferenceKey);
                                reader.ReadEndElement();
                            }

                            resultStructure.Values.Add(xmlEntry.NameHash, metaValue);
                            break;
                        }
                    case StructureEntryDataType.FlagsInt8:
                        {
                            var metaValue = new MetaFlagsInt8();

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                ReadFlagsInt8(reader, metaValue, entryInfo.ReferenceKey);
                                reader.ReadEndElement();
                            }

                            resultStructure.Values.Add(xmlEntry.NameHash, metaValue);
                            break;
                        }
                    case StructureEntryDataType.FlagsInt16:
                        {
                            var metaValue = new MetaFlagsInt16();

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                ReadFlagsInt16(reader, metaValue, entryInfo.ReferenceKey);
                                reader.ReadEndElement();
                            }

                            resultStructure.Values.Add(xmlEntry.NameHash, metaValue);
                            break;
                        }
                    case StructureEntryDataType.FlagsInt32:
                        {
                            var metaValue = new MetaFlagsInt32();

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                ReadFlagsInt32(reader, metaValue, entryInfo.ReferenceKey);
                                reader.ReadEndElement();
                            }

                            resultStructure.Values.Add(xmlEntry.NameHash, metaValue);
                            break;
                        }
                    case StructureEntryDataType.Structure:
                        {
                            var xmlInfo = FindAndCheckStructure(xmlEntry.TypeHash);
                            var structureValue = ParseStructure(reader.ReadSubtree(), xmlInfo);
                            reader.ReadEndElement();
                            resultStructure.Values.Add(xmlEntry.NameHash, structureValue);
                            break;
                        }
                    case StructureEntryDataType.Array:
                        {
                            MetaArray metaValue = new MetaArray();
                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                var arrayType = (StructureEntryDataType)xmlEntry.ArrayType.Type;

                                switch (arrayType)
                                {
                                    case StructureEntryDataType.StructurePointer:
                                        metaValue.Entries = ReadArrayStructurePointer(reader.ReadSubtree());
                                        break;

                                    case StructureEntryDataType.Structure:
                                        metaValue.Entries = ReadArrayStructure(reader.ReadSubtree(), xmlEntry.ArrayType.TypeHash);
                                        break;

                                    case StructureEntryDataType.StringHash:
                                        metaValue.Entries = ReadArrayHash(reader.ReadSubtree());
                                        break;

                                    case StructureEntryDataType.UInt8:
                                        metaValue.Entries = ReadArrayUInt8(reader.ReadSubtree());
                                        break;

                                    case StructureEntryDataType.UInt16:
                                        metaValue.Entries = ReadArrayUInt16(reader.ReadSubtree());
                                        break;

                                    case StructureEntryDataType.UInt32:
                                        metaValue.Entries = ReadArrayUInt32(reader.ReadSubtree());
                                        break;

                                    case StructureEntryDataType.Float:
                                        metaValue.Entries = ReadArrayFloat(reader.ReadSubtree());
                                        break;

                                    case StructureEntryDataType.Vector3:
                                        metaValue.Entries = ReadArrayVector3(reader.ReadSubtree());
                                        break;

                                    default:
                                        throw new Exception($"Unsupported ArrayType: {arrayType} for Type: {type}");
                                }

                                reader.ReadEndElement();
                            }

                            metaValue.info = resultStructure.info.Entries[entryInfo.ReferenceTypeIndex];
                            resultStructure.Values.Add(xmlEntry.NameHash, metaValue);
                            break;
                        }
                    case StructureEntryDataType.ArrayLocal:
                        {
                            MetaArrayLocal metaValue = null;

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadStartElement();
                                var content = reader.ReadContentAsString();

                                var arrayType = (StructureEntryDataType)xmlEntry.ArrayType.Type;

                                switch (arrayType)
                                {

                                    case StructureEntryDataType.UInt8:
                                        metaValue = new MetaArrayLocal<byte>(entryInfo) { Value = StringParseHelpers.ParseItemsAsUInt8(content).ToArray() };
                                        break;

                                    case StructureEntryDataType.UInt16:
                                        metaValue = new MetaArrayLocal<ushort>(entryInfo) { Value = StringParseHelpers.ParseItemsAsUInt16(content).ToArray() };
                                        break;

                                    case StructureEntryDataType.UInt32:
                                        metaValue = new MetaArrayLocal<uint>(entryInfo) { Value = StringParseHelpers.ParseItemsAsUInt32(content).ToArray() };
                                        break;

                                    case StructureEntryDataType.Float:
                                        metaValue = new MetaArrayLocal<float>(entryInfo) { Value = StringParseHelpers.ParseItemsAsFloat(content).ToArray() };
                                        break;

                                    default:
                                        throw new Exception($"Unsupported ArrayType: {arrayType} for Type: {type}");
                                }

                                reader.ReadEndElement();
                            }

                            resultStructure.Values.Add(xmlEntry.NameHash, metaValue);
                            break;
                        }

                    case StructureEntryDataType.DataBlockPointer:
                        {
                            MetaDataBlockPointer metaValue = new MetaDataBlockPointer(entryInfo);
                            metaValue.Data = null;

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadStartElement();
                                var content = reader.ReadContentAsString();
                                metaValue.Data = StringParseHelpers.ParseItemsAsUInt8(content).ToArray();
                                reader.ReadEndElement();
                            }

                            resultStructure.Values.Add(xmlEntry.NameHash, metaValue);
                            break;
                        }                   
                    default: throw new Exception($"Unsupported DataType: {type}");
                }
            }

            return resultStructure;
        }

        public MetaStructure GetMetaStructure(MetaStructureXml info)
        {
            foreach (var structureInfo in strList)
                if (structureInfo.StructureKey == info.Key)
                    return new MetaStructure(null, structureInfo);
            return null;
        }

        private void MetaBuildStructureInfos(MetaInformationXml xmlInfo)
        {
            strList = new ResourceSimpleArray<StructureInfo>();
            foreach (var xmlStructureInfo in xmlInfo.Structures)
            {
                var structureInfo = new StructureInfo();
                structureInfo.StructureNameHash = xmlStructureInfo.NameHash;
                structureInfo.StructureKey = xmlStructureInfo.Key;
                structureInfo.Unknown_8h = xmlStructureInfo.Unknown;
                structureInfo.StructureLength = xmlStructureInfo.Length;
                structureInfo.Entries = new ResourceSimpleArray<StructureEntryInfo>();
                foreach (var xmlStructureEntryInfo in xmlStructureInfo.Entries)
                {
                    var xmlArrayTypeStack = new Stack<MetaStructureArrayTypeXml>();
                    var xmlArrayType = xmlStructureEntryInfo.ArrayType;
                    while (xmlArrayType != null)
                    {
                        xmlArrayTypeStack.Push(xmlArrayType);
                        xmlArrayType = xmlArrayType.ArrayType;
                    }

                    while (xmlArrayTypeStack.Count > 0)
                    {
                        xmlArrayType = xmlArrayTypeStack.Pop();
                        var arrayStructureEntryInfo = new StructureEntryInfo();
                        arrayStructureEntryInfo.EntryNameHash = 0x100;
                        arrayStructureEntryInfo.DataOffset = 0;
                        arrayStructureEntryInfo.DataType = (StructureEntryDataType)xmlArrayType.Type;
                        arrayStructureEntryInfo.Unknown_9h = 0;
                        if (arrayStructureEntryInfo.DataType == StructureEntryDataType.Array)
                        {
                            arrayStructureEntryInfo.ReferenceTypeIndex = (short)(structureInfo.Entries.Count - 1);
                        }
                        else
                        {
                            arrayStructureEntryInfo.ReferenceTypeIndex = 0;
                        }
                        arrayStructureEntryInfo.ReferenceKey = xmlArrayType.TypeHash;
                        structureInfo.Entries.Add(arrayStructureEntryInfo);
                    }

                    var structureEntryInfo = new StructureEntryInfo();
                    structureEntryInfo.EntryNameHash = xmlStructureEntryInfo.NameHash;
                    structureEntryInfo.DataOffset = xmlStructureEntryInfo.Offset;
                    structureEntryInfo.DataType = (StructureEntryDataType)xmlStructureEntryInfo.Type;
                    structureEntryInfo.Unknown_9h = (byte)xmlStructureEntryInfo.Unknown;
                    if (structureEntryInfo.DataType == StructureEntryDataType.Array)
                    {
                        structureEntryInfo.ReferenceTypeIndex = (short)(structureInfo.Entries.Count - 1);
                    }
                    else
                    {
                        structureEntryInfo.ReferenceTypeIndex = 0;
                    }
                    structureEntryInfo.ReferenceKey = xmlStructureEntryInfo.TypeHash;

                    structureInfo.Entries.Add(structureEntryInfo);
                }
                strList.Add(structureInfo);
            }
        }

        public int GetHashForName(ReadOnlySpan<char> hashName)
        {
            if (hashName.StartsWith("hash_", StringComparison.OrdinalIgnoreCase))
            {
                int intAgain = int.Parse(hashName.Slice(5), NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo);
                return intAgain;
            }
            else
            {
                return (int)Jenkins.Hash(hashName);
            }
        }
        
        public int GetHashForEnumName(ReadOnlySpan<char> hashName)
        {
            if (hashName.StartsWith("enum_hash_", StringComparison.OrdinalIgnoreCase))
            {
                int intAgain = int.Parse(hashName.Slice(10), NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo);
                return intAgain;
            }
            else
            {
                return (int)Jenkins.Hash(hashName);
            }
        }

        public int GetHashForFlagName(ReadOnlySpan<char> hashName)
        {
            if (hashName.StartsWith("flag_hash_", StringComparison.OrdinalIgnoreCase))
            {
                int intAgain = int.Parse(hashName.Slice(10), NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo);
                return intAgain;
            }
            else
            {
                return (int)Jenkins.Hash(hashName);
            }
        }

        private List<IMetaValue> ReadArrayStructurePointer(XmlReader reader)
        {
            var entries = new List<IMetaValue>();

            reader.MoveToContent();

            while (reader.Read())
            {
                if (!reader.IsStartElement())
                    continue;

                MetaGeneric pointer = new MetaGeneric();
                var type = reader.GetAttribute("type");
                if (!type.Equals("NULL"))
                {
                    var hash = GetHashForName(type);
                
                    var structure = FindAndCheckStructure(hash);
                    var metaValue = ParseStructure(reader.ReadSubtree(), structure);
                    pointer.Value = metaValue;
                }
                entries.Add(pointer);
            }

            return entries;
        }

        private List<IMetaValue> ReadArrayStructure(XmlReader reader, int structureNameHash)
        {
            var entries = new List<IMetaValue>();
            var structure = FindAndCheckStructure(structureNameHash);

            reader.MoveToContent();
            while (reader.Read())
            {
                if (!reader.IsStartElement())
                    continue;

                var metaValue = ParseStructure(reader, structure);
                entries.Add(metaValue);
            }

            return entries;
        }

        private List<IMetaValue> ReadArrayHash(XmlReader reader)
        {
            var entries = new List<IMetaValue>();

            reader.MoveToContent();

            while (reader.Read())
            {
                if (!reader.IsStartElement())
                    continue;

                var metaValue = new MetaStringHash();
                metaValue.Value = 0;

                if (reader.IsEmptyElement)
                {
                    reader.ReadStartElement();
                }
                else
                {
                    reader.ReadStartElement();
                    var content = reader.ReadContentAsString();
                    metaValue.Value = GetHashForName(content);
                }

                entries.Add(metaValue);
            }

            return entries;
        }

        private List<IMetaValue> ReadArrayFloat(XmlReader reader)
        {
            var entries = new List<IMetaValue>();

            reader.MoveToContent();

            if (reader.IsEmptyElement)
            {
                reader.ReadStartElement();
            }
            else
            {
                reader.ReadStartElement();
                var content = reader.ReadContentAsString();
                var items = StringParseHelpers.ParseItemsAsFloat(content);
                
                foreach (var item in items)
                {
                    entries.Add(new MetaFloat(item));
                }
            }
            return entries;
        }

        private List<IMetaValue> ReadArrayVector3(XmlReader reader)
        {
            var entries = new List<IMetaValue>();

            reader.MoveToContent();

            if (reader.IsEmptyElement)
            {
                reader.ReadStartElement();
            }
            else
            {
                reader.ReadStartElement();
                var content = reader.ReadContentAsString();
                var items = StringParseHelpers.ParseItemsAsFloat(content);

                Debug.Assert(items.Count % 3 == 0);

                for (int i = 0; i < items.Count; i += 3)
                {
                    entries.Add(new MetaVector3(items[i], items[i + 1], items[i + 2]));
                }
            }

            return entries;
        }

        private List<IMetaValue> ReadArrayUInt8(XmlReader reader)
        {
            var entries = new List<IMetaValue>();

            reader.MoveToContent();

            if (reader.IsEmptyElement)
            {
                reader.ReadStartElement();
            }
            else
            {
                reader.ReadStartElement();
                var content = reader.ReadContentAsString();
                var items = StringParseHelpers.ParseItemsAsUInt8(content);

                foreach (var item in items)
                {
                    entries.Add(new MetaByte(item));
                }
            }

            return entries;
        }

        private List<IMetaValue> ReadArrayUInt16(XmlReader reader)
        {
            var entries = new List<IMetaValue>();

            reader.MoveToContent();

            if (reader.IsEmptyElement)
            {
                reader.ReadStartElement();
            }
            else
            {
                reader.ReadStartElement();
                var content = reader.ReadContentAsString();
                var items = StringParseHelpers.ParseItemsAsUInt16(content);

                foreach (var item in items)
                {
                    entries.Add(new MetaUInt16(item));
                }
            }

            return entries;
        }

        private List<IMetaValue> ReadArrayUInt32(XmlReader reader)
        {
            var entries = new List<IMetaValue>();

            reader.MoveToContent();

            if (reader.IsEmptyElement)
            {
                reader.ReadStartElement();
            }
            else
            {
                reader.ReadStartElement();
                var content = reader.ReadContentAsString();
                var items = StringParseHelpers.ParseItemsAsUInt32(content);

                foreach (var item in items)
                {
                    entries.Add(new MetaUInt32(item));
                }
            }

            return entries;
        }

        private void ReadEnumInt32(XmlReader reader, MetaEnumInt32 metaEnum, int enumNameHash)
        {
            reader.MoveToContent();

            if (reader.IsEmptyElement)
            {
                reader.ReadStartElement();
            }
            else
            {
                reader.ReadStartElement();
                var content = reader.ReadContentAsString();
                if (content.Equals("enum_NONE"))
                {
                    metaEnum.Value = -1;
                }
                else
                {
                    var enumKey = GetHashForEnumName(content);
                    var enumInfo = (MetaEnumXml)null;
                    foreach (var x in xmlInfos.Enums)
                    {
                        if (x.NameHash == enumNameHash)
                            enumInfo = x;
                    }
                    foreach (var x in enumInfo.Entries)
                    {
                        if (x.NameHash == enumKey)
                            metaEnum.Value = x.Value;
                    }
                }          
            }
        }

        private void ReadEnumInt16(XmlReader reader, MetaEnumInt16 metaEnum, int enumNameHash)
        {
            reader.MoveToContent();

            if (reader.IsEmptyElement)
            {
                reader.ReadStartElement();
            }
            else
            {
                reader.ReadStartElement();
                var content = reader.ReadContentAsString();
                if (content.Equals("enum_NONE"))
                {
                    metaEnum.Value = -1;
                }
                else
                {
                    var enumKey = GetHashForEnumName(content);
                    var enumInfo = (MetaEnumXml)null;
                    foreach (var x in xmlInfos.Enums)
                    {
                        if (x.NameHash == enumNameHash)
                            enumInfo = x;
                    }
                    foreach (var x in enumInfo.Entries)
                    {
                        if (x.NameHash == enumKey)
                            metaEnum.Value = (short)x.Value;
                    }
                }
            }
        }

        private void ReadEnumInt8(XmlReader reader, MetaEnumInt8 metaEnum, int enumNameHash)
        {
            reader.MoveToContent();

            if (reader.IsEmptyElement)
            {
                reader.ReadStartElement();
            }
            else
            {
                reader.ReadStartElement();
                var content = reader.ReadContentAsString();
                if (content.Equals("enum_NONE"))
                {
                    metaEnum.Value = -1;
                }
                else
                {
                    var enumKey = GetHashForEnumName(content);
                    var enumInfo = (MetaEnumXml)null;
                    foreach (var x in xmlInfos.Enums)
                    {
                        if (x.NameHash == enumNameHash)
                            enumInfo = x;
                    }
                    foreach (var x in enumInfo.Entries)
                    {
                        if (x.NameHash == enumKey)
                            metaEnum.Value = (sbyte)x.Value;
                    }
                }
            }
        }

        private void ReadFlagsInt8(XmlReader reader, MetaFlagsInt8 metaFlags, int enumNameHash)
        {
            reader.MoveToContent();

            if (reader.IsEmptyElement)
            {
                reader.ReadStartElement();
            }
            else
            {
                reader.ReadStartElement();
                var content = reader.ReadContentAsString();

                var enumInfo = (MetaEnumXml)null;
                foreach (var x in xmlInfos.Enums)
                {
                    if (x.NameHash == enumNameHash)
                        enumInfo = x;
                }

                var items = new SpanTokenizer(content);
                foreach (var item in items)
                {
                    var enumKey = GetHashForFlagName(item.ToString());
                    foreach (var p in enumInfo.Entries)
                    {
                        if (p.NameHash == enumKey)
                            metaFlags.Value += (uint)(1 << p.Value);
                    }
                }
            }
        }

        private void ReadFlagsInt16(XmlReader reader, MetaFlagsInt16 metaFlags, int enumNameHash)
        {
            reader.MoveToContent();

            if (reader.IsEmptyElement)
            {
                reader.ReadStartElement();
            }
            else
            {
                reader.ReadStartElement();
                var content = reader.ReadContentAsString();

                var enumInfo = (MetaEnumXml)null;
                foreach (var x in xmlInfos.Enums)
                {
                    if (x.NameHash == enumNameHash)
                        enumInfo = x;
                }

                var items = new SpanTokenizer(content);
                foreach (var item in items)
                {
                    var enumKey = GetHashForFlagName(item.ToString());
                    foreach (var p in enumInfo.Entries)
                    {
                        if (p.NameHash == enumKey)
                            metaFlags.Value += (ushort)(1 << p.Value);
                    }
                }
            }
        }

        private void ReadFlagsInt32(XmlReader reader, MetaFlagsInt32 metaFlags, int enumNameHash)
        {
            reader.MoveToContent();

            if (reader.IsEmptyElement)
            {
                reader.ReadStartElement();
            }
            else
            {
                reader.ReadStartElement();
                var content = reader.ReadContentAsString();
                var items = new SpanTokenizer(content);

                // BITSET ?
                if (enumNameHash == 0)
                {
                    foreach (var item in items)
                    {
                        var enumIdx = int.Parse(item.Slice(11), NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
                        metaFlags.Value += (uint)(1 << enumIdx);
                    }
                    return;
                }

                var enumInfo = (MetaEnumXml)null;
                foreach (var x in xmlInfos.Enums)
                {
                    if (x.NameHash == enumNameHash)
                        enumInfo = x;
                }

                foreach (var item in items)
                {
                    var enumKey = GetHashForFlagName(item.ToString());
                    foreach (var p in enumInfo.Entries)
                    {
                        if (p.NameHash == enumKey)
                            metaFlags.Value += (ushort)(1 << p.Value);
                    }
                }
            }
        }
    }
}
