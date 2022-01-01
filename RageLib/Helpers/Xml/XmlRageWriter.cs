// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Xml;

namespace RageLib.Helpers.Xml
{
    /// <summary>
    /// A wrapper around a <see cref="System.Xml.XmlWriter"/> to workaround the lack of exposed indentation level
    /// allowing to write innert text of a node on multiplelines without breaking indentation
    /// </summary>
    public class XmlRageWriter : XmlWriter
    {
        private int indentLevel = 0;
        private readonly XmlWriter writer;

        public XmlRageWriter(XmlWriter writer)
        {
            this.writer = writer;
        }

        public void WriteInnerIndent()
        {
            var chars = writer.Settings.IndentChars;
            for (int i = 0; i < indentLevel; i++)
                writer.WriteString(chars);
        }

        public void WriteOuterIndent()
        {
            var chars = writer.Settings.IndentChars;
            for (int i = 0; i < indentLevel - 1; i++)
                writer.WriteString(chars);
        }

        public override void WriteStartElement(string prefix, string localName, string ns)
        {
            writer.WriteStartElement(prefix, localName, ns);
            indentLevel++;
        }

        public override void WriteEndElement()
        {
            writer.WriteEndElement();
            indentLevel--;
        }

        public override WriteState WriteState => writer.WriteState;

        public override void Flush() => writer.Flush();

        public override string LookupPrefix(string ns) => writer.LookupPrefix(ns);

        public override void WriteBase64(byte[] buffer, int index, int count) => writer.WriteBase64(buffer, index, count);

        public override void WriteCData(string text) => writer.WriteCData(text);

        public override void WriteCharEntity(char ch) => writer.WriteCharEntity(ch);

        public override void WriteChars(char[] buffer, int index, int count) => writer.WriteChars(buffer, index, count);

        public override void WriteComment(string text) => writer.WriteComment(text);

        public override void WriteDocType(string name, string pubid, string sysid, string subset) => writer.WriteDocType(name, pubid, sysid, subset);

        public override void WriteEndAttribute() => writer.WriteEndAttribute();

        public override void WriteEndDocument() => writer.WriteEndDocument();
        
        public override void WriteEntityRef(string name) => writer.WriteEntityRef(name);

        public override void WriteFullEndElement() => writer.WriteFullEndElement();

        public override void WriteProcessingInstruction(string name, string text) => writer.WriteProcessingInstruction(name, text);

        public override void WriteRaw(char[] buffer, int index, int count) => writer.WriteRaw(buffer, index, count);

        public override void WriteRaw(string data) => writer.WriteRaw(data);

        public override void WriteStartAttribute(string prefix, string localName, string ns) => writer.WriteStartAttribute(prefix, localName, ns);

        public override void WriteStartDocument() => writer.WriteStartDocument();

        public override void WriteStartDocument(bool standalone) => writer.WriteStartDocument(standalone);

        public override void WriteString(string text) => writer.WriteString(text);

        public override void WriteSurrogateCharEntity(char lowChar, char highChar) => writer.WriteSurrogateCharEntity(lowChar, highChar);

        public override void WriteWhitespace(string ws) => writer.WriteWhitespace(ws);
    }
}
