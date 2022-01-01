// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.GTA5.ResourceWrappers.PC.Meta.Types;
using RageLib.Resources.Common;
using RageLib.Resources.GTA5.PC.Meta;
using System.Collections.Generic;

namespace RageLib.GTA5.Tests.ResourceWrappers.PC.Meta
{
    public class TestDataset
    {
        public static MetaStructure MakeDataset()
        {
            var valueInfo = new StructureInfo();
            valueInfo.StructureNameHash = 0x22DD6F04;

            var rootStructure = new MetaStructure(null, valueInfo);
            rootStructure.Values = new Dictionary<int, IMetaValue>();
            rootStructure.Values.Add(unchecked((int)0x38C62F77), MakeStructureWithSimpleData());
            rootStructure.Values.Add(unchecked((int)0x97CC848A), MakeStructureWithEnumData());
            rootStructure.Values.Add(0x3A5B9F33, MakeStructureWithSimpleReferencedData());
            rootStructure.Values.Add(0x53663957, MakeStructureWithComplexReferencedData());
            return rootStructure;
        }

        private static MetaStructure MakeStructureWithSimpleData()
        {
            var structureWithSimpleTypesInfo = new StructureInfo();
            var structureWithSimpleTypes = new MetaStructure(null, structureWithSimpleTypesInfo);
            structureWithSimpleTypes.Values = new Dictionary<int, IMetaValue>();
            structureWithSimpleTypes.Values.Add(unchecked((int)0x36C55540), new MetaBool(false));
            structureWithSimpleTypes.Values.Add(unchecked((int)0x707975FF), new MetaBool(true));
            structureWithSimpleTypes.Values.Add(unchecked((int)0x96142337), new MetaSByte(-128));
            structureWithSimpleTypes.Values.Add(unchecked((int)0xD4D9059D), new MetaSByte(-127));
            structureWithSimpleTypes.Values.Add(unchecked((int)0x710D0955), new MetaSByte(126));
            structureWithSimpleTypes.Values.Add(unchecked((int)0x860ACDD8), new MetaSByte(127));
            structureWithSimpleTypes.Values.Add(unchecked((int)0xCDFD7789), new MetaByte(0));
            structureWithSimpleTypes.Values.Add(unchecked((int)0x7DCCF225), new MetaByte(1));
            structureWithSimpleTypes.Values.Add(unchecked((int)0xA0AB9B78), new MetaByte(254));
            structureWithSimpleTypes.Values.Add(unchecked((int)0x3A223898), new MetaByte(255));
            structureWithSimpleTypes.Values.Add(unchecked((int)0x2F07F270), new MetaInt16(-32768));
            structureWithSimpleTypes.Values.Add(unchecked((int)0x83E5053E), new MetaInt16(-32767));
            structureWithSimpleTypes.Values.Add(unchecked((int)0x86F3BC1E), new MetaInt16(32766));
            structureWithSimpleTypes.Values.Add(unchecked((int)0x2C916F02), new MetaInt16(32767));
            structureWithSimpleTypes.Values.Add(unchecked((int)0x1972DD39), new MetaUInt16(0));
            structureWithSimpleTypes.Values.Add(unchecked((int)0x1433E9A2), new MetaUInt16(1));
            structureWithSimpleTypes.Values.Add(unchecked((int)0xABF22E97), new MetaUInt16(65534));
            structureWithSimpleTypes.Values.Add(unchecked((int)0xCAD920FA), new MetaUInt16(65535));
            structureWithSimpleTypes.Values.Add(unchecked((int)0x96AA9C22), new MetaInt32(-2147483648));
            structureWithSimpleTypes.Values.Add(unchecked((int)0xBE498F77), new MetaInt32(-2147483647));
            structureWithSimpleTypes.Values.Add(unchecked((int)0xDEA66123), new MetaInt32(2147483646));
            structureWithSimpleTypes.Values.Add(unchecked((int)0xA7A347FE), new MetaInt32(2147483647));
            structureWithSimpleTypes.Values.Add(unchecked((int)0x5940A2C4), new MetaUInt32(0));
            structureWithSimpleTypes.Values.Add(unchecked((int)0x7AE8E34B), new MetaUInt32(1));
            structureWithSimpleTypes.Values.Add(unchecked((int)0x48758F24), new MetaUInt32(4294967294));
            structureWithSimpleTypes.Values.Add(unchecked((int)0x2DCCF53B), new MetaUInt32(4294967295));
            structureWithSimpleTypes.Values.Add(unchecked((int)0x7C6BAA24), new MetaFloat(1.2f));
            structureWithSimpleTypes.Values.Add(unchecked((int)0xC81C39E6), new MetaFloat(12.0f));
            structureWithSimpleTypes.Values.Add(unchecked((int)0xC599B2B0), new MetaVector3(1.2f, 3.4f, 5.6f));
            structureWithSimpleTypes.Values.Add(unchecked((int)0xA2B4F045), new MetaVector3(12.0f, 34.0f, 56.0f));
            structureWithSimpleTypes.Values.Add(unchecked((int)0xE0B18333), new MetaVector4(1.2f, 3.4f, 5.6f, 7.8f));
            structureWithSimpleTypes.Values.Add(unchecked((int)0xA7E3D660), new MetaVector4(12.0f, 34.0f, 56.0f, 78.0f));
            structureWithSimpleTypes.Values.Add(unchecked((int)0x3B8AF0C2), new MetaArrayLocal<byte>(null) { Value = new byte[] { 0, 1, 254, 255 } });
            var charinfo = new StructureEntryInfo();
            charinfo.ReferenceKey = 64;
            structureWithSimpleTypes.Values.Add(unchecked((int)0x8FF34AB5), new MetaString(charinfo, "A String"));
            structureWithSimpleTypes.Values.Add(unchecked((int)0x17525AB1), new MetaStringHash(unchecked((int)0xF63A8BC0)));
            structureWithSimpleTypes.Values.Add(unchecked((int)0x10D59C62), new MetaStringHash(0));
            return structureWithSimpleTypes;
        }

        private static MetaStructure MakeStructureWithEnumData()
        {
            var structureWithEnumsInfo = new StructureInfo();
            var structureWithEnums = new MetaStructure(null, structureWithEnumsInfo);
            structureWithEnums.Values = new Dictionary<int, IMetaValue>();
            structureWithEnums.Values.Add(unchecked((int)0x2300AF3B), MakeByteEnum());
            structureWithEnums.Values.Add(unchecked((int)0x56E94C50), MakeIntEnumA());
            structureWithEnums.Values.Add(unchecked((int)0x43F0EEF4), MakeIntEnumB());
            structureWithEnums.Values.Add(unchecked((int)0xB4B7824B), MakeShortFlagsA());
            structureWithEnums.Values.Add(unchecked((int)0x1B3098A9), MakeShortFlagsB());
            structureWithEnums.Values.Add(unchecked((int)0xA51CF61E), MakeIntFlags1A());
            structureWithEnums.Values.Add(unchecked((int)0x11045D33), MakeIntFlags1B());
            structureWithEnums.Values.Add(unchecked((int)0xC66A7EC6), MakeIntFlags2A());
            structureWithEnums.Values.Add(unchecked((int)0x53C471C0), MakeIntFlags2B());
            structureWithEnums.Values.Add(unchecked((int)0x89D91A45), MakeIntFlags2C());
            return structureWithEnums;
        }

        private static MetaEnumInt8 MakeByteEnum()
        {
            var byteEnum = new MetaEnumInt8(1);
            var en1info = new EnumInfo();
            en1info.Entries = new ResourceSimpleArray<EnumEntryInfo>();
            var q1 = new EnumEntryInfo();
            q1.EntryValue = 0;
            q1.EntryNameHash = unchecked((int)0x8462C771);
            var q2 = new EnumEntryInfo();
            q2.EntryValue = 1;
            q2.EntryNameHash = unchecked((int)0xB6D45EB0);
            en1info.Entries.Add(q1);
            en1info.Entries.Add(q2);
            byteEnum.info = en1info;
            return byteEnum;
        }

        private static MetaEnumInt32 MakeIntEnumA()
        {
            var intEnum = new MetaEnumInt32(1);
            intEnum.info = MakeIntEnumInfo();
            return intEnum;
        }

        private static MetaEnumInt32 MakeIntEnumB()
        {
            var intEnum = new MetaEnumInt32(-1);
            intEnum.info = MakeIntEnumInfo();
            return intEnum;
        }

        private static EnumInfo MakeIntEnumInfo()
        {
            var intEnumInfo = new EnumInfo();
            intEnumInfo.Entries = new ResourceSimpleArray<EnumEntryInfo>();
            var q1x = new EnumEntryInfo();
            q1x.EntryValue = 0;
            q1x.EntryNameHash = unchecked((int)0x856716BD);
            var q2x = new EnumEntryInfo();
            q2x.EntryValue = 1;
            q2x.EntryNameHash = unchecked((int)0x244B7640);
            intEnumInfo.Entries.Add(q1x);
            intEnumInfo.Entries.Add(q2x);
            return intEnumInfo;
        }

        private static MetaFlagsInt16 MakeShortFlagsA()
        {
            var shortFlags = new MetaFlagsInt16();
            shortFlags.info = MakeShortFlagsInfo();
            shortFlags.Value = 3;
            return shortFlags;
        }

        private static MetaFlagsInt16 MakeShortFlagsB()
        {
            var shortFlags = new MetaFlagsInt16();
            shortFlags.info = MakeShortFlagsInfo();
            shortFlags.Value = 0;
            return shortFlags;
        }

        private static EnumInfo MakeShortFlagsInfo()
        {
            var shortFlagsInfo = new EnumInfo();
            shortFlagsInfo.Entries = new ResourceSimpleArray<EnumEntryInfo>();
            var shortFlag1Info = new EnumEntryInfo();
            shortFlag1Info.EntryValue = 0;
            shortFlag1Info.EntryNameHash = unchecked((int)0xEC9BDD31);
            var shortFlag2Info = new EnumEntryInfo();
            shortFlag2Info.EntryValue = 1;
            shortFlag2Info.EntryNameHash = unchecked((int)0xDC2ECBE8);
            shortFlagsInfo.Entries.Add(shortFlag1Info);
            shortFlagsInfo.Entries.Add(shortFlag2Info);
            return shortFlagsInfo;
        }

        private static MetaFlagsInt8 MakeIntFlags1A()
        {
            var intFlags = new MetaFlagsInt8();
            intFlags.info = MakeIntFlags1Info();
            intFlags.Value = 3;
            return intFlags;
        }

        private static MetaFlagsInt8 MakeIntFlags1B()
        {
            var intFlags = new MetaFlagsInt8();
            intFlags.info = MakeIntFlags1Info();
            intFlags.Value = 0;
            return intFlags;
        }

        private static EnumInfo MakeIntFlags1Info()
        {
            var intFlagsInfo = new EnumInfo();
            intFlagsInfo.Entries = new ResourceSimpleArray<EnumEntryInfo>();
            var intFlag1Info = new EnumEntryInfo();
            intFlag1Info.EntryValue = 0;
            intFlag1Info.EntryNameHash = unchecked((int)0xF3F0428C);
            var intFlag2Info = new EnumEntryInfo();
            intFlag2Info.EntryValue = 1;
            intFlag2Info.EntryNameHash = unchecked((int)0x28DBBD98);
            intFlagsInfo.Entries.Add(intFlag1Info);
            intFlagsInfo.Entries.Add(intFlag2Info);
            return intFlagsInfo;
        }

        private static MetaFlagsInt32 MakeIntFlags2A()
        {
            return new MetaFlagsInt32(MakeIntFlags2Info(), 3);
        }

        private static MetaFlagsInt32 MakeIntFlags2B()
        {
            return new MetaFlagsInt32(null, 3);
        }

        private static MetaFlagsInt32 MakeIntFlags2C()
        {
            return new MetaFlagsInt32(MakeIntFlags2Info(), 0);
        }

        private static EnumInfo MakeIntFlags2Info()
        {
            var intFlagsInfo = new EnumInfo();
            intFlagsInfo.Entries = new ResourceSimpleArray<EnumEntryInfo>();
            var int22Flag1Info = new EnumEntryInfo();
            int22Flag1Info.EntryValue = 0;
            int22Flag1Info.EntryNameHash = unchecked((int)0x01769F10);
            var int22Flag2Info = new EnumEntryInfo();
            int22Flag2Info.EntryValue = 1;
            int22Flag2Info.EntryNameHash = unchecked((int)0x3AFAC976);
            intFlagsInfo.Entries.Add(int22Flag1Info);
            intFlagsInfo.Entries.Add(int22Flag2Info);
            return intFlagsInfo;
        }

        private static MetaStructure MakeStructureWithSimpleReferencedData()
        {
            var structureWithSimpleReferenceDataInfo = new StructureInfo();
            var structureWithSimpleReferenceData = new MetaStructure(null, structureWithSimpleReferenceDataInfo);
            structureWithSimpleReferenceData.Values = new Dictionary<int, IMetaValue>();
            structureWithSimpleReferenceData.Values.Add(unchecked((int)0xEF099C3A), MakeCharArray());
            structureWithSimpleReferenceData.Values.Add(unchecked((int)0x79FE4E42), MakeShortArray());
            structureWithSimpleReferenceData.Values.Add(unchecked((int)0x62AFD2A7), MakeIntArray());
            structureWithSimpleReferenceData.Values.Add(unchecked((int)0x8FD208FE), MakeFloatArray());
            structureWithSimpleReferenceData.Values.Add(unchecked((int)0xD094EFE2), MakeFloatVectorArray());
            structureWithSimpleReferenceData.Values.Add(unchecked((int)0x68B43521), MakeHashArray());
            structureWithSimpleReferenceData.Values.Add(unchecked((int)0x3A6E4591), new MetaStringPointer("A String"));
            structureWithSimpleReferenceData.Values.Add(unchecked((int)0xC9811541), new MetaStringPointer(null));
            structureWithSimpleReferenceData.Values.Add(unchecked((int)0xC8C01542), new MetaDataBlockPointer(null, new byte[] { 0, 1, 254, 255 }));
            structureWithSimpleReferenceData.Values.Add(unchecked((int)0x2FCAB965), new MetaDataBlockPointer(null, null));
            return structureWithSimpleReferenceData;
        }

        private static MetaArray MakeCharArray()
        {
            var metaArray = new MetaArray();
            metaArray.info = new StructureEntryInfo();
            metaArray.info.DataType = StructureEntryDataType.UInt8;
            metaArray.Entries = new List<IMetaValue>();
            metaArray.Entries.Add(new MetaByte(0));
            metaArray.Entries.Add(new MetaByte(1));
            metaArray.Entries.Add(new MetaByte(254));
            metaArray.Entries.Add(new MetaByte(255));
            return metaArray;
        }

        private static MetaArray MakeShortArray()
        {
            var metaArray = new MetaArray();
            metaArray.info = new StructureEntryInfo();
            metaArray.info.DataType = StructureEntryDataType.UInt16;
            metaArray.Entries = new List<IMetaValue>();
            metaArray.Entries.Add(new MetaUInt16(0));
            metaArray.Entries.Add(new MetaUInt16(1));
            metaArray.Entries.Add(new MetaUInt16(65534));
            metaArray.Entries.Add(new MetaUInt16(65535));
            return metaArray;
        }

        private static MetaArray MakeIntArray()
        {
            var metaArray = new MetaArray();
            metaArray.info = new StructureEntryInfo();
            metaArray.info.DataType = StructureEntryDataType.UInt32;
            metaArray.Entries = new List<IMetaValue>();
            metaArray.Entries.Add(new MetaUInt32(0));
            metaArray.Entries.Add(new MetaUInt32(1));
            metaArray.Entries.Add(new MetaUInt32(4294967294));
            metaArray.Entries.Add(new MetaUInt32(4294967295));
            return metaArray;
        }

        private static MetaArray MakeFloatArray()
        {
            var metaArray = new MetaArray();
            metaArray.info = new StructureEntryInfo();
            metaArray.info.DataType = StructureEntryDataType.Float;
            metaArray.Entries = new List<IMetaValue>();
            metaArray.Entries.Add(new MetaFloat(0.1f));
            metaArray.Entries.Add(new MetaFloat(0.2f));
            metaArray.Entries.Add(new MetaFloat(1000f));
            metaArray.Entries.Add(new MetaFloat(2000f));
            return metaArray;
        }

        private static MetaArray MakeFloatVectorArray()
        {
            var metaArray = new MetaArray();
            metaArray.info = new StructureEntryInfo();
            metaArray.info.DataType = StructureEntryDataType.Vector3;
            metaArray.Entries = new List<IMetaValue>();
            metaArray.Entries.Add(new MetaVector3(-50.00f, -6.00f, -30.00f));
            metaArray.Entries.Add(new MetaVector3(50.00f, 6.00f, 30.00f));
            return metaArray;
        }

        private static MetaArray MakeHashArray()
        {
            var metaArray = new MetaArray();
            metaArray.info = new StructureEntryInfo();
            metaArray.info.DataType = StructureEntryDataType.StringHash;
            metaArray.Entries = new List<IMetaValue>();
            metaArray.Entries.Add(new MetaStringHash(unchecked((int)0xCA134811)));
            metaArray.Entries.Add(new MetaStringHash(unchecked((int)0x1AA9F061)));
            metaArray.Entries.Add(new MetaStringHash(0));
            return metaArray;
        }

        private static MetaStructure MakeStructureWithComplexReferencedData()
        {
            var structureWithComplexReferenceDataInfo = new StructureInfo();
            var structureWithComplexReferenceData = new MetaStructure(null, structureWithComplexReferenceDataInfo);
            structureWithComplexReferenceData.Values = new Dictionary<int, IMetaValue>();
            structureWithComplexReferenceData.Values.Add(unchecked((int)0x6F004ECC), MakeStructureArray());
            structureWithComplexReferenceData.Values.Add(unchecked((int)0x8F3E7BA7), MakeStructurePointerArray());
            return structureWithComplexReferenceData;
        }

        private static MetaArray MakeStructureArray()
        {
            MetaStructure substr1 = new MetaStructure(null, null);
            substr1.Values = new Dictionary<int, IMetaValue>();
            substr1.Values.Add(unchecked((int)0x620795CF), new MetaBool(false));
            substr1.Values.Add(unchecked((int)0x2518B65F), new MetaBool(true));

            MetaStructure substr2 = new MetaStructure(null, null);
            substr2.Values = new Dictionary<int, IMetaValue>();
            substr2.Values.Add(unchecked((int)0x620795CF), new MetaBool(true));
            substr2.Values.Add(unchecked((int)0x2518B65F), new MetaBool(false));

            MetaArray structureArray = new MetaArray();
            structureArray.info = new StructureEntryInfo();
            structureArray.info.DataType = StructureEntryDataType.Structure;
            structureArray.Entries = new List<IMetaValue>();
            structureArray.Entries.Add(substr1);
            structureArray.Entries.Add(substr2);
            return structureArray;
        }

        private static MetaArray MakeStructurePointerArray()
        {
            var metainf111 = new StructureInfo();
            metainf111.StructureNameHash = 0x2D8B6A9C;
            MetaStructure metasubstr1 = new MetaStructure(null, metainf111);
            metasubstr1.Values = new Dictionary<int, IMetaValue>();
            metasubstr1.Values.Add(unchecked((int)0x04792618), new MetaBool(false));
            metasubstr1.Values.Add(unchecked((int)0xD302778A), new MetaBool(true));
            MetaGeneric pointerValue1 = new MetaGeneric();
            pointerValue1.Value = metasubstr1;

            var metainf222 = new StructureInfo();
            metainf222.StructureNameHash = unchecked((int)0xA71A1B09);
            MetaStructure metasubstr2 = new MetaStructure(null, metainf222);
            metasubstr2.Values = new Dictionary<int, IMetaValue>();
            metasubstr2.Values.Add(unchecked((int)0x8705BF6F), new MetaBool(true));
            metasubstr2.Values.Add(unchecked((int)0x981F3DBC), new MetaBool(false));
            MetaGeneric pointerValue2 = new MetaGeneric();
            pointerValue2.Value = metasubstr2;

            MetaGeneric pointerValue3 = new MetaGeneric();
            pointerValue3.Value = null;

            MetaArray pointerArray = new MetaArray();
            pointerArray.info = new StructureEntryInfo();
            pointerArray.info.DataType = StructureEntryDataType.Structure;
            pointerArray.Entries = new List<IMetaValue>();
            pointerArray.Entries.Add(pointerValue1);
            pointerArray.Entries.Add(pointerValue2);
            pointerArray.Entries.Add(pointerValue3);
            return pointerArray;
        }
    }
}
