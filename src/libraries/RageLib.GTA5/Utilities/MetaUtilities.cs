// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Compression;
using RageLib.Cryptography;
using RageLib.GTA5.Archives;
using RageLib.GTA5.ArchiveWrappers;
using RageLib.GTA5.Cryptography;
using RageLib.GTA5.PSO;
using RageLib.GTA5.Resources.PC;
using RageLib.Hash;
using RageLib.Resources.GTA5;
using RageLib.Resources.GTA5.PC.Meta;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Xml;

namespace RageLib.GTA5.Utilities
{
    public static class MetaUtilities
    {
        public static HashSet<int> GetAllHashesFromMetas(string gameDirectoryName)
        {
            var hashes = new HashSet<int>();
            foreach (var hash in GetAllHashesFromResourceMetas(gameDirectoryName))
                hashes.Add(hash);
            foreach (var hash in GetAllHashesFromPsoMetas(gameDirectoryName))
                hashes.Add(hash);
            return hashes;
        }

        public static HashSet<int> GetAllHashesFromResourceMetas(string gameDirectoryName)
        {
            var hashes = new HashSet<int>();
            ArchiveUtilities.ForEachResourceFile(gameDirectoryName, (fullFileName, file, encryption) =>
            {
                if (file.Name.EndsWith(ResourceFileTypes_GTA5_pc.Meta.Extension, StringComparison.OrdinalIgnoreCase) ||
                file.Name.EndsWith(ResourceFileTypes_GTA5_pc.Types.Extension, StringComparison.OrdinalIgnoreCase) ||
                file.Name.EndsWith(ResourceFileTypes_GTA5_pc.Maps.Extension, StringComparison.OrdinalIgnoreCase)
                )
                {
                    var stream = new MemoryStream();
                    file.Export(stream);
                    stream.Position = 0;

                    var resource = new Resource7<MetaData>();
                    resource.Load(stream);

                    var meta = resource.ResourceData;
                    if (meta.StructureInfos != null)
                    {
                        foreach (var structureInfo in meta.StructureInfos)
                        {
                            hashes.Add(structureInfo.StructureKey);
                            hashes.Add(structureInfo.StructureNameHash);
                            foreach (var structureEntryInfo in structureInfo.Entries)
                            {
                                if (structureEntryInfo.EntryNameHash != 0x100)
                                {
                                    hashes.Add(structureEntryInfo.EntryNameHash);
                                }
                            }
                        }
                    }

                    if (meta.EnumInfos != null)
                    {
                        foreach (var enumInfo in meta.EnumInfos)
                        {
                            hashes.Add(enumInfo.EnumKey);
                            hashes.Add(enumInfo.EnumNameHash);
                            foreach (var enumEntryInfo in enumInfo.Entries)
                            {
                                hashes.Add(enumEntryInfo.EntryNameHash);
                            }
                        }
                    }

                    Console.WriteLine(file.Name);
                }
            });
            return hashes;
        }

        public static HashSet<int> GetAllHashesFromPsoMetas(string gameDirectoryName)
        {
            var hashes = new HashSet<int>();
            ArchiveUtilities.ForEachBinaryFile(gameDirectoryName, (fullFileName, file, encryption) =>
            {
                if (file.Name.EndsWith(".ymf") || file.Name.EndsWith(".ymt"))
                {
                    var cleanStream = new MemoryStream();
                    (file as RageArchiveBinaryFileWrapper7).ExportUncompressed(cleanStream);
                    cleanStream.Position = 0;

                    if (PsoFile.IsPSO(cleanStream))
                    {
                        PsoFile pso = new PsoFile();
                        pso.Load(cleanStream);

                        foreach (var info in pso.DefinitionSection.EntriesIdx)
                        {
                            hashes.Add(info.NameHash);
                        }
                        foreach (var info in pso.DefinitionSection.Entries)
                        {
                            if (info is PsoStructureInfo)
                            {
                                var structureInfo = (PsoStructureInfo)info;
                                foreach (var entryInfo in structureInfo.Entries)
                                {
                                    hashes.Add(entryInfo.EntryNameHash);
                                }
                            }

                            if (info is PsoEnumInfo)
                            {
                                var enumInfo = (PsoEnumInfo)info;
                                foreach (var entryInfo in enumInfo.Entries)
                                {
                                    hashes.Add(entryInfo.EntryNameHash);
                                }
                            }
                        }

                        Console.WriteLine(file.Name);
                    }
                }
            });
            return hashes;
        }
        
        public static HashSet<string> GetAllStringsFromAllXmls(string gameDirectoryName)
        {
            var xmlStrings = new HashSet<string>();

            ArchiveUtilities.ForEachBinaryFile(gameDirectoryName, (fullFileName, file, encryption) =>
            {
                if (file.Name.EndsWith(".meta", StringComparison.OrdinalIgnoreCase) ||
                 file.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    var cleanStream = new MemoryStream();
                    (file as RageArchiveBinaryFileWrapper7).ExportUncompressed(cleanStream);
                    cleanStream.Position = 0;

                    foreach (string xmlString in GetAllStringsFromXml(cleanStream))
                    {
                        xmlStrings.Add(xmlString);
                    }

                    Console.WriteLine(file.Name);
                }
            });

            return xmlStrings;
        }

        public static HashSet<string> GetAllStringsFromXml(string xmlFileName)
        {
            using (var xmlFileStream = new FileStream(xmlFileName, FileMode.Open, FileAccess.Read))
            {
                return GetAllStringsFromXml(xmlFileStream);
            }
        }

        public static HashSet<string> GetAllStringsFromXml(Stream xmlFileStream)
        {
            var xmlStrings = new HashSet<string>();
            var separators = new char[] { ' ', '\t', '\n' };

            var document = new XmlDocument();
            document.Load(xmlFileStream);

            var stack = new Stack<XmlNode>();
            stack.Push(document.DocumentElement);
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                foreach (XmlNode descendantNode in node.SelectNodes("descendant::*"))
                {
                    stack.Push(descendantNode);
                }
                if (node.NodeType == XmlNodeType.Text)
                {
                    xmlStrings.Add(node.InnerText.Trim());

                    // for flags...
                    string[] splitted = node.InnerText.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var x in splitted)
                    {
                        xmlStrings.Add(x.Trim());
                    }
                }
                if (node.NodeType == XmlNodeType.Element)
                {
                    xmlStrings.Add(node.Name.Trim());
                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        xmlStrings.Add(attribute.Name.Trim());
                        xmlStrings.Add(attribute.Value.Trim());
                    }
                }
            }

            return xmlStrings;
        }

        public static HashSet<string> GetAllStringsThatMatchAHash(IEnumerable<string> listOfStrings, ISet<int> listOfHashes)
        {
            var matchingStrings = new HashSet<string>();
            foreach (var s in listOfStrings)
            {
                int hash = unchecked((int)Jenkins.Hash(s));
                if (listOfHashes.Contains(hash))
                    matchingStrings.Add(s);
            }
            return matchingStrings;
        }

        public static Tuple<Dictionary<int, StructureInfo>, Dictionary<int, EnumInfo>> GetAllStructureInfoAndEnumInfoFromMetas(string gameDirectoryName)
        {
            Dictionary<int, StructureInfo> structureInfos = new Dictionary<int, StructureInfo>();
            Dictionary<int, EnumInfo> enumInfos = new Dictionary<int, EnumInfo>();

            ArchiveUtilities.ForEachResourceFile(gameDirectoryName, (fullFileName, file, encryption) =>
            {
                if (file.Name.EndsWith(ResourceFileTypes_GTA5_pc.Meta.Extension, StringComparison.OrdinalIgnoreCase) ||
                file.Name.EndsWith(ResourceFileTypes_GTA5_pc.Types.Extension, StringComparison.OrdinalIgnoreCase) ||
                file.Name.EndsWith(ResourceFileTypes_GTA5_pc.Maps.Extension, StringComparison.OrdinalIgnoreCase)
                )
                {
                    var stream = new MemoryStream();
                    file.Export(stream);
                    stream.Position = 0;

                    var resource = new Resource7<MetaData>();
                    resource.Load(stream);

                    var meta = resource.ResourceData;

                    if (meta.StructureInfos != null)
                    {
                        foreach (var structureInfo in meta.StructureInfos)
                        {
                            structureInfos.TryAdd(structureInfo.StructureKey, structureInfo);
                        }
                    }

                    if (meta.EnumInfos != null)
                    {
                        foreach (var enumInfo in meta.EnumInfos)
                        {
                            enumInfos.TryAdd(enumInfo.EnumKey, enumInfo);
                        }
                    }

                    Console.WriteLine(file.Name);
                }
            });

            return new Tuple<Dictionary<int, StructureInfo>, Dictionary<int, EnumInfo>>(structureInfos, enumInfos);
        }

        public static Tuple<Dictionary<int, PsoStructureInfo>, Dictionary<int, PsoEnumInfo>> GetAllStructureInfoAndEnumInfoFromPsoMetas(string gameDirectoryName)
        {
            Dictionary<int, PsoStructureInfo> structureInfos = new Dictionary<int, PsoStructureInfo>();
            Dictionary<int, PsoEnumInfo> enumInfos = new Dictionary<int, PsoEnumInfo>();

            ArchiveUtilities.ForEachBinaryFile(gameDirectoryName, (fullFileName, file, encryption) =>
            {
                if (file.Name.EndsWith(".ymf") || file.Name.EndsWith(".ymt"))
                {
                    var cleanStream = new MemoryStream();
                    (file as RageArchiveBinaryFileWrapper7).ExportUncompressed(cleanStream);
                    cleanStream.Position = 0;

                    if (PsoFile.IsPSO(cleanStream))
                    {
                        PsoFile pso = new PsoFile();
                        pso.Load(cleanStream);

                        for (int i = 0; i < pso.DefinitionSection.Count; i++)
                        {
                            var id = pso.DefinitionSection.EntriesIdx[i];

                            var info = pso.DefinitionSection.Entries[i];

                            if (info is PsoStructureInfo structureInfo)
                            {
                                structureInfos.TryAdd(id.NameHash, structureInfo);
                            }

                            if (info is PsoEnumInfo enumInfo)
                            {
                                enumInfos.TryAdd(id.NameHash, enumInfo);
                            }
                        }

                        Console.WriteLine(file.Name);
                    }
                }
            });

            return new Tuple<Dictionary<int, PsoStructureInfo>, Dictionary<int, PsoEnumInfo>>(structureInfos, enumInfos);
        }
    }
}
