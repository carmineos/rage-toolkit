// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using RageLib.GTA5.ResourceWrappers.PC.Meta;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Xml;

namespace RageLib.GTA5.Tests.ResourceWrappers.PC.Meta
{
    [TestClass]
    public class MetaXmlExporterIntegrationTests
    {
        private const string TEST_DATASET = "RageLib.GTA5.Tests.ResourceWrappers.PC.Meta.TestDataset.xml";

        [TestMethod]
        public void Export_Always_CorrectlyExportsXml()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var exporter = new MetaXmlExporter();
            var xmlStream = new MemoryStream();

            var rootStructure = TestDataset.MakeDataset();

            exporter.Export(rootStructure, xmlStream);

            var assembly = Assembly.GetExecutingAssembly();
            var expectedDocument = new XmlDocument();
            using (var expectedDocumentStream = assembly.GetManifestResourceStream(TEST_DATASET))
            {
                expectedDocument.Load(expectedDocumentStream);
            }
            var actualDocument = new XmlDocument();
            xmlStream.Position = 0;
            actualDocument.Load(xmlStream);
            AssertXml(expectedDocument, actualDocument);
        }

        public void AssertXml(XmlDocument expectedDocument, XmlDocument actualDocument)
        {
            var expectedNodes = expectedDocument.ChildNodes;
            var actualNodes = actualDocument.ChildNodes;

            Assert.AreEqual(expectedNodes.Count, actualNodes.Count);
            for (int i = 0; i < expectedNodes.Count; i++)
            {
                AssertXmlNode(expectedNodes[i], actualNodes[i]);
            }
        }

        public void AssertXmlNode(XmlNode expectedNode, XmlNode actualNode)
        {
            if (expectedNode.NodeType == XmlNodeType.Text)
            {
                string[] s1 = expectedNode.Value.Trim().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                string[] s2 = actualNode.Value.Trim().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                Assert.AreEqual(s1.Length, s2.Length);
                for (int i = 0; i < s1.Length; i++)
                {
                    Assert.AreEqual(s1[i].Trim(), s2[i].Trim());
                }
            }
            else
            {
                AssertXmlAttributes(expectedNode.Attributes, actualNode.Attributes);

                var expectedNodes = expectedNode.ChildNodes;
                var actualNodes = actualNode.ChildNodes;
                Assert.AreEqual(expectedNodes.Count, actualNodes.Count);
                for (int i = 0; i < expectedNodes.Count; i++)
                {
                    AssertXmlNode(expectedNodes[i], actualNodes[i]);
                }
            }

        }

        public void AssertXmlAttributes(XmlAttributeCollection expectedAttributes, XmlAttributeCollection actualAttributes)
        {
            Assert.AreEqual(expectedAttributes?.Count, actualAttributes?.Count);
            if (expectedAttributes != null)
            {
                for (int i = 0; i < expectedAttributes.Count; i++)
                {
                    Assert.AreEqual(expectedAttributes[i].Name, actualAttributes[i].Name);
                    Assert.AreEqual(expectedAttributes[i].Value, actualAttributes[i].Value);
                }
            }
        }
    }
}
