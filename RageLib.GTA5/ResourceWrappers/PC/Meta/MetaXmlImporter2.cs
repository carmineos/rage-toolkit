// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.GTA5.ResourceWrappers.PC.Meta.Definitions;
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
using System.Xml;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta
{
    public class MetaXmlImporter2
    {
        // TODO: do we need both definitions and structureInfos?? probably not ...
        private MetaDefinitions definitions;
        private ResourceSimpleArray<StructureInfo> structureInfos;

        public MetaStructure Import(string xmlFileName)
        {
            using (var xmlFileStream = new FileStream(xmlFileName, FileMode.Open, FileAccess.Read))
            {
                return Import(xmlFileStream);
            }
        }

        public MetaXmlImporter2(MetaDefinitions xmlinfos)
        {
            this.definitions = xmlinfos;
            this.structureInfos = xmlinfos.BuildMetaStructureInfos();
        }

        public MetaStructure Import(Stream xmlFileStream)
        {
            
            var reader = XmlReader.Create(xmlFileStream, new XmlReaderSettings() { IgnoreWhitespace = true });

            // Skip declaration
            reader.MoveToContent();

            var rootInfo = GetMetaStructureXml(GetHashForName(reader.Name));

            var res = ParseStructure(reader, rootInfo);
            return res;
        }

        // TODO:    Edit MetaInformationXml to store a dictionary for fast lookup by hash
        //          do we need to check the structure??
        public MetaStructureXml GetMetaStructureXml(int hash)
        {
            foreach (var structureXml in definitions.Structures)
                if (structureXml.NameHash == hash)
                    return structureXml;
            return null;
        }

        public MetaEnumXml GetMetaEnumXml(int hash)
        {
            foreach (var enumXml in definitions.Enums)
                if (enumXml.NameHash == hash)
                    return enumXml;
            return null;
        }

        public StructureEntryInfo GetStructureEntryInfo(StructureInfo structureInfo, int entryInfoNameHash)
        {
            foreach (var entryInfo in structureInfo.Entries)
                if (entryInfo.EntryNameHash == entryInfoNameHash)
                    return entryInfo;

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
                var name = reader.Name;
                var hash = GetHashForName(name);
                
                var xmlEntry = info.Entries[i];
                var entryInfo = GetStructureEntryInfo(resultStructure.info, xmlEntry.NameHash);
                
                if (xmlEntry.NameHash != hash)
                    throw new Exception($"Expected:{xmlEntry.NameHash}, Current: {hash}({reader.Name})");
                
                var type = (StructureEntryDataType)xmlEntry.Type;

                IMetaValue metaValue = null;

                switch (type)
                {
                    case StructureEntryDataType.StringLocal:
                        {
                            metaValue = new MetaString(entryInfo) { Value = null };

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadStartElement();
                                var content = reader.ReadContentAsString();
                                (metaValue as MetaString).Value = content;
                                reader.ReadEndElement();
                            }

                            break;
                        }
                    case StructureEntryDataType.StringHash:
                        {
                            metaValue = new MetaStringHash() { Value = 0 };

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadStartElement();
                                var content = reader.ReadContentAsString();
                                (metaValue as MetaStringHash).Value = GetHashForName(content);
                                reader.ReadEndElement();
                            }

                            break;
                        }
                    case StructureEntryDataType.StringPointer:
                        {
                            metaValue = new MetaStringPointer() { Value = null };

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadStartElement();
                                var content = reader.ReadContentAsString();
                                (metaValue as MetaStringPointer).Value = content;
                                reader.ReadEndElement();
                            }

                            break;
                        }
                    case StructureEntryDataType.Bool:
                        {
                            metaValue = new MetaBool() { Value = reader.GetAttributeValueAsBool() };

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadEndElement();
                            }

                            break;
                        }
                    case StructureEntryDataType.UInt8:
                        {
                            metaValue = new MetaByte() { Value= reader.GetAttributeValueAsByte() };

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadEndElement();
                            }

                            break;
                        }
                    case StructureEntryDataType.Int8:
                        {
                            metaValue = new MetaSByte() { Value = reader.GetAttributeValueAsSByte() };

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadEndElement();
                            }

                            break;
                        }
                    case StructureEntryDataType.UInt16:
                        {
                            metaValue = new MetaUInt16() { Value = reader.GetAttributeValueAsUInt16() };

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadEndElement();
                            }

                            break;
                        }
                    case StructureEntryDataType.Int16:
                        {
                            metaValue = new MetaInt16() { Value = reader.GetAttributeValueAsInt16() };

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadEndElement();
                            }

                            break;
                        }
                    case StructureEntryDataType.UInt32:
                        {
                            metaValue = new MetaUInt32() { Value = reader.GetAttributeValueAsUInt32() };

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadEndElement();
                            }

                            break;
                        }
                    case StructureEntryDataType.Int32:
                        {
                            metaValue = new MetaInt32() { Value = reader.GetAttributeValueAsInt32() };

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadEndElement();
                            }

                            break;
                        }
                    case StructureEntryDataType.Float:
                        {
                            metaValue = new MetaFloat() { Value = reader.GetAttributeValueAsFloat() };

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadEndElement();
                            }

                            break;
                        }
                    case StructureEntryDataType.Vector3:
                        {
                            metaValue = new MetaVector3() { Value = reader.GetAttributesXYZAsVector3() };

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadEndElement();
                            }

                            break;
                        }
                    case StructureEntryDataType.Vector4:
                        {
                            metaValue = new MetaVector4() { Value = reader.GetAttributesXYZWAsVector4() };

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadEndElement();
                            }

                            break;
                        }
                    case StructureEntryDataType.EnumInt8:
                        {
                            metaValue = new MetaEnumInt8();

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                (metaValue as MetaEnumInt8).Value = ReadEnumInt8(reader, entryInfo.ReferenceKey);
                                reader.ReadEndElement();
                            }

                            break;
                        }
                    case StructureEntryDataType.EnumInt16:
                        {
                            metaValue = new MetaEnumInt16();

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                (metaValue as MetaEnumInt16).Value = ReadEnumInt16(reader, entryInfo.ReferenceKey);
                                reader.ReadEndElement();
                            }

                            break;
                        }
                    case StructureEntryDataType.EnumInt32:
                        {
                            metaValue = new MetaEnumInt32();

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                (metaValue as MetaEnumInt32).Value = ReadEnumInt32(reader, entryInfo.ReferenceKey);
                                reader.ReadEndElement();
                            }

                            break;
                        }
                    case StructureEntryDataType.FlagsInt8:
                        {
                            metaValue = new MetaFlagsInt8();

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                (metaValue as MetaFlagsInt8).Value = ReadFlagsUInt8(reader, entryInfo.ReferenceKey);
                                reader.ReadEndElement();
                            }

                            break;
                        }
                    case StructureEntryDataType.FlagsInt16:
                        {
                            metaValue = new MetaFlagsInt16();

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                (metaValue as MetaFlagsInt16).Value = ReadFlagsUInt16(reader, entryInfo.ReferenceKey);
                                reader.ReadEndElement();
                            }

                            break;
                        }
                    case StructureEntryDataType.FlagsInt32:
                        {
                            metaValue = new MetaFlagsInt32();

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                (metaValue as MetaFlagsInt32).Value = ReadFlagsUInt32(reader, entryInfo.ReferenceKey);
                                reader.ReadEndElement();
                            }

                            break;
                        }
                    case StructureEntryDataType.Structure:
                        {
                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                var xmlInfo = GetMetaStructureXml(xmlEntry.TypeHash);
                                metaValue = ParseStructure(reader.ReadSubtree(), xmlInfo);
                                reader.ReadEndElement();
                            }

                            break;
                        }
                    case StructureEntryDataType.Array:
                        {
                            metaValue = new MetaArray() { info = resultStructure.info.Entries[entryInfo.ReferenceTypeIndex] };

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
                                        (metaValue as MetaArray).Entries = ReadArrayStructurePointer(reader.ReadSubtree());
                                        break;

                                    case StructureEntryDataType.Structure:
                                        (metaValue as MetaArray).Entries = ReadArrayStructure(reader.ReadSubtree(), xmlEntry.ArrayType.TypeHash);
                                        break;

                                    case StructureEntryDataType.StringHash:
                                        (metaValue as MetaArray).Entries = ReadArrayHash(reader.ReadSubtree());
                                        break;

                                    case StructureEntryDataType.UInt8:
                                        (metaValue as MetaArray).Entries = ReadArrayUInt8(reader.ReadSubtree());
                                        break;

                                    case StructureEntryDataType.UInt16:
                                        (metaValue as MetaArray).Entries = ReadArrayUInt16(reader.ReadSubtree());
                                        break;

                                    case StructureEntryDataType.UInt32:
                                        (metaValue as MetaArray).Entries = ReadArrayUInt32(reader.ReadSubtree());
                                        break;

                                    case StructureEntryDataType.Float:
                                        (metaValue as MetaArray).Entries = ReadArrayFloat(reader.ReadSubtree());
                                        break;

                                    case StructureEntryDataType.Vector3:
                                        (metaValue as MetaArray).Entries = ReadArrayVector3(reader.ReadSubtree());
                                        break;

                                    default:
                                        throw new Exception($"Unsupported ArrayType: {arrayType} for Type: {type}");
                                }

                                reader.ReadEndElement();
                            }

                            break;
                        }
                    case StructureEntryDataType.ArrayLocal:
                        {
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

                            break;
                        }

                    case StructureEntryDataType.DataBlockPointer:
                        {
                            metaValue = new MetaDataBlockPointer(entryInfo) { Data = null };

                            if (reader.IsEmptyElement)
                            {
                                reader.ReadStartElement();
                            }
                            else
                            {
                                reader.ReadStartElement();
                                var content = reader.ReadContentAsString();
                                (metaValue as MetaDataBlockPointer).Data = StringParseHelpers.ParseItemsAsUInt8(content).ToArray();
                                reader.ReadEndElement();
                            }

                            break;
                        }                   
                    default: throw new Exception($"Unsupported DataType: {type}");
                }

                resultStructure.Values.Add(xmlEntry.NameHash, metaValue);
            }

            return resultStructure;
        }

        public MetaStructure GetMetaStructure(MetaStructureXml info)
        {
            foreach (var structureInfo in structureInfos)
                if (structureInfo.StructureKey == info.Key)
                    return new MetaStructure(null, structureInfo);
            return null;
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
                
                    var structure = GetMetaStructureXml(hash);
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
            var structure = GetMetaStructureXml(structureNameHash);

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

        private int ReadEnumInt32(XmlReader reader, int enumNameHash)
        {
            int value = -1;
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
                    return -1;

                var enumKey = GetHashForEnumName(content);
                var enumInfo = GetMetaEnumXml(enumNameHash);

                foreach (var x in enumInfo.Entries)
                    if (x.NameHash == enumKey)
                        return x.Value;
            }
            return value;
        }

        private short ReadEnumInt16(XmlReader reader, int enumNameHash)
        {
            short value = -1;
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
                    return -1;

                var enumKey = GetHashForEnumName(content);
                var enumInfo = GetMetaEnumXml(enumNameHash);

                foreach (var x in enumInfo.Entries)
                    if (x.NameHash == enumKey)
                        return (short)x.Value;
            }
            return value;
        }

        private sbyte ReadEnumInt8(XmlReader reader, int enumNameHash)
        {
            sbyte value = -1;
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
                    return -1;

                var enumKey = GetHashForEnumName(content);
                var enumInfo = GetMetaEnumXml(enumNameHash);

                foreach (var x in enumInfo.Entries)
                    if (x.NameHash == enumKey)
                        return (sbyte)x.Value;
            }
            return value;
        }

        private uint ReadFlagsUInt8(XmlReader reader, int enumNameHash)
        {
            uint value = 0;
            reader.MoveToContent();

            if (reader.IsEmptyElement)
            {
                reader.ReadStartElement();
            }
            else
            {
                reader.ReadStartElement();
                var content = reader.ReadContentAsString();

                var enumInfo = GetMetaEnumXml(enumNameHash);

                var items = new SpanTokenizer(content);
                foreach (var item in items)
                {
                    var enumKey = GetHashForFlagName(item.ToString());
                    foreach (var p in enumInfo.Entries)
                    {
                        if (p.NameHash == enumKey)
                            value += (uint)(1 << p.Value);
                    }
                }
            }
            return value;
        }

        private ushort ReadFlagsUInt16(XmlReader reader, int enumNameHash)
        {
            ushort value = 0;
            reader.MoveToContent();

            if (reader.IsEmptyElement)
            {
                reader.ReadStartElement();
            }
            else
            {
                reader.ReadStartElement();
                var content = reader.ReadContentAsString();

                var enumInfo = GetMetaEnumXml(enumNameHash);

                var items = new SpanTokenizer(content);
                foreach (var item in items)
                {
                    var enumKey = GetHashForFlagName(item.ToString());
                    foreach (var p in enumInfo.Entries)
                    {
                        if (p.NameHash == enumKey)
                            value += (ushort)(1 << p.Value);
                    }
                }
            }
            return value;
        }

        private uint ReadFlagsUInt32(XmlReader reader, int enumNameHash)
        {
            uint value = 0;
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
                        value += (uint)(1 << enumIdx);
                    }
                    return value;
                }

                var enumInfo = GetMetaEnumXml(enumNameHash);

                foreach (var item in items)
                {
                    var enumKey = GetHashForFlagName(item.ToString());
                    foreach (var p in enumInfo.Entries)
                    {
                        if (p.NameHash == enumKey)
                            value += (ushort)(1 << p.Value);
                    }
                }
            }

            return value;
        }
    }
}
