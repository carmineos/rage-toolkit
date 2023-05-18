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
using System.Numerics;
using System.Xml;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta
{
    public class MetaXmlImporter
    {
        private MetaDefinitions xmlInfos;
        private ResourceSimpleArray<StructureInfo> strList;

        public MetaStructure Import(string xmlFileName)
        {
            using (var xmlFileStream = new FileStream(xmlFileName, FileMode.Open, FileAccess.Read))
            {
                return Import(xmlFileStream);
            }
        }

        public MetaXmlImporter(MetaDefinitions xmlinfos)
        {
            this.xmlInfos = xmlinfos;
            MetaBuildStructureInfos(xmlinfos);
        }

        public MetaStructure Import(Stream xmlFileStream)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFileStream);
            var rootOfData = xmlDoc.LastChild;
            
            var rootInfo = FindAndCheckStructure(rootOfData);

            var res = ParseStructure(rootOfData, rootInfo);
            return res;
        }

        public MetaStructure ParseStructure(XmlNode node, MetaStructureXml info)
        {
            MetaStructure resultStructure = null;
            foreach (var x in strList)
                if (x.StructureKey == info.Key)
                    resultStructure = new MetaStructure(null, x);
            resultStructure.Values = new Dictionary<int, IMetaValue>();

            foreach (var xmlEntry in info.Entries)
            {
                XmlNode xmlNode = null;
                foreach (XmlNode x in node.ChildNodes)
                {
                    var hash = GetHashForName(x.Name);
                    if (hash == xmlEntry.NameHash)
                        xmlNode = x;
                }

                StructureEntryInfo entryInfo = null;
                foreach (var x in resultStructure.info.Entries)
                    if (x.EntryNameHash == xmlEntry.NameHash)
                        entryInfo = x;

                var type = (StructureEntryDataType)xmlEntry.Type;
                switch (type)
                {
                    case StructureEntryDataType.Array:
                        {

                            var arrayType = (StructureEntryDataType)xmlEntry.ArrayType.Type;
                            switch (arrayType)
                            {
                                case StructureEntryDataType.StructurePointer:
                                    {
                                        MetaArray arrayValue = ReadPointerArray(xmlNode);
                                        arrayValue.info = resultStructure.info.Entries[entryInfo.ReferenceTypeIndex];
                                        resultStructure.Values.Add(xmlEntry.NameHash, arrayValue);
                                        break;
                                    }

                                case StructureEntryDataType.Structure:
                                    {
                                        MetaArray arryVal = ReadStructureArray(xmlNode, xmlEntry.ArrayType.TypeHash);
                                        arryVal.info = resultStructure.info.Entries[entryInfo.ReferenceTypeIndex];
                                        resultStructure.Values.Add(xmlEntry.NameHash, arryVal);
                                        break;
                                    }

                                case StructureEntryDataType.UInt8:
                                    {
                                        MetaArray arryVal = ReadByteArray(xmlNode);
                                        arryVal.info = resultStructure.info.Entries[entryInfo.ReferenceTypeIndex];
                                        resultStructure.Values.Add(xmlEntry.NameHash, arryVal);
                                        break;
                                    }

                                case StructureEntryDataType.UInt16:
                                    {
                                        MetaArray arryVal = ReadShortArray(xmlNode);
                                        arryVal.info = resultStructure.info.Entries[entryInfo.ReferenceTypeIndex];
                                        resultStructure.Values.Add(xmlEntry.NameHash, arryVal);
                                        break;
                                    }

                                case StructureEntryDataType.UInt32:
                                    {
                                        MetaArray arryVal = ReadIntArray(xmlNode);
                                        arryVal.info = resultStructure.info.Entries[entryInfo.ReferenceTypeIndex];
                                        resultStructure.Values.Add(xmlEntry.NameHash, arryVal);
                                        break;
                                    }

                                case StructureEntryDataType.Float:
                                    {
                                        MetaArray arryVal = ReadFloatArray(xmlNode);
                                        arryVal.info = resultStructure.info.Entries[entryInfo.ReferenceTypeIndex];
                                        resultStructure.Values.Add(xmlEntry.NameHash, arryVal);
                                        break;
                                    }

                                case StructureEntryDataType.Vector3:
                                    {
                                        MetaArray arryVal = ReadFloatVectorArray(xmlNode);
                                        arryVal.info = resultStructure.info.Entries[entryInfo.ReferenceTypeIndex];
                                        resultStructure.Values.Add(xmlEntry.NameHash, arryVal);
                                        break;
                                    }

                                case StructureEntryDataType.StringHash:
                                    {
                                        MetaArray arryVal = ReadHashArray(xmlNode);
                                        arryVal.info = resultStructure.info.Entries[entryInfo.ReferenceTypeIndex];
                                        resultStructure.Values.Add(xmlEntry.NameHash, arryVal);
                                        break;
                                    }
                            }

                            break;
                        }

                    case StructureEntryDataType.Bool:
                        resultStructure.Values.Add(xmlEntry.NameHash, ReadBoolean(xmlNode));
                        break;
                    case StructureEntryDataType.Int8:
                        resultStructure.Values.Add(xmlEntry.NameHash, ReadSignedByte(xmlNode));
                        break;
                    case StructureEntryDataType.UInt8:
                        resultStructure.Values.Add(xmlEntry.NameHash, ReadUnsignedByte(xmlNode));
                        break;
                    case StructureEntryDataType.Int16:
                        resultStructure.Values.Add(xmlEntry.NameHash, ReadSignedShort(xmlNode));
                        break;
                    case StructureEntryDataType.UInt16:
                        resultStructure.Values.Add(xmlEntry.NameHash, ReadUnsignedShort(xmlNode));
                        break;
                    case StructureEntryDataType.Int32:
                        resultStructure.Values.Add(xmlEntry.NameHash, ReadSignedInt(xmlNode));
                        break;
                    case StructureEntryDataType.UInt32:
                        resultStructure.Values.Add(xmlEntry.NameHash, ReadUnsignedInt(xmlNode));
                        break;
                    case StructureEntryDataType.Float:
                        resultStructure.Values.Add(xmlEntry.NameHash, ReadFloat(xmlNode));
                        break;
                    case StructureEntryDataType.Vector3:
                        resultStructure.Values.Add(xmlEntry.NameHash, ReadFloatXYZ(xmlNode));
                        break;
                    case StructureEntryDataType.Vector4:
                        resultStructure.Values.Add(xmlEntry.NameHash, ReadFloatXYZW(xmlNode));
                        break;
                    case StructureEntryDataType.EnumInt8:
                        resultStructure.Values.Add(xmlEntry.NameHash, ReadByteEnum(xmlNode, entryInfo.ReferenceKey));
                        break;
                    case StructureEntryDataType.EnumInt16:
                        resultStructure.Values.Add(xmlEntry.NameHash, ReadShortEnum(xmlNode, entryInfo.ReferenceKey));
                        break;
                    case StructureEntryDataType.EnumInt32:
                        resultStructure.Values.Add(xmlEntry.NameHash, ReadIntEnum(xmlNode, entryInfo.ReferenceKey));
                        break;
                    case StructureEntryDataType.FlagsInt8:
                        resultStructure.Values.Add(xmlEntry.NameHash, ReadIntFlags1(xmlNode, entryInfo.ReferenceKey));
                        break;
                    case StructureEntryDataType.FlagsInt16:
                        resultStructure.Values.Add(xmlEntry.NameHash, ReadShortFlags(xmlNode, entryInfo.ReferenceKey));
                        break;
                    case StructureEntryDataType.FlagsInt32:
                        resultStructure.Values.Add(xmlEntry.NameHash, ReadIntFlags2(xmlNode, entryInfo.ReferenceKey));
                        break;

                    case StructureEntryDataType.ArrayLocal:
                        {
                            MetaArrayLocal entryValue;

                            var arrayType = (StructureEntryDataType)xmlEntry.ArrayType.Type;
                            switch (arrayType)
                            {
                                case StructureEntryDataType.UInt8:
                                    entryValue = new MetaArrayLocal<byte>(entryInfo);
                                    ((MetaArrayLocal<byte>)entryValue).Value = StringParseHelpers.ParseItems<byte>(xmlNode.InnerText).ToArray();
                                    break;
                                case StructureEntryDataType.UInt16:
                                    entryValue = new MetaArrayLocal<ushort>(entryInfo);
                                    ((MetaArrayLocal<ushort>)entryValue).Value = StringParseHelpers.ParseItems<ushort>(xmlNode.InnerText).ToArray();
                                    break;
                                case StructureEntryDataType.UInt32:
                                    entryValue = new MetaArrayLocal<uint>(entryInfo);
                                    ((MetaArrayLocal<uint>)entryValue).Value = StringParseHelpers.ParseItems<uint>(xmlNode.InnerText).ToArray();
                                    break;
                                case StructureEntryDataType.Float:
                                    entryValue = new MetaArrayLocal<float>(entryInfo);
                                    ((MetaArrayLocal<float>)entryValue).Value = StringParseHelpers.ParseItems<float>(xmlNode.InnerText).ToArray();
                                    break;
                                default:
                                    throw new Exception($"Unsupported ArrayType: {arrayType} for Type: {type}");
                            }

                            resultStructure.Values.Add(xmlEntry.NameHash, entryValue);
                            break;
                        }

                    case StructureEntryDataType.StringLocal:
                        {
                            var charArrayValue = new MetaString(entryInfo);
                            charArrayValue.Value = xmlNode.InnerText;
                            resultStructure.Values.Add(xmlEntry.NameHash, charArrayValue);
                            break;
                        }
                    case StructureEntryDataType.StringPointer:
                        {
                            var charPointerValue = new MetaStringPointer();
                            charPointerValue.Value = xmlNode.InnerText;
                            if (charPointerValue.Value.Equals(""))
                                charPointerValue.Value = null;
                            resultStructure.Values.Add(xmlEntry.NameHash, charPointerValue);
                            break;
                        }
                    case StructureEntryDataType.StringHash:
                        {
                            var hashValue = new MetaStringHash();
                            if (xmlNode.InnerText.Trim().Length > 0)
                            {
                                hashValue.Value = GetHashForName(xmlNode.InnerText);
                            }
                            resultStructure.Values.Add(xmlEntry.NameHash, hashValue);
                            break;
                        }
                    case StructureEntryDataType.DataBlockPointer:
                        {
                            var dataBlockValue = new MetaDataBlockPointer(entryInfo);
                            dataBlockValue.Data = StringParseHelpers.ParseItems<byte>(xmlNode.InnerText).ToArray();
                            if (dataBlockValue.Data.Length == 0)
                                dataBlockValue.Data = null;
                            resultStructure.Values.Add(xmlEntry.NameHash, dataBlockValue);
                            break;
                        }

                    case StructureEntryDataType.Structure:
                        {
                            var xmlInfo = FindAndCheckStructure(xmlEntry.TypeHash, xmlNode);
                            var structureValue = ParseStructure(xmlNode, xmlInfo);
                            resultStructure.Values.Add(xmlEntry.NameHash, structureValue);
                            break;
                        }
                }
            }

            return resultStructure;
        }








        private MetaBool ReadBoolean(XmlNode node)
        {
            var booleanValue = new MetaBool();
            booleanValue.Value = bool.Parse(node.Attributes["value"].Value);
            return booleanValue;
        }

        private MetaSByte ReadSignedByte(XmlNode node)
        {
            var byteValue = new MetaSByte();
            byteValue.Value = sbyte.Parse(node.Attributes["value"].Value, NumberFormatInfo.InvariantInfo);
            return byteValue;
        }

        private MetaByte ReadUnsignedByte(XmlNode node)
        {
            var byteValue = new MetaByte();
            byteValue.Value = byte.Parse(node.Attributes["value"].Value, NumberFormatInfo.InvariantInfo);
            return byteValue;
        }

        private MetaInt16 ReadSignedShort(XmlNode node)
        {
            var shortValue = new MetaInt16();
            shortValue.Value = short.Parse(node.Attributes["value"].Value, NumberFormatInfo.InvariantInfo);
            return shortValue;
        }

        private MetaUInt16 ReadUnsignedShort(XmlNode node)
        {
            var shortValue = new MetaUInt16();
            shortValue.Value = ushort.Parse(node.Attributes["value"].Value, NumberFormatInfo.InvariantInfo);
            return shortValue;
        }

        private MetaInt32 ReadSignedInt(XmlNode node)
        {
            var intValue = new MetaInt32();
            intValue.Value = int.Parse(node.Attributes["value"].Value, NumberFormatInfo.InvariantInfo);
            return intValue;
        }

        private MetaUInt32 ReadUnsignedInt(XmlNode node)
        {
            var intValue = new MetaUInt32();
            intValue.Value = uint.Parse(node.Attributes["value"].Value, NumberFormatInfo.InvariantInfo);
            return intValue;
        }

        private MetaFloat ReadFloat(XmlNode node)
        {
            var floatValue = new MetaFloat();
            floatValue.Value = float.Parse(node.Attributes["value"].Value, NumberFormatInfo.InvariantInfo);
            return floatValue;
        }

        private MetaVector3 ReadFloatXYZ(XmlNode node)
        {
            var floatVectorValue = new MetaVector3();
            float x = float.Parse(node.Attributes["x"].Value, NumberFormatInfo.InvariantInfo);
            float y = float.Parse(node.Attributes["y"].Value, NumberFormatInfo.InvariantInfo);
            float z = float.Parse(node.Attributes["z"].Value, NumberFormatInfo.InvariantInfo);
            floatVectorValue.Value = new Vector3(x, y, z);
            return floatVectorValue;
        }

        private MetaVector4 ReadFloatXYZW(XmlNode node)
        {
            var floatVectorValue = new MetaVector4();
            float x = float.Parse(node.Attributes["x"].Value, NumberFormatInfo.InvariantInfo);
            float y = float.Parse(node.Attributes["y"].Value, NumberFormatInfo.InvariantInfo);
            float z = float.Parse(node.Attributes["z"].Value, NumberFormatInfo.InvariantInfo);
            float w = float.Parse(node.Attributes["w"].Value, NumberFormatInfo.InvariantInfo);
            floatVectorValue.Value = new Vector4(x, y, z, w);
            return floatVectorValue;
        }
        
        private MetaEnumInt8 ReadByteEnum(XmlNode node, int enumNameHash)
        {
            var byteEnum = new MetaEnumInt8();
            var enumKey = GetHashForEnumName(node.InnerText);
            var enumInfo = (MetaEnumXml)null;
            foreach (var x in xmlInfos.Enums)
            {
                if (x.NameHash == enumNameHash)
                    enumInfo = x;
            }
            foreach (var x in enumInfo.Entries)
            {
                if (x.NameHash == enumKey)
                    byteEnum.Value = (sbyte)x.Value;
            }
            return byteEnum;
        }

        private MetaEnumInt16 ReadShortEnum(XmlNode node, int enumNameHash)
        {
            var intEnum = new MetaEnumInt16();
            var it = node.InnerText.Trim();
            if (it.Equals("enum_NONE"))
            {
                intEnum.Value = -1;
            }
            else
            {
                var enumKey = GetHashForEnumName(it);
                var enumInfo = (MetaEnumXml)null;
                foreach (var x in xmlInfos.Enums)
                {
                    if (x.NameHash == enumNameHash)
                        enumInfo = x;
                }
                foreach (var x in enumInfo.Entries)
                {
                    if (x.NameHash == enumKey)
                        intEnum.Value = (short)x.Value;
                }
            }
            return intEnum;
        }

        private MetaEnumInt32 ReadIntEnum(XmlNode node, int enumNameHash)
        {
            var intEnum = new MetaEnumInt32();
            var it = node.InnerText.Trim();
            if (it.Equals("enum_NONE"))
            {
                intEnum.Value = -1;
            }
            else
            {
                var enumKey = GetHashForEnumName(it);
                var enumInfo = (MetaEnumXml)null;
                foreach (var x in xmlInfos.Enums)
                {
                    if (x.NameHash == enumNameHash)
                        enumInfo = x;
                }
                foreach (var x in enumInfo.Entries)
                {
                    if (x.NameHash == enumKey)
                        intEnum.Value = x.Value;
                }
            }
            return intEnum;
        }

        private MetaFlagsInt16 ReadShortFlags(XmlNode node, int enumNameHash)
        {
            var shortFlags = new MetaFlagsInt16();
            var enumInfo = (MetaEnumXml)null;
            foreach (var x in xmlInfos.Enums)
            {
                if (x.NameHash == enumNameHash)
                    enumInfo = x;
            }
            // TODO: Parse using Span
            var keyStrings = node.InnerText.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var x in keyStrings)
            {
                var enumKey = GetHashForFlagName(x);
                foreach (var p in enumInfo.Entries)
                {
                    if (p.NameHash == enumKey)
                        shortFlags.Value += (ushort)(1 << p.Value);
                }
            }

            return shortFlags;
        }

        private MetaFlagsInt8 ReadIntFlags1(XmlNode node, int enumNameHash)
        {
            var intFlags = new MetaFlagsInt8();

            var enumInfo = (MetaEnumXml)null;
            foreach (var x in xmlInfos.Enums)
            {
                if (x.NameHash == enumNameHash)
                    enumInfo = x;
            }
            // TODO: Parse using Span
            var keyStrings = node.InnerText.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var x in keyStrings)
            {
                var enumKey = GetHashForFlagName(x);
                foreach (var p in enumInfo.Entries)
                {
                    if (p.NameHash == enumKey)
                        intFlags.Value += (uint)(1 << p.Value);
                }
            }

            return intFlags;
        }

        private MetaFlagsInt32 ReadIntFlags2(XmlNode node, int enumNameHash)
        {
            var intFlags = new MetaFlagsInt32();

            if (enumNameHash == 0)
            {
                // TODO: Parse using Span
                var keyStrings = node.InnerText.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var x in keyStrings)
                {
                    var enumIdx = int.Parse(x.AsSpan(11), NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
                    intFlags.Value += (uint)(1 << enumIdx);
                }
            }
            else
            {
                var enumInfo = (MetaEnumXml)null;
                foreach (var x in xmlInfos.Enums)
                {
                    if (x.NameHash == enumNameHash)
                        enumInfo = x;
                }
                // TODO: Parse using Span
                var keyStrings = node.InnerText.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var x in keyStrings)
                {
                    var enumKey = GetHashForFlagName(x);
                    foreach (var y in enumInfo.Entries)
                    {
                        if (y.NameHash == enumKey)
                            intFlags.Value += (uint)(1 << y.Value);
                    }
                }
            }

            return intFlags;
        }





        private MetaArray ReadPointerArray(XmlNode node)
        {
            var arrayValue = new MetaArray();
            if (node.ChildNodes.Count > 0)
            {
                arrayValue.Entries = new List<IMetaValue>();
                foreach (XmlNode xmlPointerValue in node.ChildNodes)
                {
                    MetaGeneric gen = new MetaGeneric();
                    var theType = xmlPointerValue.Attributes["type"].Value;
                    if (!theType.Equals("NULL"))
                    {
                        var theHash = GetHashForName(theType);

                        var xnd = FindAndCheckStructure(theHash, xmlPointerValue);
                        var yy = ParseStructure(xmlPointerValue, xnd);
                        gen.Value = yy;
                    }
                    arrayValue.Entries.Add(gen);
                }
            }

            return arrayValue;
        }

        private MetaArray ReadStructureArray(XmlNode node, int structureNameHash)
        {
            var arrayValue = new MetaArray();
            var arrayType = structureNameHash;
            if (node.ChildNodes.Count > 0)
            {
                arrayValue.Entries = new List<IMetaValue>();
                foreach (XmlNode arrent in node.ChildNodes)
                {
                    var xnd = FindAndCheckStructure(arrayType, arrent);
                    var yy = ParseStructure(arrent, xnd);
                    arrayValue.Entries.Add(yy);
                }
            }
            return arrayValue;
        }

        private MetaArray ReadByteArray(XmlNode node)
        {
            var arrayValue = new MetaArray();
            var innerText = node.InnerText;

            if (string.IsNullOrEmpty(innerText))
                return arrayValue;

            arrayValue.Entries = new List<IMetaValue>();
            var items = StringParseHelpers.ParseItems<byte>(innerText);
            
            foreach (var item in items)
            {
                arrayValue.Entries.Add(new MetaByte(item));
            }

            return arrayValue;
        }

        private MetaArray ReadShortArray(XmlNode node)
        {
            var arrayValue = new MetaArray();
            var innerText = node.InnerText;

            if (string.IsNullOrEmpty(innerText))
                return arrayValue;

            arrayValue.Entries = new List<IMetaValue>();
            var items = StringParseHelpers.ParseItems<ushort>(innerText);

            foreach (var item in items)
            {
                arrayValue.Entries.Add(new MetaUInt16(item));
            }

            return arrayValue;
        }

        private MetaArray ReadIntArray(XmlNode node)
        {
            var arrayValue = new MetaArray();
            var innerText = node.InnerText;

            if (string.IsNullOrEmpty(innerText))
                return arrayValue;

            arrayValue.Entries = new List<IMetaValue>();
            var items = StringParseHelpers.ParseItems<uint>(innerText);

            foreach (var item in items)
            {
                arrayValue.Entries.Add(new MetaUInt32(item));
            }

            return arrayValue;
        }

        private MetaArray ReadFloatArray(XmlNode node)
        {
            var arrayValue = new MetaArray();
            var innerText = node.InnerText;

            if (string.IsNullOrEmpty(innerText))
                return arrayValue;

            arrayValue.Entries = new List<IMetaValue>();
            var items = StringParseHelpers.ParseItems<float>(innerText);

            foreach (var item in items)
            {
                arrayValue.Entries.Add(new MetaFloat(item));
            }

            return arrayValue;
        }

        private MetaArray ReadFloatVectorArray(XmlNode node)
        {
            var arrayValue = new MetaArray();
            var innerText = node.InnerText;

            if (string.IsNullOrEmpty(innerText))
                return arrayValue;

            arrayValue.Entries = new List<IMetaValue>();
            var items = StringParseHelpers.ParseItems<float>(innerText);
            Debug.Assert(items.Count % 3 == 0);

            for (int i = 0; i < items.Count; i+=3)
            {
                arrayValue.Entries.Add(new MetaVector3(items[i], items[i + 1], items[i + 2]));
            }

            return arrayValue;
        }

        private MetaArray ReadHashArray(XmlNode node)
        {
            var arrayValue = new MetaArray();
            if (node.ChildNodes.Count > 0)
            {
                arrayValue.Entries = new List<IMetaValue>();
                foreach (XmlNode kkk in node.ChildNodes)
                {
                    var p = kkk.InnerText.Trim();
                    if (!string.IsNullOrEmpty(p))
                    {
                        arrayValue.Entries.Add(new MetaStringHash(GetHashForName(p)));
                    }
                    else
                    {
                        arrayValue.Entries.Add(new MetaStringHash(0));
                    }
                }
            }

            return arrayValue;
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

        private void MetaBuildStructureInfos(MetaDefinitions xmlInfo)
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

        public MetaStructureXml FindAndCheckStructure(XmlNode node)
        {
            int h = GetHashForName(node.Name);
            foreach(var x in xmlInfos.Structures)
            {
                if (x.NameHash == h)
                {
                    if (x.Entries.Count != node.ChildNodes.Count)
                        continue;

                    bool everythinOk = true;
                    foreach (var zz in x.Entries)
                    {
                        bool fnd = false;
                        foreach (XmlNode qq in node.ChildNodes)
                        {
                            var qqnamehash = GetHashForName(qq.Name);
                            if (qqnamehash == zz.NameHash)
                                fnd = true;
                        }
                        if (!fnd)
                            everythinOk = false;
                    }
                    if (!everythinOk)
                        continue;


                    return x;
                }
            }

            return null;
        }

        public MetaStructureXml FindAndCheckStructure(int h, XmlNode node)
        {
            foreach (var x in xmlInfos.Structures)
            {
                if (x.NameHash == h)
                {
                    if (x.Entries.Count != node.ChildNodes.Count)
                        continue;

                    bool everythinOk = true;
                    foreach (var zz in x.Entries)
                    {
                        bool fnd = false;
                        foreach (XmlNode qq in node.ChildNodes)
                        {
                            var qqnamehash = GetHashForName(qq.Name);
                            if (qqnamehash == zz.NameHash)
                                fnd = true;
                        }
                        if (!fnd)
                            everythinOk = false;
                    }
                    if (!everythinOk)
                        continue;


                    return x;
                }
            }

            return null;
        }

    }
}
