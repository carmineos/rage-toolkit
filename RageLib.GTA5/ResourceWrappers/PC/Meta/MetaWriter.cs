// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.GTA5.ResourceWrappers.PC.Meta.Data;
using RageLib.GTA5.ResourceWrappers.PC.Meta.Definitions;
using RageLib.GTA5.ResourceWrappers.PC.Meta.Types;
using RageLib.Resources.Common;
using RageLib.Resources.GTA5;
using RageLib.Resources.GTA5.PC.Meta;
using System.Collections.Generic;
using System.IO;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta
{
    public class MetaWriter
    {
        private readonly MetaDefinitions metaDefinitions;

        private MetaFile meta;
        private ISet<int> usedStructureKeys = new HashSet<int>();
        private ISet<int> usedEnumKeys = new HashSet<int>();

        public MetaWriter(MetaDefinitions definitions)
        {
            metaDefinitions = definitions;
        }

        public void Write(IMetaValue value, string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Create))
                Write(value, fileStream);
        }

        public void Write(IMetaValue value, Stream fileStream)
        {
            var resource = new Resource7<MetaFile>();
            resource.Version = 2;
            resource.ResourceData = Build(value);
            resource.Save(fileStream);
        }

        private MetaFile Build(IMetaValue value)
        {
            meta = new MetaFile();
            meta.StructureInfos = metaDefinitions.BuildMetaStructureInfos();
            meta.EnumInfos = metaDefinitions.BuildMetaEnumInfos();

            var writer = new MetaDataWriter();
            writer.SelectBlockByNameHash(((MetaStructure)value).info.StructureNameHash);
            WriteStructure(writer, (MetaStructure)value);

            RemoveUnusedInfos();

            meta.DataBlocks = new ResourceSimpleArray<DataBlock>();
            foreach (var block in writer.Blocks)
            {
                var metaDataBlock = new DataBlock();
                metaDataBlock.StructureNameHash = block.NameHash;
                metaDataBlock.Data = block.GetSimpleArray();
                meta.DataBlocks.Add(metaDataBlock);
            }

            for (int i = 0; i < meta.DataBlocks.Count; i++)
            {
                if (meta.DataBlocks[i].StructureNameHash == ((MetaStructure)value).info.StructureNameHash)
                {
                    meta.RootBlockIndex = i + 1;
                }
            }

            return meta;
        }

        private void RemoveUnusedInfos()
        {
            for (int k = meta.StructureInfos.Count - 1; k >= 0; k--)
            {
                if (!usedStructureKeys.Contains(meta.StructureInfos[k].StructureKey))
                    meta.StructureInfos.RemoveAt(k);
            }

            for (int e = meta.EnumInfos.Count - 1; e >= 0; e--)
            {
                if (!usedEnumKeys.Contains(meta.EnumInfos[e].EnumKey))
                    meta.EnumInfos.RemoveAt(e);
            }

            if (meta.EnumInfos.Count < 1)
                meta.EnumInfos = null;
        }

        private void WriteStructure(MetaDataWriter writer, MetaStructure value)
        {
            var updateStack = new Stack<IMetaValue>();

            // build stack for update...
            var structuresToCheck = new Stack<MetaStructure>();
            structuresToCheck.Push(value);
            while (structuresToCheck.Count > 0)
            {
                var structureToCheck = structuresToCheck.Pop();

                // add structure to list of occurring structures
                usedStructureKeys.Add(structureToCheck.info.StructureKey);

                foreach (var structureEntryToCheck in structureToCheck.Values)
                {
                    if(structureEntryToCheck.Value is MetaEnumInt8 enumInt8)
                        usedEnumKeys.Add(enumInt8.info.EnumKey);
                    else if (structureEntryToCheck.Value is MetaEnumInt16 enumInt16)
                        usedEnumKeys.Add(enumInt16.info.EnumKey);
                    else if (structureEntryToCheck.Value is MetaEnumInt32 enumInt32)
                        usedEnumKeys.Add(enumInt32.info.EnumKey);
                    else if (structureEntryToCheck.Value is MetaFlagsInt8 flagInt8)
                        usedEnumKeys.Add(flagInt8.info.EnumKey);
                    else if (structureEntryToCheck.Value is MetaFlagsInt16 flagInt16)
                        usedEnumKeys.Add(flagInt16.info.EnumKey);
                    else if (structureEntryToCheck.Value is MetaFlagsInt32 flagInt32)
                        usedEnumKeys.Add(flagInt32.info.EnumKey);

                    else if (structureEntryToCheck.Value is MetaArray)
                    {
                        updateStack.Push(structureEntryToCheck.Value);

                        var arrayStructureEntryToCheck = structureEntryToCheck.Value as MetaArray;
                        if (arrayStructureEntryToCheck.Entries != null)
                        {
                            for (int k = arrayStructureEntryToCheck.Entries.Count - 1; k >= 0; k--)
                            {
                                var x = arrayStructureEntryToCheck.Entries[k];
                                if (x is MetaStructure)
                                {
                                    structuresToCheck.Push(x as MetaStructure);
                                }
                                else if (x is MetaGeneric)
                                {
                                    updateStack.Push(x);
                                    structuresToCheck.Push((MetaStructure)(x as MetaGeneric).Value);
                                }
                            }
                        }
                    }
                    else if (structureEntryToCheck.Value is MetaStringPointer)
                    {
                        updateStack.Push(structureEntryToCheck.Value);
                    }
                    else if(structureEntryToCheck.Value is MetaDataBlockPointer)
                    {
                        updateStack.Push(structureEntryToCheck.Value);
                    }
                    else if(structureEntryToCheck.Value is MetaGeneric)
                    {
                        updateStack.Push(structureEntryToCheck.Value);

                        var genericStructureEntryToCheck = structureEntryToCheck.Value as MetaGeneric;
                        structuresToCheck.Push((MetaStructure)genericStructureEntryToCheck.Value);
                    }
                    else if(structureEntryToCheck.Value is MetaStructure)
                    {
                        structuresToCheck.Push((MetaStructure)structureEntryToCheck.Value);
                    }
                }
            }

            // update structures...
            while (updateStack.Count > 0)
            {
                var v = updateStack.Pop();
                if (v is MetaArray)
                {
                    var arrayValue = (MetaArray)v;
                    if (arrayValue.Entries != null)
                    {
                        if (arrayValue.info.DataType == StructureEntryDataType.Structure)
                        {
                            // WORKAROUND
                            if (arrayValue.IsAlwaysAtZeroOffset)
                            {
                                writer.CreateBlockByNameHash(arrayValue.info.ReferenceKey);
                                writer.Position = writer.Length;
                            }
                            else
                            {
                                writer.SelectBlockByNameHash(arrayValue.info.ReferenceKey);
                                writer.Position = writer.Length;
                            }
                        }
                        else
                        {
                            writer.SelectBlockByNameHash((int)arrayValue.info.DataType);
                            writer.Position = writer.Length;
                        }
                        arrayValue.BlockIndex = writer.BlockIndex + 1;
                        arrayValue.Offset = (int)writer.Position;
                        arrayValue.NumberOfEntries = arrayValue.Entries.Count;
                        foreach (var entry in arrayValue.Entries)
                        {
                            entry.Write(writer);
                        }
                    }
                    else
                    {
                        arrayValue.BlockIndex = 0;
                        arrayValue.Offset = 0;
                        arrayValue.NumberOfEntries = 0;
                    }
                }
                else if (v is MetaStringPointer)
                {
                    var charPointerValue = (MetaStringPointer)v;
                    if (charPointerValue.Value != null)
                    {
                        writer.SelectBlockByNameHash(0x10);
                        writer.Position = writer.Length;
                        charPointerValue.DataBlockIndex = writer.BlockIndex + 1;
                        charPointerValue.DataOffset = (int)writer.Position;
                        charPointerValue.StringLength = charPointerValue.Value.Length;
                        charPointerValue.StringCapacity = charPointerValue.Value.Length + 1;
                        writer.Write(charPointerValue.Value);
                    }
                    else
                    {
                        charPointerValue.DataBlockIndex = 0;
                        charPointerValue.DataOffset = 0;
                        charPointerValue.StringLength = 0;
                        charPointerValue.StringCapacity = 0;
                    }
                }
                else if (v is MetaDataBlockPointer)
                {
                    var charPointerValue = (MetaDataBlockPointer)v;
                    if (charPointerValue.Data != null)
                    {
                        writer.CreateBlockByNameHash(0x11);
                        writer.Position = 0;
                        charPointerValue.BlockIndex = writer.BlockIndex + 1;
                        writer.Write(charPointerValue.Data);
                    }
                    else
                    {
                        charPointerValue.BlockIndex = 0;
                    }
                }
                else if (v is MetaGeneric)
                {
                    var genericValue = (MetaGeneric)v;
                    writer.SelectBlockByNameHash(((MetaStructure)genericValue.Value).info.StructureNameHash);
                    writer.Position = writer.Length;
                    genericValue.BlockIndex = writer.BlockIndex + 1;
                    genericValue.Offset = (int)writer.Position / 16;
                    genericValue.Value.Write(writer);
                }
            }

            // now only the root itself is left...
            writer.SelectBlockByNameHash(value.info.StructureNameHash);
            writer.Position = writer.Length;
            value.Write(writer);
        }
    }
}
