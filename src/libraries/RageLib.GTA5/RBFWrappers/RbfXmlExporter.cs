﻿// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.GTA5.RBF;
using RageLib.GTA5.RBF.Types;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace RageLib.GTA5.RBFWrappers
{
    public class RbfXmlExporter
    {
        public void Export(RbfStructure value, string xmlFileName)
        {
            using (var xmlFileStream = new FileStream(xmlFileName, FileMode.Create))
            {
                Export(value, xmlFileStream);
            }
        }

        public void Export(RbfStructure value, Stream xmlFileStream)
        {
            var writer = XmlWriter.Create(xmlFileStream, new XmlWriterSettings() { Indent = true, Encoding = Encoding.UTF8 });
            writer.WriteStartDocument();
            writer.WriteStartElement(value.Name);
            WriteStructureContentXml(value, writer);
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
        }

        private void WriteStructureContentXml(RbfStructure value, XmlWriter writer)
        {
            foreach (var child in value.Children)
            {
                if (child is RbfBytes)
                {
                    var bytesChild = (RbfBytes)child;

                    var contentField = (RbfString)null;
                    foreach (var xyz in value.Children)
                    {
                        if (xyz.Name != null && xyz.Name.Equals("content"))
                        {
                            contentField = (RbfString)xyz;
                        }
                    }

                    if (contentField != null)
                    {
                        if (contentField.Value.Equals("char_array"))
                        {
                            var sb = new StringBuilder();
                            sb.AppendLine("");
                            foreach (var k in bytesChild.Value)
                            {
                                sb.AppendLine(k.ToString());
                            }
                            writer.WriteString(sb.ToString());

                        }
                        else if (contentField.Value.Equals("short_array"))
                        {
                            var sb = new StringBuilder();
                            var valueReader = new DataReader(new MemoryStream(bytesChild.Value));
                            while (valueReader.Position < valueReader.Length)
                            {
                                var y = valueReader.ReadUInt16();
                                sb.AppendLine(y.ToString());
                            }
                            writer.WriteString(sb.ToString());
                        }
                        else
                        {
                            throw new Exception("Unexpected content type");
                        }
                    }
                    else
                    {
                        string stringValue = Encoding.ASCII.GetString(bytesChild.Value);
                        writer.WriteString(stringValue.Substring(0, stringValue.Length - 1));
                    }
                }

                if (child is RbfFloat)
                {
                    writer.WriteStartElement(child.Name);
                    var floatChild = (RbfFloat)child;
                    var s1 = string.Format(CultureInfo.InvariantCulture, "{0:0.0###########}", floatChild.Value);
                    writer.WriteAttributeString("value", s1);
                    writer.WriteEndElement();
                }

                if (child is RbfString)
                {
                    var stringChild = (RbfString)child;
                    if (stringChild.Name.Equals("content"))
                    {
                        writer.WriteAttributeString("content", stringChild.Value);
                    }
                    else if (stringChild.Name.Equals("type"))
                    {
                        writer.WriteAttributeString("type", stringChild.Value);
                    }
                    else
                    {
                        throw new Exception("Unexpected string content");
                    }
                }

                if (child is RbfStructure)
                {
                    writer.WriteStartElement(child.Name);
                    WriteStructureContentXml((RbfStructure)child, writer);
                    writer.WriteEndElement();
                }

                if (child is RbfUint32)
                {
                    var intChild = (RbfUint32)child;
                    writer.WriteStartElement(child.Name);
                    writer.WriteAttributeString("value", "0x" + intChild.Value.ToString("X8"));
                    writer.WriteEndElement();
                }

                if (child is RbfBoolean)
                {
                    var booleanChild = (RbfBoolean)child;
                    writer.WriteStartElement(child.Name);
                    writer.WriteAttributeString("value", booleanChild.Value ? "true" : "false");
                    writer.WriteEndElement();
                }

                if (child is RbfFloat3)
                {
                    writer.WriteStartElement(child.Name);
                    var floatVectorChild = (RbfFloat3)child;
                    var s1 = string.Format(CultureInfo.InvariantCulture, "{0:0.0###########}", floatVectorChild.X);
                    var s2 = string.Format(CultureInfo.InvariantCulture, "{0:0.0###########}", floatVectorChild.Y);
                    var s3 = string.Format(CultureInfo.InvariantCulture, "{0:0.0###########}", floatVectorChild.Z);
                    writer.WriteAttributeString("x", s1);
                    writer.WriteAttributeString("y", s2);
                    writer.WriteAttributeString("z", s3);
                    writer.WriteEndElement();
                }
            }
        }
    }
}
