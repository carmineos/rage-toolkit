﻿// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using RageLib.Helpers.Xml;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Definitions
{
    [Serializable]
    public class MetaDefinitions
    {
        [XmlElement("Structure")]
        public List<MetaStructureXml> Structures { get; set; }

        [XmlElement("Enum")]
        public List<MetaEnumXml> Enums { get; set; }

        public static MetaDefinitions LoadEmbedded()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream xmlStream = assembly.GetManifestResourceStream("RageLib.GTA5.ResourceWrappers.PC.Meta.Definitions.MetaDefinitions.xml"))
            {
                var ser = new XmlSerializer(typeof(MetaDefinitions));
                return (MetaDefinitions)ser.Deserialize(xmlStream);
            }
        }
    }

    [Serializable]
    public class MetaStructureXml
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
        public int Key { get; set; }

        [XmlAttribute("Key")]
        public string KeyAsHex
        {
            get { return HexConverter.ToHex(Key); }
            set { Key = HexConverter.ToInt32(value); }
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
        public List<MetaStructureEntryXml> Entries { get; set; }
    }

    [Serializable]
    public class MetaStructureEntryXml
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
        public int Offset { get; set; }

        [XmlAttribute("Type")]
        public int Type { get; set; }

        [XmlIgnore]
        public int TypeHash { get; set; }

        [XmlAttribute("TypeHash")]
        public string TypeHashAsHex
        {
            get { return HexConverter.ToHex(TypeHash); }
            set { TypeHash = HexConverter.ToInt32(value); }
        }

        [XmlIgnore]
        public int Unknown { get; set; }

        [XmlAttribute("Unknown")]
        public string UnknownAsHex
        {
            get { return HexConverter.ToHex(Unknown); }
            set { Unknown = HexConverter.ToInt32(value); }
        }

        [XmlElement("ArrayType")]
        public MetaStructureArrayTypeXml ArrayType { get; set; }
    }

    [Serializable]
    public class MetaStructureArrayTypeXml
    {
        [XmlAttribute("Type")]
        public int Type { get; set; }

        [XmlIgnore]
        public int TypeHash { get; set; }

        [XmlAttribute("TypeHash")]
        public string TypeHashAsHex
        {
            get { return HexConverter.ToHex(TypeHash); }
            set { TypeHash = HexConverter.ToInt32(value); }
        }

        [XmlElement("ArrayType")]
        public MetaStructureArrayTypeXml ArrayType { get; set; }
    }

    [Serializable]
    public class MetaEnumXml
    {
        [XmlIgnore]
        public int Key { get; set; }

        [XmlAttribute("Key")]
        public string KeyAsHex
        {
            get { return HexConverter.ToHex(Key); }
            set { Key = HexConverter.ToInt32(value); }
        }


        [XmlIgnore]
        public int NameHash { get; set; }

        [XmlAttribute("NameHash")]
        public string NameHashAsHex
        {
            get { return HexConverter.ToHex(NameHash); }
            set { NameHash = HexConverter.ToInt32(value); }
        }

        [XmlElement("EnumEntry")]
        public List<MetaEnumEntryXml> Entries { get; set; }
    }

    [Serializable]
    public class MetaEnumEntryXml
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
