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
                    case StructureEntryDataType.EnumInt32:
                        {
                            var metaValue = new MetaEnumInt32();

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                ReadIntEnum(reader, metaValue, entryInfo.ReferenceKey);
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
                                        ReadStructurePointerArray(reader.ReadSubtree(), metaValue);
                                        reader.ReadEndElement();
                                        break;

                                    case StructureEntryDataType.Structure:
                                        ReadStructureArray(reader.ReadSubtree(), metaValue, xmlEntry.ArrayType.TypeHash);
                                        reader.ReadEndElement();
                                        break;

                                    case StructureEntryDataType.StringHash:
                                        ReadHashArray(reader.ReadSubtree(), metaValue);
                                        reader.ReadEndElement();
                                        break;

                                    case StructureEntryDataType.Float:
                                        ReadFloatArray(reader.ReadSubtree(), metaValue);
                                        reader.ReadEndElement();
                                        break;

                                    case StructureEntryDataType.UInt32:
                                        ReadIntArray(reader.ReadSubtree(), metaValue);
                                        reader.ReadEndElement();
                                        break;

                                    case StructureEntryDataType.Vector3:
                                        ReadVector3Array(reader.ReadSubtree(), metaValue);
                                        reader.ReadEndElement();
                                        break;
                                    default:
                                        throw new Exception($"Unsupported ArrayType: {arrayType} for Type: {type}");
                                }
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
                                        metaValue = new MetaArrayLocal<byte>(entryInfo);
                                        ((MetaArrayLocal<byte>)metaValue).Value = StringParseHelpers.ParseItemsAsUInt8(content).ToArray();
                                        break;
                                    case StructureEntryDataType.UInt16:
                                        metaValue = new MetaArrayLocal<ushort>(entryInfo);
                                        ((MetaArrayLocal<ushort>)metaValue).Value = StringParseHelpers.ParseItemsAsUInt16(content).ToArray();
                                        break;
                                    case StructureEntryDataType.UInt32:
                                        metaValue = new MetaArrayLocal<uint>(entryInfo);
                                        ((MetaArrayLocal<uint>)metaValue).Value = StringParseHelpers.ParseItemsAsUInt32(content).ToArray();
                                        break;
                                    case StructureEntryDataType.Float:
                                        metaValue = new MetaArrayLocal<float>(entryInfo);
                                        ((MetaArrayLocal<float>)metaValue).Value = StringParseHelpers.ParseItemsAsFloat(content).ToArray();
                                        break;

                                    default:
                                        throw new Exception($"Unsupported ArrayType: {arrayType} for Type: {type}");
                                }

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

        public int GetHashForName(string hashName)
        {
            if (hashName.StartsWith("hash_", StringComparison.OrdinalIgnoreCase))
            {
                int intAgain = int.Parse(hashName.AsSpan(5), NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo);
                return intAgain;
            }
            else
            {
                return (int)Jenkins.Hash(hashName);
            }
        }
        
        public int GetHashForEnumName(string hashName)
        {
            if (hashName.StartsWith("enum_hash_", StringComparison.OrdinalIgnoreCase))
            {
                int intAgain = int.Parse(hashName.AsSpan(10), NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo);
                return intAgain;
            }
            else
            {
                return (int)Jenkins.Hash(hashName);
            }
        }

        public int GetHashForFlagName(string hashName)
        {
            if (hashName.StartsWith("flag_hash_", StringComparison.OrdinalIgnoreCase))
            {
                int intAgain = int.Parse(hashName.AsSpan(10), NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo);
                return intAgain;
            }
            else
            {
                return (int)Jenkins.Hash(hashName);
            }
        }

        private void ReadStructurePointerArray(XmlReader reader, MetaArray array)
        {
            array.Entries = new List<IMetaValue>();

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
                array.Entries.Add(pointer);
            }
        }

        private void ReadStructureArray(XmlReader reader, MetaArray array, int structureNameHash)
        {
            array.Entries = new List<IMetaValue>();
            var structure = FindAndCheckStructure(structureNameHash);

            reader.MoveToContent();
            while (reader.Read())
            {
                if (!reader.IsStartElement())
                    continue;

                var metaValue = ParseStructure(reader, structure);
                array.Entries.Add(metaValue);
            }
        }

        private void ReadHashArray(XmlReader reader, MetaArray array)
        {
            array.Entries = new List<IMetaValue>();

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

                array.Entries.Add(metaValue);
            }
        }

        private void ReadFloatArray(XmlReader reader, MetaArray array)
        {
            array.Entries = new List<IMetaValue>();

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
                    array.Entries.Add(new MetaFloat(item));
                }
            }
        }

        private void ReadVector3Array(XmlReader reader, MetaArray array)
        {
            array.Entries = new List<IMetaValue>();

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
                    array.Entries.Add(new MetaVector3(items[i], items[i + 1], items[i + 2]));
                }
            }
        }

        private void ReadIntArray(XmlReader reader, MetaArray array)
        {
            array.Entries = new List<IMetaValue>();

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
                    array.Entries.Add(new MetaUInt32(item));
                }
            }
        }

        private void ReadIntEnum(XmlReader reader, MetaEnumInt32 metaEnum, int enumNameHash)
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
    }
}
