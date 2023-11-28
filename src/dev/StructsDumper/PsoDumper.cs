using RageLib.GTA5.PSO;
using RageLib.GTA5.Utilities;
using RageLib.Services;

namespace StructsDumper.GTA5
{
    public class PsoDumper
    {
        public JenkinsDictionary joaatDictionary;
        public StreamWriter writer;

        public PsoDumper(JenkinsDictionary? dictionary = null)
        {
            joaatDictionary = dictionary ?? JenkinsDictionary.Shared;
        }

        public void Dump(string gameDirectoryName)
        {
            var tuple = MetaUtilities.GetAllStructureInfoAndEnumInfoFromPsoMetas(gameDirectoryName);

            Dictionary<int, PsoEnumInfo> enumInfos = tuple.Item2;
            Dictionary<int, PsoStructureInfo> structureInfos = tuple.Item1;

            using (writer = new StreamWriter("PsoDumps.cs"))
            {
                foreach (var kvp in structureInfos)
                {
                    WriteStructureInfo(kvp.Key, kvp.Value);
                }

                foreach (var kvp in enumInfos)
                {
                    var enumInfo = kvp.Value;
                    var enumName = joaatDictionary.TryGetValue(kvp.Key, out string enumInfoName) ? enumInfoName : $"UNK_{kvp.Key:X8}";
                    writer.WriteLine($"public enum {enumName}");
                    writer.WriteLine('{');

                    foreach (var entry in enumInfo.Entries)
                    {
                        var fieldName = joaatDictionary.TryGetValue(entry.EntryNameHash, out string entryInfoName) ? entryInfoName : $"UNK_{entry.EntryNameHash:X8}"; ;
                        writer.WriteLine($"\t{fieldName} = {entry.EntryKey},");
                    }

                    writer.WriteLine('}');
                    writer.WriteLine();
                }
            }
        }

        void WriteStructureInfo(int structureKey, PsoStructureInfo structureInfo)
        {
            var structname = joaatDictionary.TryGetValue(structureKey, out string structureInfoName) ? structureInfoName : $"UNK_{structureKey:X8}";

            writer.WriteLine($"[StructLayout(LayoutKind.Explicit, Size={structureInfo.StructureLength}, CharSet=CharSet.Ansi)]");
            writer.WriteLine($"public unsafe struct {structname}");
            writer.WriteLine('{');

            for (int i = 0; i < structureInfo.Entries.Count; i++)
            {
                var entry = structureInfo.Entries[i];
                var typeName = GetCSharpTypeName(entry.Type, entry.SubType);

                if (entry.EntryNameHash == 0x100)
                {
                    // ArrayInfo
                    var arrayInfoEntry = entry;
                    i++;
                    entry = structureInfo.Entries[i];
                    var arrayInfoDataType = GetCSharpTypeName(arrayInfoEntry.Type, arrayInfoEntry.SubType);

                    typeName = $"{GetCSharpTypeName(entry.Type, entry.SubType)}<{arrayInfoDataType}>";

                    var fieldName = joaatDictionary.TryGetValue(entry.EntryNameHash, out string entryInfoName) ? entryInfoName : $"UNK_{entry.EntryNameHash:X8}";

                    if (entry.Type == ParMemberType.ARRAY && ((ParMemberArraySubtype)entry.SubType) == ParMemberArraySubtype.MEMBER)
                    {
                        var fixedSize = entry.ReferenceKey >> 16 & 0xFFFF;
                        var other = entry.ReferenceKey & 0xFFFF;
                        writer.WriteLine($"\t[FieldOffset({entry.DataOffset})] public fixed {arrayInfoDataType} {fieldName}[{fixedSize}]; // {other}");
                    }
                    else
                    {
                        writer.WriteLine($"\t[FieldOffset({entry.DataOffset})] public {typeName} {fieldName};");
                    }
                }
                else
                {
                    var fieldName = joaatDictionary.TryGetValue(entry.EntryNameHash, out string entryInfoName) ? entryInfoName : $"UNK_{entry.EntryNameHash:X8}";

                    if (entry.Type == ParMemberType.STRUCT ||
                        entry.Type == ParMemberType.ENUM ||
                        entry.Type == ParMemberType.BITSET)
                    {
                        typeName = joaatDictionary.TryGetValue(entry.ReferenceKey, out string structFieldName) ? structFieldName : $"UNK_{entry.ReferenceKey:X8}";
                    }

                    writer.WriteLine($"\t[FieldOffset({entry.DataOffset})] public {typeName} {fieldName};");
                }
            }

            writer.WriteLine('}');
            writer.WriteLine();
        }

        private string GetCSharpTypeName(ParMemberType type, byte subType)
        {
            return type switch
            {
                ParMemberType.BOOL => "NativeBool",
                ParMemberType.CHAR => "sbyte",
                ParMemberType.UCHAR => "byte",
                ParMemberType.SHORT => "short",
                ParMemberType.USHORT => "ushort",
                ParMemberType.INT => "int",
                ParMemberType.UINT => "uint",
                ParMemberType.FLOAT => "float",
                ParMemberType.VECTOR2 => "Vector2",
                ParMemberType.VECTOR3 => "Vector3",
                ParMemberType.VECTOR4 => "Vector4",
                ParMemberType.STRING => "JoaatHash",
                //ParMemberType.STRUCT => throw new NotImplementedException(),
                //ParMemberType.ARRAY => throw new NotImplementedException(),
                //ParMemberType.ENUM => throw new NotImplementedException(),
                //ParMemberType.BITSET => throw new NotImplementedException(),
                //ParMemberType.MAP => throw new NotImplementedException(),
                //ParMemberType.MATRIX34 => throw new NotImplementedException(),
                //ParMemberType.MATRIX44 => throw new NotImplementedException(),
                //ParMemberType.VEC2V => throw new NotImplementedException(),
                //ParMemberType.VEC3V => throw new NotImplementedException(),
                //ParMemberType.VEC4V => throw new NotImplementedException(),
                //ParMemberType.MAT33V => throw new NotImplementedException(),
                //ParMemberType.MAT34V => throw new NotImplementedException(),
                //ParMemberType.MAT44V => throw new NotImplementedException(),
                //ParMemberType.SCALARV => throw new NotImplementedException(),
                //ParMemberType.BOOLV => throw new NotImplementedException(),
                //ParMemberType.VECBOOLV => throw new NotImplementedException(),
                //ParMemberType.PTRDIFFT => throw new NotImplementedException(),
                //ParMemberType.SIZET => throw new NotImplementedException(),
                ParMemberType.FLOAT16 => "Half",
                ParMemberType.INT64 => "long",
                ParMemberType.UINT64 => "ulong",
                ParMemberType.DOUBLE => "double",
                _ => type.ToString(),
            };
        }
    }
}
