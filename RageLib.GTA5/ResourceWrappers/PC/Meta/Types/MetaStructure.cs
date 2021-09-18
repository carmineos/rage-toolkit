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

using RageLib.Data;
using RageLib.Resources.GTA5.PC.Meta;
using System;
using System.Collections.Generic;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Types
{
    public class MetaStructure : IMetaValue
    {
        private readonly MetaFile meta;
        public readonly StructureInfo info;

        public Dictionary<int, IMetaValue> Values { get; set; }

        public MetaStructure(MetaFile meta, StructureInfo info)
        {
            this.meta = meta;
            this.info = info;
        }

        public void Read(DataReader reader)
        {
            long position = reader.Position;

            this.Values = new Dictionary<int, IMetaValue>();
            for (int i = 0; i < info.Entries.Count; i++)
            {
                var entry = info.Entries[i];
                
                StructureEntryInfo arrayInfo = null;

                if (entry.EntryNameHash == 0x100)
                {
                    arrayInfo = entry;
                    entry = info.Entries[i + 1];
                    i++;
                }

                reader.Position = position + entry.DataOffset;
                switch (entry.DataType)
                {
                    case StructureEntryDataType.Array:
                        {
                            var entryValue = new MetaArray();
                            entryValue.info = info.Entries[entry.ReferenceTypeIndex];
                            entryValue.Read(reader);

                            if (entry.Unknown_9h != 0)
                            {
                                entryValue.IsAlwaysAtZeroOffset = true;
                            }

                            this.Values.Add(entry.EntryNameHash, entryValue);
                            break;
                        }
                    case StructureEntryDataType.Bool:
                        {
                            var entryValue = new MetaBool();
                            entryValue.Read(reader);
                            this.Values.Add(entry.EntryNameHash, entryValue);
                            break;
                        }
                    case StructureEntryDataType.Int8:
                        {
                            var entryValue = new MetaSByte();
                            entryValue.Read(reader);
                            this.Values.Add(entry.EntryNameHash, entryValue);
                            break;
                        }
                    case StructureEntryDataType.UInt8:
                        {
                            var entryValue = new MetaByte();
                            entryValue.Read(reader);
                            this.Values.Add(entry.EntryNameHash, entryValue);
                            break;
                        }
                    case StructureEntryDataType.Int16:
                        {
                            var entryValue = new MetaInt16();
                            entryValue.Read(reader);
                            this.Values.Add(entry.EntryNameHash, entryValue);
                            break;
                        }
                    case StructureEntryDataType.UInt16:
                        {
                            var entryValue = new MetaUInt16();
                            entryValue.Read(reader);
                            this.Values.Add(entry.EntryNameHash, entryValue);
                            break;
                        }
                    case StructureEntryDataType.Int32:
                        {
                            var entryValue = new MetaInt32();
                            entryValue.Read(reader);
                            this.Values.Add(entry.EntryNameHash, entryValue);
                            break;
                        }
                    case StructureEntryDataType.UInt32:
                        {
                            var entryValue = new MetaUInt32();
                            entryValue.Read(reader);
                            this.Values.Add(entry.EntryNameHash, entryValue);
                            break;
                        }
                    case StructureEntryDataType.StringLocal:
                        {
                            var entryValue = new MetaString(entry);
                            entryValue.Read(reader);
                            this.Values.Add(entry.EntryNameHash, entryValue);
                            break;
                        }
                    case StructureEntryDataType.StringPointer:
                        {
                            var entryValue = new MetaStringPointer();
                            entryValue.Read(reader);
                            this.Values.Add(entry.EntryNameHash, entryValue);
                            break;
                        }
                    case StructureEntryDataType.StringHash:
                        {
                            var entryValue = new MetaStringHash();
                            entryValue.Read(reader);
                            this.Values.Add(entry.EntryNameHash, entryValue);
                            break;
                        }
                    case StructureEntryDataType.Float:
                        {
                            var entryValue = new MetaFloat();
                            entryValue.Read(reader);
                            this.Values.Add(entry.EntryNameHash, entryValue);
                            break;
                        }
                    case StructureEntryDataType.Vector3:
                        {
                            var entryValue = new MetaVector3();
                            entryValue.Read(reader);
                            this.Values.Add(entry.EntryNameHash, entryValue);
                            break;
                        }
                    case StructureEntryDataType.Vector4:
                        {
                            var entryValue = new MetaVector4();
                            entryValue.Read(reader);
                            this.Values.Add(entry.EntryNameHash, entryValue);
                            break;
                        }
                    case StructureEntryDataType.ArrayLocal:
                        {
                            MetaArrayLocal entryValue;
                            
                            // TODO: Check why OpenIV sometimes doesn't handle these well
                            switch (arrayInfo.DataType)
                            {
                                case StructureEntryDataType.UInt8:
                                    entryValue = new MetaArrayLocal<byte>(entry);
                                    break;
                                case StructureEntryDataType.UInt16:
                                    entryValue = new MetaArrayLocal<ushort>(entry);
                                    break;
                                case StructureEntryDataType.UInt32:
                                    entryValue = new MetaArrayLocal<uint>(entry);
                                    break;
                                case StructureEntryDataType.Float:
                                    entryValue = new MetaArrayLocal<float>(entry);
                                    break;
                                default:
                                    throw new Exception($"Unsupported ArrayType: {arrayInfo.DataType} for Type: {entry.DataType}");
                            }

                            entryValue.Read(reader);
                            this.Values.Add(entry.EntryNameHash, entryValue);
                            break;
                        }
                    case StructureEntryDataType.EnumInt8:
                        {
                            var entryValue = new MetaEnumInt8();
                            entryValue.info = GetEnumInfo(meta, entry.ReferenceKey);
                            entryValue.Read(reader);
                            this.Values.Add(entry.EntryNameHash, entryValue);
                            break;
                        }
                    case StructureEntryDataType.EnumInt16:
                        {
                            var entryValue = new MetaEnumInt16();
                            entryValue.info = GetEnumInfo(meta, entry.ReferenceKey);
                            entryValue.Read(reader);
                            this.Values.Add(entry.EntryNameHash, entryValue);
                            break;
                        }
                    case StructureEntryDataType.EnumInt32:
                        {
                            var entryValue = new MetaEnumInt32();
                            entryValue.Read(reader);
                            this.Values.Add(entry.EntryNameHash, entryValue);
                            entryValue.info = GetEnumInfo(meta, entry.ReferenceKey);
                            break;
                        }
                    case StructureEntryDataType.FlagsInt8:
                        {
                            var entryValue = new MetaFlagsInt8();
                            entryValue.Read(reader);
                            entryValue.info = GetEnumInfo(meta, entry.ReferenceKey);
                            this.Values.Add(entry.EntryNameHash, entryValue);
                            break;
                        }
                    case StructureEntryDataType.FlagsInt16: // flags!
                        {
                            var entryValue = new MetaFlagsInt16();
                            entryValue.Read(reader);
                            entryValue.info = GetEnumInfo(meta, entry.ReferenceKey);
                            this.Values.Add(entry.EntryNameHash, entryValue);
                            break;
                        }
                    case StructureEntryDataType.FlagsInt32: // flags
                        {
                            var entryValue = new MetaFlagsInt32();
                            entryValue.Read(reader);
                            entryValue.info = GetEnumInfo(meta, entry.ReferenceKey);
                            this.Values.Add(entry.EntryNameHash, entryValue);
                            break;
                        }
                    case StructureEntryDataType.DataBlockPointer:
                        {
                            var entryValue = new MetaDataBlockPointer(entry);
                            entryValue.Read(reader);
                            this.Values.Add(entry.EntryNameHash, entryValue);
                            break;
                        }
                    case StructureEntryDataType.Structure:
                        {
                            var entryValue = new MetaStructure(meta, GetStructureInfo(meta, entry.ReferenceKey));
                            entryValue.Read(reader);
                            this.Values.Add(entry.EntryNameHash, entryValue);
                            break;
                        }
                    case StructureEntryDataType.StructurePointer:
                        {
                            var entryValue = new MetaGeneric();
                            entryValue.Read(reader);
                            this.Values.Add(entry.EntryNameHash, entryValue);
                            break;
                        }
                    default:
                        throw new Exception("Unknown Type");
                }
            }

            reader.Position = position + info.StructureLength;
        }

        public static StructureInfo GetStructureInfo(MetaFile meta, int structureKey)
        {
            foreach (var structureInfo in meta.StructureInfos)
                if (structureInfo.StructureNameHash == structureKey)
                    return structureInfo;
            return null;
        }

        public static EnumInfo GetEnumInfo(MetaFile meta, int structureKey)
        {
            foreach (var enumInfo in meta.EnumInfos)
                if (enumInfo.EnumNameHash == structureKey)
                    return enumInfo;
            return null;
        }

        public void Write(DataWriter writer)
        {
            long position = writer.Position;

            writer.Write(new byte[info.StructureLength]);
            writer.Position = position;

            foreach (var entry in info.Entries)
            {
                if (entry.EntryNameHash != 0x100)
                {
                    writer.Position = position + entry.DataOffset;
                    this.Values[entry.EntryNameHash].Write(writer);
                }
            }
            writer.Position = position + info.StructureLength;
        }
    }
}
