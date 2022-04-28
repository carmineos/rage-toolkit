// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.GTA5.ResourceWrappers.PC.Meta.Types;
using RageLib.Resources.Common;
using RageLib.Resources.GTA5;
using RageLib.Resources.GTA5.PC.Meta;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta
{
    public class MetaReader
    {
        public IMetaValue Read(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                return Read(fileStream);
            }
        }

        public IMetaValue Read(Stream fileStream)
        {
            var resource = new ResourceFile_GTA5_pc<MetaFile>();
            resource.Load(fileStream);
            return Parse(resource.ResourceData);
        }

        public IMetaValue Parse(MetaFile meta)
        {
            var blockKeys = new List<int>();
            var blocks = new List<List<IMetaValue>>();

            //////////////////////////////////////////////////
            // first step: flat conversion
            //////////////////////////////////////////////////

            foreach (var block in meta.DataBlocks)
            {
                blockKeys.Add(block.StructureNameHash);
                switch (block.StructureNameHash)
                {
                    case (int)StructureEntryDataType.StructurePointer:
                        blocks.Add(ReadBlock(block, () => new MetaGeneric())); // has no special type declaration in .meta -> pointer
                        break;
                    case (int)StructureEntryDataType.Int8:
                        blocks.Add(ReadBlock(block, () => new MetaSByte())); // char_array
                        break;
                    case (int)StructureEntryDataType.UInt8:
                        blocks.Add(ReadBlock(block, () => new MetaByte()));  // has no special type declaration in .meta -> string
                        break;
                    case (int)StructureEntryDataType.UInt16:
                        blocks.Add(ReadBlock(block, () => new MetaUInt16())); // probably short_array
                        break;
                    case (int)StructureEntryDataType.UInt32:
                        blocks.Add(ReadBlock(block, () => new MetaUInt32())); // int_array
                        break;
                    case (int)StructureEntryDataType.Float:
                        blocks.Add(ReadBlock(block, () => new MetaFloat())); // float_array
                        break;
                    case (int)StructureEntryDataType.Vector3:
                        blocks.Add(ReadBlock(block, () => new MetaVector3())); // vector3_array
                        break;
                    case (int)StructureEntryDataType.StringHash:
                        blocks.Add(ReadBlock(block, () => new MetaStringHash())); // probably list of <Item>HASH_OF_SOME_NAME</Item>
                        break;
                    default:
                        blocks.Add(ReadBlock(block, () => new MetaStructure(meta, GetInfo(meta, block.StructureNameHash)))); // has no special type declaration in .meta -> structure
                        break;
                }
            }

            //////////////////////////////////////////////////
            // second step: map references
            //////////////////////////////////////////////////

            var referenced = new HashSet<IMetaValue>();
            var stack = new Stack<IMetaValue>();
            foreach (var block in blocks)
            {
                foreach (var entry in block)
                {
                    stack.Push(entry);
                }
            }
            while (stack.Count > 0)
            {
                var entry = stack.Pop();
                if (entry is MetaArray)
                {
                    var arrayEntry = entry as MetaArray;
                    var realBlockIndex = arrayEntry.BlockIndex - 1;
                    if (realBlockIndex >= 0)
                    {
                        arrayEntry.Entries = new List<IMetaValue>();
                        var realEntryIndex = arrayEntry.Offset / GetSize(meta, blockKeys[realBlockIndex]);
                        for (int i = 0; i < arrayEntry.NumberOfEntries; i++)
                        {
                            var x = blocks[realBlockIndex][realEntryIndex + i];
                            arrayEntry.Entries.Add(x);
                            referenced.Add(x);
                        }
                    }
                }
                if (entry is MetaStringPointer)
                {
                    var charPointerEntry = entry as MetaStringPointer;
                    var realBlockIndex = charPointerEntry.DataBlockIndex - 1;
                    if (realBlockIndex >= 0)
                    {
                        string value = "";
                        for (int i = 0; i < charPointerEntry.StringLength; i++)
                        {
                            var x = (MetaSByte)blocks[realBlockIndex][i + charPointerEntry.DataOffset];
                            value += (char)x.Value;
                        }
                        charPointerEntry.Value = value;
                    }
                }
                if (entry is MetaDataBlockPointer)
                {
                    var dataPointerEntry = entry as MetaDataBlockPointer;
                    var realBlockIndex = dataPointerEntry.BlockIndex - 1;
                    if (realBlockIndex >= 0)
                    {
                        byte[] b = ToBytes(meta.DataBlocks[realBlockIndex].Data);
                        dataPointerEntry.Data = b;
                    }
                }
                if (entry is MetaGeneric)
                {
                    var genericEntry = entry as MetaGeneric;
                    var realBlockIndex = genericEntry.BlockIndex - 1;
                    var realEntryIndex = genericEntry.Offset * 16 / GetSize(meta, blockKeys[realBlockIndex]);
                    var x = blocks[realBlockIndex][realEntryIndex];
                    genericEntry.Value = x;
                    referenced.Add(x);
                }
                if (entry is MetaStructure)
                {
                    var structureEntry = entry as MetaStructure;
                    foreach (var x in structureEntry.Values)
                    {
                        stack.Push(x.Value);
                    }
                }
            }

            //////////////////////////////////////////////////
            // third step: find root
            //////////////////////////////////////////////////

            var rootSet = new HashSet<IMetaValue>();
            foreach (var x in blocks)
            {
                foreach (var y in x)
                {
                    if (y is MetaStructure && !referenced.Contains(y))
                    {
                        rootSet.Add(y);
                    }
                }
            }

            var res = rootSet.First();

            if (res != blocks[(int)meta.RootBlockIndex - 1][0])
                throw new System.Exception("wrong root block index");

            return res;
        }

        private List<IMetaValue> ReadBlock(DataBlock block, CreateMetaValueDelegate CreateMetaValue)
        {
            var result = new List<IMetaValue>();
            var reader = new DataReader(new MemoryStream(ToBytes(block.Data)));
            while (reader.Position < reader.Length)
            {
                var value = CreateMetaValue();
                value.Read(reader);
                result.Add(value);
            }
            return result;
        }

        private byte[] ToBytes(SimpleArray<byte> data)
        {
            return data.ToArray();
        }

        public static StructureInfo GetInfo(MetaFile meta, int structureKey)
        {
            StructureInfo info = null;
            foreach (var x in meta.StructureInfos)
                if (x.StructureNameHash == structureKey)
                    info = x;
            return info;
        }

        public int GetSize(MetaFile meta, int typeKey)
        {
            var type = (StructureEntryDataType)typeKey;

            switch (type)
            {
                case StructureEntryDataType.Int8:
                case StructureEntryDataType.UInt8:
                    return 1;
                
                case StructureEntryDataType.Int16:
                case StructureEntryDataType.UInt16:
                    return 2;
               
                case StructureEntryDataType.Int32:
                case StructureEntryDataType.UInt32:
                case StructureEntryDataType.Float:
                case StructureEntryDataType.StringHash:
                    return 4;

                case StructureEntryDataType.StructurePointer:
                    return 8;

                case StructureEntryDataType.Vector3:
                    return 16;

                default:
                    return GetInfo(meta, typeKey).StructureLength;
            }
        }
    }
}
