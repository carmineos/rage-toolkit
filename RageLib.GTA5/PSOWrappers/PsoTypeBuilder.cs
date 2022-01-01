// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.GTA5.PSO;
using RageLib.GTA5.PSOWrappers.Types;
using System;
using System.Diagnostics;

namespace RageLib.GTA5.PSOWrappers
{
    public static class PsoTypeBuilder
    {
        public static IPsoValue Make(PsoFile pso, PsoStructureInfo structureInfo, PsoStructureEntryInfo entryInfo)
        {
            switch (entryInfo.Type)
            {
                case ParMemberType.ARRAY:
                    {
                        var typeIndex = entryInfo.ReferenceKey & 0x0000FFFF;
                        var count = (entryInfo.ReferenceKey >> 16) & 0x0000FFFF;
                        var type = structureInfo.Entries[typeIndex];

                        switch ((ParMemberArraySubtype)entryInfo.SubType)
                        {
                            case ParMemberArraySubtype.ATARRAY: return new PsoArray0(pso, structureInfo, type);
                            case ParMemberArraySubtype.ATFIXEDARRAY: return new PsoArray1(pso, structureInfo, type, count);
                            case ParMemberArraySubtype.ATRANGEARRAY: return new PsoArray2(pso, structureInfo, type, count);
                            case ParMemberArraySubtype.MEMBER: return new PsoArray4(pso, structureInfo, type, count);
                            case (ParMemberArraySubtype)0x81: return new PsoArray129(pso, structureInfo, type, count);
                        }
                        break;
                    }
                case ParMemberType.STRING:
                    {
                        switch ((ParMemberStringSubtype)entryInfo.SubType)
                        {
                            case ParMemberStringSubtype.MEMBER:
                                {
                                    var len = (entryInfo.ReferenceKey >> 16) & 0x0000FFFF;
                                    return new PsoString0(len);
                                }
                            case ParMemberStringSubtype.POINTER: return new PsoString1();
                            case ParMemberStringSubtype.CONST_STRING: return new PsoString2();
                            case ParMemberStringSubtype.ATSTRING: return new PsoString3();
                            case ParMemberStringSubtype.ATNONFINALHASHSTRING: return new PsoString7();
                            case ParMemberStringSubtype.ATFINALHASHSTRING: return new PsoString8();
                        }
                        break;
                    }
                case ParMemberType.ENUM:
                    {
                        var enumInfo = GetEnumInfo(pso, entryInfo.ReferenceKey);

                        switch ((ParMemberEnumSubtype)entryInfo.SubType)
                        {
                            case ParMemberEnumSubtype._32BIT: return new PsoEnumInt32(enumInfo);
                            case ParMemberEnumSubtype._16BIT: return new PsoEnumInt16(enumInfo);
                            case ParMemberEnumSubtype._8BIT: return new PsoEnumInt8(enumInfo);
                        }
                        break;
                    }
                case ParMemberType.BITSET:
                    {
                        var sidx = entryInfo.ReferenceKey & 0x0000FFFF;
                        var reftype = structureInfo.Entries[sidx];
                        var enumInfo = GetEnumInfo(pso, reftype.ReferenceKey);

                        switch ((ParMemberBitsetSubtype)entryInfo.SubType)
                        {
                            case ParMemberBitsetSubtype._32BIT: return new PsoFlagsInt32(enumInfo);
                            case ParMemberBitsetSubtype._16BIT: return new PsoFlagsInt16(enumInfo);
                            case ParMemberBitsetSubtype._8BIT: return new PsoFlagsInt8(enumInfo);
                        }
                        break;
                    }
                case ParMemberType.UINT:
                    {
                        switch (entryInfo.SubType)
                        {
                            case 0: return new PsoUInt32();
                            case 1: return new PsoUInt32Hex();
                        }
                        break;
                    }
                case ParMemberType.STRUCT:
                    {
                        switch ((ParMemberStructSubtype)entryInfo.SubType)
                        {
                            case ParMemberStructSubtype.STRUCTURE:
                                {
                                    var t1 = GetStructureInfo(pso, entryInfo.ReferenceKey);
                                    var t2 = GetStructureIndexInfo(pso, entryInfo.ReferenceKey);
                                    var entryValue = new PsoStructure(pso, t1, t2, entryInfo);
                                    return entryValue;
                                }
                            case ParMemberStructSubtype.POINTER:
                                {
                                    return new PsoStructure3(pso, structureInfo, entryInfo);
                                }
                        }
                        break;
                    }
                case ParMemberType.MAP:
                    {
                        switch ((ParMemberMapSubtype)entryInfo.SubType)
                        {
                            case ParMemberMapSubtype.ATBINARYMAP:
                                {
                                    var idx1 = entryInfo.ReferenceKey & 0x0000FFFF;
                                    var idx2 = (entryInfo.ReferenceKey >> 16) & 0x0000FFFF;
                                    var reftype1 = structureInfo.Entries[idx2];
                                    var reftype2 = structureInfo.Entries[idx1];
                                    return new PsoMap(pso, structureInfo, reftype1, reftype2);
                                }
                        }
                        break;
                    }
                case ParMemberType.BOOL:
                    {
                        Debug.Assert(entryInfo.SubType == 0);
                        return new PsoBoolean();
                    }
                case ParMemberType.CHAR:
                    {
                        Debug.Assert(entryInfo.SubType == 0);
                        return new PsoSByte();
                    }
                case ParMemberType.UCHAR:
                    {
                        Debug.Assert(entryInfo.SubType == 0);
                        return new PsoByte();
                    }
                case ParMemberType.SHORT:
                    {
                        Debug.Assert(entryInfo.SubType == 0);
                        return new PsoInt16();
                    }
                case ParMemberType.USHORT:
                    {
                        Debug.Assert(entryInfo.SubType == 0);
                        return new PsoUInt16();
                    }
                case ParMemberType.INT:
                    {
                        Debug.Assert(entryInfo.SubType == 0);
                        return new PsoInt32();
                    }
                case ParMemberType.FLOAT:
                    {
                        Debug.Assert(entryInfo.SubType == 0);
                        return new PsoFloat();
                    }
                case ParMemberType.VECTOR2:
                    {
                        Debug.Assert(entryInfo.SubType == 0);
                        return new PsoVector2();
                    }
                case ParMemberType.VECTOR3:
                    {
                        Debug.Assert(entryInfo.SubType == 0);
                        return new PsoVector3();
                    }
                case ParMemberType.VECTOR4:
                    {
                        Debug.Assert(entryInfo.SubType == 0);
                        return new PsoVector4();
                    }
                case ParMemberType.VEC3V:
                    {
                        Debug.Assert(entryInfo.SubType == 0);
                        return new PsoVec3V();
                    }
                case ParMemberType.VEC4V:
                    {
                        Debug.Assert(entryInfo.SubType == 0);
                        return new PsoVec4V();
                    }
                case ParMemberType.FLOAT16:
                    {
                        Debug.Assert(entryInfo.SubType == 0);
                        return new PsoFloat16();
                    }
                case ParMemberType.UINT64:
                    {
                        Debug.Assert(entryInfo.SubType == 0);
                        return new PsoUInt64();
                    }
                case ParMemberType.INT64:
                    {
                        Debug.Assert(entryInfo.SubType == 0);
                        return new PsoInt64();
                    }
            }
            throw new Exception($"Unsupported {nameof(entryInfo.SubType)}: {entryInfo.SubType} for {nameof(ParMemberType)}: {entryInfo.Type}");
        }

        public static PsoStructureInfo GetStructureInfo(PsoFile meta, int structureKey)
        {
            for (int i = 0; i < meta.DefinitionSection.Count; i++)
                if (meta.DefinitionSection.EntriesIdx[i].NameHash == structureKey)
                    return (PsoStructureInfo)meta.DefinitionSection.Entries[i];
            return null;
        }

        public static PsoEnumInfo GetEnumInfo(PsoFile meta, int structureKey)
        {
            for (int i = 0; i < meta.DefinitionSection.Count; i++)
                if (meta.DefinitionSection.EntriesIdx[i].NameHash == structureKey)
                    return (PsoEnumInfo)meta.DefinitionSection.Entries[i];
            return null;
        }

        public static PsoElementIndexInfo GetStructureIndexInfo(PsoFile meta, int structureKey)
        {
            for (int i = 0; i < meta.DefinitionSection.Count; i++)
                if (meta.DefinitionSection.EntriesIdx[i].NameHash == structureKey)
                    return (PsoElementIndexInfo)meta.DefinitionSection.EntriesIdx[i];
            return null;
        }

        public static Tuple<PsoElementInfo, PsoElementIndexInfo> GetElementInfoAndElementIndexInfo(PsoFile meta, int structureKey)
        {
            for (int i = 0; i < meta.DefinitionSection.Count; i++)
                if (meta.DefinitionSection.EntriesIdx[i].NameHash == structureKey)
                    return new Tuple<PsoElementInfo, PsoElementIndexInfo>(meta.DefinitionSection.Entries[i], meta.DefinitionSection.EntriesIdx[i]);
            return null;
        }
    }
}
