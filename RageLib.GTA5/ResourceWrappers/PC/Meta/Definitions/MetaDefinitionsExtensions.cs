// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common.Collections;
using RageLib.Resources.GTA5.PC.Meta;
using System.Collections.Generic;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Definitions
{
    public static class MetaDefinitionsExtensions
    {
        public static ResourceSimpleArray<StructureInfo> BuildMetaStructureInfos(this MetaDefinitions definitions)
        {
            var structureInfos = new ResourceSimpleArray<StructureInfo>();

            foreach (var xmlStructureInfo in definitions.Structures)
                structureInfos.Add(xmlStructureInfo.ToStructureInfo());

            return structureInfos;
        }

        public static ResourceSimpleArray<EnumInfo> BuildMetaEnumInfos(this MetaDefinitions definitions)
        {
            var enumInfos = new ResourceSimpleArray<EnumInfo>();

            foreach (var xmlEnumInfo in definitions.Enums)
                enumInfos.Add(xmlEnumInfo.ToEnumInfo());

            return enumInfos;
        }

        public static EnumInfo ToEnumInfo(this MetaEnumXml enumDefinition)
        {
            var enumInfo = new EnumInfo
            {
                EnumNameHash = enumDefinition.NameHash,
                EnumKey = enumDefinition.Key,
                Entries = new ResourceSimpleArray<EnumEntryInfo>()
            };

            foreach (var xmlEnumEntryInfo in enumDefinition.Entries)
            {
                var enumEntryInfo = new EnumEntryInfo
                {
                    EntryNameHash = xmlEnumEntryInfo.NameHash,
                    EntryValue = xmlEnumEntryInfo.Value
                };

                enumInfo.Entries.Add(enumEntryInfo);
            }

            return enumInfo;
        }

        public static StructureInfo ToStructureInfo(this MetaStructureXml structureDefinition)
        {
            var structureInfo = new StructureInfo
            {
                StructureNameHash = structureDefinition.NameHash,
                StructureKey = structureDefinition.Key,
                Unknown_8h = structureDefinition.Unknown,
                StructureLength = structureDefinition.Length,
                Entries = new ResourceSimpleArray<StructureEntryInfo>()
            };

            foreach (var structureEntryDefinition in structureDefinition.Entries)
            {
                var xmlArrayTypeStack = new Stack<MetaStructureArrayTypeXml>();
                var xmlArrayType = structureEntryDefinition.ArrayType;
                while (xmlArrayType != null)
                {
                    xmlArrayTypeStack.Push(xmlArrayType);
                    xmlArrayType = xmlArrayType.ArrayType;
                }

                while (xmlArrayTypeStack.Count > 0)
                {
                    xmlArrayType = xmlArrayTypeStack.Pop();

                    var arrayDataType = (StructureEntryDataType)xmlArrayType.Type;
                    var arrayStructureEntryInfo = new StructureEntryInfo
                    {
                        EntryNameHash = 0x100,
                        DataOffset = 0,
                        DataType = arrayDataType,
                        Unknown_9h = 0,
                        ReferenceKey = xmlArrayType.TypeHash,
                        ReferenceTypeIndex = arrayDataType == StructureEntryDataType.Array || arrayDataType == StructureEntryDataType.ArrayLocal
                        ? (short)(structureInfo.Entries.Count - 1)
                        : (short)0
                    };

                    structureInfo.Entries.Add(arrayStructureEntryInfo);
                }

                var dataType = (StructureEntryDataType)structureEntryDefinition.Type;

                var structureEntryInfo = new StructureEntryInfo
                {
                    EntryNameHash = structureEntryDefinition.NameHash,
                    DataOffset = structureEntryDefinition.Offset,
                    DataType = dataType,
                    Unknown_9h = (byte)structureEntryDefinition.Unknown,
                    ReferenceKey = structureEntryDefinition.TypeHash,
                    ReferenceTypeIndex = dataType == StructureEntryDataType.Array || dataType == StructureEntryDataType.ArrayLocal
                    ? (short)(structureInfo.Entries.Count - 1)
                    : (short)0
                };

                structureInfo.Entries.Add(structureEntryInfo);
            }

            return structureInfo;
        }
    }
}