// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using RageLib.Helpers.Xml;

namespace RageLib.GTA5.PSOWrappers.Xml
{
    [Serializable]
    public class PsoDefinitionXml
    {
        [XmlElement("Structure")]
        public List<PsoStructureXml> Structures { get; set; }

        [XmlElement("Enum")]
        public List<PsoEnumXml> Enums { get; set; }
    }

    [Serializable]
    public class PsoStructureXml
    {
        [XmlIgnore]
        public int NameHash { get; set; }

        [XmlAttribute("NameHash")]
        public string NameHashAsHex
        {
            get { return HexConverter.ToHex(NameHash); }
            set { NameHash = HexConverter.ToInt32(value); }
        }
        
        [XmlIgnore]
        public int Unknown { get; set; }

        [XmlAttribute("Unknown")]
        public string UnknownAsHex
        {
            get { return HexConverter.ToHex(Unknown); }
            set { Unknown = HexConverter.ToInt32(value); }
        }

        [XmlAttribute("Length")]
        public int Length { get; set; }

        [XmlElement("StructureEntry")]
        public List<PsoStructureEntryXml> Entries { get; set; }
    }

    [Serializable]
    public class PsoStructureEntryXml
    {
        [XmlIgnore]
        public int NameHash { get; set; }

        [XmlAttribute("NameHash")]
        public string NameHashAsHex
        {
            get { return HexConverter.ToHex(NameHash); }
            set { NameHash = HexConverter.ToInt32(value); }
        }

        [XmlAttribute("Offset")]
        public ushort Offset { get; set; }

        [XmlAttribute("Type")]
        public byte Type { get; set; }

        [XmlAttribute("SubType")]
        public byte SubType { get; set; }

        [XmlIgnore]
        public int TypeHash { get; set; }

        [XmlAttribute("TypeHash")]
        public string TypeHashAsHex
        {
            get { return HexConverter.ToHex(TypeHash); }
            set { TypeHash = HexConverter.ToInt32(value); }
        }

        [XmlElement("ArrayType")]
        public PsoStructureEntryXml ArrayType { get; set; }
    }

    [Serializable]
    public class PsoEnumXml
    {
        [XmlIgnore]
        public int NameHash { get; set; }

        [XmlAttribute("NameHash")]
        public string NameHashAsHex
        {
            get { return HexConverter.ToHex(NameHash); }
            set { NameHash = HexConverter.ToInt32(value); }
        }

        [XmlElement("EnumEntry")]
        public List<PsoEnumEntryXml> Entries { get; set; }
    }

    [Serializable]
    public class PsoEnumEntryXml
    {
        [XmlIgnore]
        public int NameHash { get; set; }

        [XmlAttribute("NameHash")]
        public string NameHashAsHex
        {
            get { return HexConverter.ToHex(NameHash); }
            set { NameHash = HexConverter.ToInt32(value); }
        }

        [XmlAttribute("Value")]
        public int Value { get; set; }
    }
}
