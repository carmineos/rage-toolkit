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
                        switch ((ParMemberArraySubtype)entryInfo.SubType)
                        {
                            case ParMemberArraySubtype.ATARRAY:
                                {
                                    var t = structureInfo.Entries[entryInfo.ReferenceKey & 0x0000FFFF];
                                    return new PsoArray0(pso, structureInfo, t);
                                }
                            case ParMemberArraySubtype.ATFIXEDARRAY:
                                {
                                    var typeIndex = entryInfo.ReferenceKey & 0x0000FFFF;
                                    var num = (entryInfo.ReferenceKey >> 16) & 0x0000FFFF;
                                    var t = structureInfo.Entries[typeIndex];
                                    return new PsoArray1(pso, structureInfo, t, num);
                                }
                            case ParMemberArraySubtype.MEMBER:
                                {
                                    var typeIndex = entryInfo.ReferenceKey & 0x0000FFFF;
                                    var num = (entryInfo.ReferenceKey >> 16) & 0x0000FFFF;
                                    var t = structureInfo.Entries[typeIndex];
                                    return new PsoArray4(pso, structureInfo, t, num);
                                }
                            default: throw new Exception($"Unsupported {nameof(entryInfo.SubType)}: {entryInfo.SubType} for {nameof(ParMemberType)}: {entryInfo.Type}");
                        }
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
                            case ParMemberStringSubtype.POINTER:
                                {
                                    return new PsoString1();
                                }
                            case ParMemberStringSubtype.CONST_STRING:
                                {
                                    return new PsoString2();
                                }
                            case ParMemberStringSubtype.ATSTRING:
                                {
                                    return new PsoString3();
                                }
                            case ParMemberStringSubtype.ATNONFINALHASHSTRING:
                                {
                                    return new PsoString7();
                                }
                            case ParMemberStringSubtype.ATFINALHASHSTRING:
                                {
                                    return new PsoString8();
                                }
                            default: throw new Exception($"Unsupported {nameof(entryInfo.SubType)}: {entryInfo.SubType} for {nameof(ParMemberType)}: {entryInfo.Type}");
                        }
                    }
                case ParMemberType.ENUM:
                    {
                        switch ((ParMemberEnumSubtype)entryInfo.SubType)
                        {
                            case ParMemberEnumSubtype._32BIT:
                                {
                                    var entryValue = new PsoEnumInt();
                                    entryValue.TypeInfo = GetEnumInfo(pso, entryInfo.ReferenceKey);
                                    return entryValue;
                                }
                            case ParMemberEnumSubtype._8BIT:
                                {
                                    var entryValue = new PsoEnumByte();
                                    entryValue.TypeInfo = GetEnumInfo(pso, entryInfo.ReferenceKey);
                                    return entryValue;
                                }
                            default: throw new Exception($"Unsupported {nameof(entryInfo.SubType)}: {entryInfo.SubType} for {nameof(ParMemberType)}: {entryInfo.Type}");
                        }
                    }
                case ParMemberType.BITSET:
                    {
                        switch ((ParMemberBitsetSubtype)entryInfo.SubType)
                        {
                            case ParMemberBitsetSubtype._32BIT:
                                {
                                    var entryValue = new PsoFlagsInt();
                                    var sidx = entryInfo.ReferenceKey & 0x0000FFFF;

                                    if (sidx != 0xfff)
                                    {
                                        var reftype = structureInfo.Entries[sidx];
                                        entryValue.TypeInfo = GetEnumInfo(pso, reftype.ReferenceKey);
                                    }


                                    return entryValue;
                                }
                            case ParMemberBitsetSubtype._16BIT:
                                {
                                    var entryValue = new PsoFlagsShort();
                                    var sidx = entryInfo.ReferenceKey & 0x0000FFFF;

                                    var reftype = structureInfo.Entries[sidx];
                                    entryValue.TypeInfo = GetEnumInfo(pso, reftype.ReferenceKey);

                                    return entryValue;
                                }
                            case ParMemberBitsetSubtype._8BIT:
                                {
                                    var entryValue = new PsoFlagsByte();
                                    var sidx = entryInfo.ReferenceKey & 0x0000FFFF;
                                    var reftype = structureInfo.Entries[sidx];
                                    entryValue.TypeInfo = GetEnumInfo(pso, reftype.ReferenceKey);
                                    return entryValue;
                                }
                            default: throw new Exception($"Unsupported {nameof(entryInfo.SubType)}: {entryInfo.SubType} for {nameof(ParMemberType)}: {entryInfo.Type}");
                        }
                    }
                case ParMemberType.UINT:
                    {
                        switch (entryInfo.SubType)
                        {
                            case 0: return new PsoUInt32();
                            case 1: return new PsoUInt32Hex();
                            default: throw new Exception($"Unsupported {nameof(entryInfo.SubType)}: {entryInfo.SubType} for {nameof(ParMemberType)}: {entryInfo.Type}");
                        }
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
                            default: throw new Exception($"Unsupported {nameof(entryInfo.SubType)}: {entryInfo.SubType} for {nameof(ParMemberType)}: {entryInfo.Type}");
                        }
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
                            default: throw new Exception($"Unsupported {nameof(entryInfo.SubType)}: {entryInfo.SubType} for {nameof(ParMemberType)}: {entryInfo.Type}");
                        }

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
                default:
                    throw new Exception($"Unsupported {nameof(ParMemberType)}: {entryInfo.Type}");
            }
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

    }
}
