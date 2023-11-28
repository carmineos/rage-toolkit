﻿// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using System.IO;

namespace RageLib.GTA5.PSO
{
    public enum PsoSection : uint
    {
        PSIN = 0x5053494E,
        PMAP = 0x504D4150,
        PSCH = 0x50534348,
        PSIG = 0x50534947,
        STRF = 0x53545246,
        STRS = 0x53545253,
        STRE = 0x53545245,
        CHKS = 0x43484B53,
    }

    public class PsoFile
    {
        public PsoDataSection DataSection { get; set; }
        public PsoDataMappingSection DataMappingSection { get; set; }
        public PsoDefinitionSection DefinitionSection { get; set; }
        public PsoSTRFSection STRFSection { get; set; }
        public PsoSTRSSection STRSSection { get; set; }
        public PsoPSIGSection PSIGSection { get; set; }
        public PsoSTRESection STRESection { get; set; }
        public PsoCHKSSection CHKSSection { get; set; }

        public void Load(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                Load(stream);
        }

        public virtual void Load(Stream stream)
        {
            stream.Position = 0;

            var reader = new DataReader(stream, Endianness.BigEndian);
            while (reader.Position < reader.Length)
            {
                var ident = (PsoSection)reader.ReadUInt32();
                var length = reader.ReadInt32();

                reader.Position -= 8;

                var sectionData = reader.ReadBytes(length);
                var sectionStream = new MemoryStream(sectionData);
                var sectionReader = new DataReader(sectionStream, Endianness.BigEndian);

                switch (ident)
                {
                    case PsoSection.PSIN:
                        DataSection = new PsoDataSection();
                        DataSection.Read(sectionReader);
                        break;
                    case PsoSection.PMAP:
                        DataMappingSection = new PsoDataMappingSection();
                        DataMappingSection.Read(sectionReader);
                        break;
                    case PsoSection.PSCH:
                        DefinitionSection = new PsoDefinitionSection();
                        DefinitionSection.Read(sectionReader);
                        break;
                    case PsoSection.STRF:
                        STRFSection = new PsoSTRFSection();
                        STRFSection.Read(sectionReader);
                        break;
                    case PsoSection.STRS:
                        STRSSection = new PsoSTRSSection();
                        STRSSection.Read(sectionReader);
                        break;
                    case PsoSection.STRE:
                        STRESection = new PsoSTRESection();
                        STRESection.Read(sectionReader);
                        break;
                    case PsoSection.PSIG:
                        PSIGSection = new PsoPSIGSection();
                        PSIGSection.Read(sectionReader);
                        break;
                    case PsoSection.CHKS:
                        CHKSSection = new PsoCHKSSection();
                        CHKSSection.Read(sectionReader);
                        break;
                    default:
                        break;
                }
            }
        }

        public void Save(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Create))
                Save(stream);
        }

        public virtual void Save(Stream stream)
        {
            var writer = new DataWriter(stream, Endianness.BigEndian);
            if (DataSection != null) DataSection.Write(writer);
            if (DataMappingSection != null) DataMappingSection.Write(writer);
            if (DefinitionSection != null) DefinitionSection.Write(writer);
            if (STRFSection != null) STRFSection.Write(writer);
            if (STRSSection != null) STRSSection.Write(writer);
            if (PSIGSection != null) PSIGSection.Write(writer);
            if (STRESection != null) STRESection.Write(writer);
            if (CHKSSection != null) CHKSSection.Write(writer);
        }

        public static bool IsPSO(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                return IsPSO(stream);
        }

        public static bool IsPSO(Stream stream)
        {
            var reader = new DataReader(stream, Endianness.BigEndian);
            var ident = reader.ReadUInt32();
            stream.Position -= 4;
            return ident == (uint)PsoSection.PSIN;
        }
    }
}
