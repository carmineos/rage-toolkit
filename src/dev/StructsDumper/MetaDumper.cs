using RageLib.GTA5.Utilities;
using RageLib.Resources.GTA5.PC.Meta;
using RageLib.Services;

namespace StructsDumper.GTA5
{
    public class MetaDumper
    {
        public JenkinsDictionary joaatDictionary;
        public StreamWriter writer;

        public MetaDumper(JenkinsDictionary? dictionary = null)
        {
            joaatDictionary = dictionary ?? JenkinsDictionary.Shared;
        }

        public void Dump(string gameDirectoryName)
        {
            var tuple = MetaUtilities.GetAllStructureInfoAndEnumInfoFromMetas(gameDirectoryName);

            Dictionary<int, EnumInfo> enumInfos = tuple.Item2;
            Dictionary<int, StructureInfo> structureInfos = tuple.Item1;

            // Build C# type for enums
            Dictionary<int, StructureEntryDataType> enumTypes = new Dictionary<int, StructureEntryDataType>();
            foreach (var kvp in structureInfos)
            {
                var structure = kvp.Value;

                for (int i = 0; i < structure.Entries.Count; i++)
                {
                    var entry = structure.Entries[i];

                    if (entry.DataType == StructureEntryDataType.FlagsInt8 ||
                            entry.DataType == StructureEntryDataType.FlagsInt16 ||
                            entry.DataType == StructureEntryDataType.FlagsInt32 ||
                            entry.DataType == StructureEntryDataType.EnumInt8 ||
                            entry.DataType == StructureEntryDataType.EnumInt16 ||
                            entry.DataType == StructureEntryDataType.EnumInt32)
                    {
                        enumTypes.TryAdd(entry.ReferenceKey, entry.DataType);
                    }
                }
            }

            
            using (writer = new StreamWriter("MetaDumps.cs"))
            {
                foreach (var kvp in structureInfos)
                {
                    var structure = kvp.Value;
                    WriteStructureInfo(structure);
                }

                foreach (var kvp in enumInfos)
                {
                    var enumInfo = kvp.Value;
                    var enumName = joaatDictionary.TryGetValue(enumInfo.EnumNameHash, out string enumInfoName) ? enumInfoName : $"UNK_{enumInfo.EnumNameHash:X8}";

                    if (!enumTypes.TryGetValue(enumInfo.EnumNameHash, out StructureEntryDataType enumType))
                        throw new Exception();

                    switch (enumType)
                    {
                        case StructureEntryDataType.FlagsInt8:
                        case StructureEntryDataType.FlagsInt16:
                        case StructureEntryDataType.FlagsInt32:
                            WriteEnumInfoAsFlag(enumInfo, enumType);
                            break;
                        case StructureEntryDataType.EnumInt8:
                        case StructureEntryDataType.EnumInt16:                           
                        case StructureEntryDataType.EnumInt32:
                            WriteEnumInfoAsEnum(enumInfo, enumType);
                            break;

                    }
                }
            }
        }

        void WriteStructureInfo(StructureInfo structureInfo)
        {
            var structname = joaatDictionary.TryGetValue(structureInfo.StructureNameHash, out string structureInfoName) ? structureInfoName : $"UNK_{structureInfo.StructureNameHash:X8}";
            writer.WriteLine($"[StructLayout(LayoutKind.Explicit, Size={structureInfo.StructureLength}, CharSet=CharSet.Ansi)]");
            writer.WriteLine($"public unsafe struct {structname}");
            writer.WriteLine('{');

            for (int i = 0; i < structureInfo.Entries.Count; i++)
            {
                var entry = structureInfo.Entries[i];
                var typeName = GetCSharpTypeName(entry.DataType);

                if (entry.EntryNameHash == 0x100)
                {
                    // ArrayInfo
                    var arrayInfoEntry = entry;
                    i++;
                    entry = structureInfo.Entries[i];
                    typeName = GetCSharpTypeName(entry.DataType);
                    var arrayInfoDataType = GetCSharpTypeName(arrayInfoEntry.DataType);
                    
                    if (arrayInfoEntry.DataType == StructureEntryDataType.Structure)
                    {
                        arrayInfoDataType = joaatDictionary.TryGetValue(arrayInfoEntry.ReferenceKey, out string arrayInfoName) ? arrayInfoName : $"UNK_{arrayInfoEntry.ReferenceKey:X8}";
                    }
                    else if (arrayInfoEntry.DataType == StructureEntryDataType.StructurePointer)
                    {
                        arrayInfoDataType = joaatDictionary.TryGetValue(arrayInfoEntry.ReferenceKey, out string arrayInfoName) ? arrayInfoName : $"UNK_{arrayInfoEntry.ReferenceKey:X8}";
                        // TODO: Get Structure type name
                    }

                    typeName = $"{typeName}<{arrayInfoDataType}>";
                    var fieldName = joaatDictionary.TryGetValue(entry.EntryNameHash, out string entryInfoName) ? entryInfoName : $"UNK_{entry.EntryNameHash:X8}";

                    if (entry.DataType == StructureEntryDataType.ArrayLocal)
                    {
                        writer.WriteLine($"\t[FieldOffset({entry.DataOffset})] public fixed {arrayInfoDataType} {fieldName}[{entry.ReferenceKey}];");
                    }
                    else
                    {
                        writer.WriteLine($"\t[FieldOffset({entry.DataOffset})] public {typeName} {fieldName};");
                    }
                }
                else
                {
                    var fieldName = joaatDictionary.TryGetValue(entry.EntryNameHash, out string entryInfoName) ? entryInfoName : $"UNK_{entry.EntryNameHash:X8}";

                    if (entry.DataType == StructureEntryDataType.Structure ||
                        entry.DataType == StructureEntryDataType.FlagsInt8 ||
                        entry.DataType == StructureEntryDataType.FlagsInt16 ||
                        entry.DataType == StructureEntryDataType.FlagsInt32 ||
                        entry.DataType == StructureEntryDataType.EnumInt8 ||
                        entry.DataType == StructureEntryDataType.EnumInt16 ||
                        entry.DataType == StructureEntryDataType.EnumInt32)
                    {
                        typeName = joaatDictionary.TryGetValue(entry.ReferenceKey, out string structFieldName) ? structFieldName : $"UNK_{entry.ReferenceKey:X8}";
                    }

                    writer.WriteLine($"\t[FieldOffset({entry.DataOffset})] public {typeName} {fieldName};");

                }
            }

            writer.WriteLine('}');
            writer.WriteLine();
        }

        void WriteEnumInfoAsEnum(EnumInfo enumInfo, StructureEntryDataType dataType)
        {
            var enumName = joaatDictionary.TryGetValue(enumInfo.EnumNameHash, out string enumInfoName) ? enumInfoName : $"UNK_{enumInfo.EnumNameHash:X8}";

            writer.WriteLine($"public enum {enumName} : {GetCSharpTypeName(dataType)}");
            writer.WriteLine('{');

            foreach (var entry in enumInfo.Entries)
            {
                var fieldName = joaatDictionary.TryGetValue(entry.EntryNameHash, out string entryInfoName) ? entryInfoName : $"UNK_{entry.EntryNameHash:X8}";
                writer.WriteLine($"\t{fieldName} = {entry.EntryValue},");
            }

            writer.WriteLine('}');
            writer.WriteLine();
        }

        void WriteEnumInfoAsFlag(EnumInfo enumInfo, StructureEntryDataType dataType)
        {
            var enumName = joaatDictionary.TryGetValue(enumInfo.EnumNameHash, out string enumInfoName) ? enumInfoName : $"UNK_{enumInfo.EnumNameHash:X8}";

            writer.WriteLine("[Flags]");
            writer.WriteLine($"public enum {enumName} : {GetCSharpTypeName(dataType)}");
            writer.WriteLine('{');

            foreach (var entry in enumInfo.Entries)
            {
                var fieldName = joaatDictionary.TryGetValue(entry.EntryNameHash, out string entryInfoName) ? entryInfoName : $"UNK_{entry.EntryNameHash:X8}";
                
                if(entry.EntryValue < 0)
                    writer.WriteLine($"\t{fieldName} = 1 >> {Math.Abs(entry.EntryValue)},");
                else
                    writer.WriteLine($"\t{fieldName} = 1 << {entry.EntryValue},");
            }

            writer.WriteLine('}');
            writer.WriteLine();
        }

        static string GetCSharpTypeName(StructureEntryDataType dataType)
        {
            return dataType switch
            {
                StructureEntryDataType.Bool => "NativeBool",
                //StructureEntryDataType.Structure => throw new NotImplementedException(),
                //StructureEntryDataType.StructurePointer => throw new NotImplementedException(),
                StructureEntryDataType.Int8 => "sbyte",
                StructureEntryDataType.UInt8 => "byte",
                StructureEntryDataType.Int16 => "short",
                StructureEntryDataType.UInt16 => "ushort",
                StructureEntryDataType.Int32 => "int",
                StructureEntryDataType.UInt32 => "uint",
                StructureEntryDataType.Float => "float",
                StructureEntryDataType.Vector3 => "Vector3",
                StructureEntryDataType.Vector4 => "Vector4",
                //StructureEntryDataType.StringLocal => throw new NotImplementedException(),
                //StructureEntryDataType.StringPointer => throw new NotImplementedException(),
                StructureEntryDataType.StringHash => "JoaatHash",
                //StructureEntryDataType.ArrayLocal => throw new NotImplementedException(),
                //StructureEntryDataType.Array => throw new NotImplementedException(),
                //StructureEntryDataType.DataBlockPointer => throw new NotImplementedException(),
                StructureEntryDataType.EnumInt8 => "sbyte",
                StructureEntryDataType.EnumInt16 => "short",
                StructureEntryDataType.EnumInt32 => "int",
                StructureEntryDataType.FlagsInt8 => "int",
                StructureEntryDataType.FlagsInt16 => "short",
                StructureEntryDataType.FlagsInt32 => "int",
                _ => dataType.ToString(),
            };
        }
    }
}
