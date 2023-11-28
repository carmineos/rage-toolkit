// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using RageLib.GTA5.ResourceWrappers.PC.Meta;
using RageLib.GTA5.ResourceWrappers.PC.Meta.Definitions;
using RageLib.GTA5.ResourceWrappers.PC.Meta.Types;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Xml.Serialization;

namespace RageLib.GTA5.Tests.ResourceWrappers.PC.Meta
{
    [TestClass]
    public class MetaXmlImporterIntegrationTests
    {
        private const string TEST_DATASET = "RageLib.GTA5.Tests.ResourceWrappers.PC.Meta.TestDataset.xml";
        private const string TEST_DATASET_DEFINITIONS = "RageLib.GTA5.Tests.ResourceWrappers.PC.Meta.TestDatasetDefinitions.xml";

        [TestMethod]
        public void Import_Always_CorrectlyImportsXml()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var def = LoadXmls();

            var importer = new MetaXmlImporter(def);

            var assembly = Assembly.GetExecutingAssembly();

            var expectedDocumentStream = assembly.GetManifestResourceStream(TEST_DATASET);
            var x = importer.Import(expectedDocumentStream);


            var refData = TestDataset.MakeDataset();

            AssertValue(refData, x);
        }

        public MetaDefinitions LoadXmls()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream xmlStream = assembly.GetManifestResourceStream(TEST_DATASET_DEFINITIONS))
            {
                var ser = new XmlSerializer(typeof(MetaDefinitions));
                var definitions = (MetaDefinitions)ser.Deserialize(xmlStream);
                return definitions;
            }
        }

        public void AssertValue(IMetaValue expectedValue, IMetaValue actualValue)
        {
            Assert.AreEqual(expectedValue.GetType(), actualValue.GetType());

            if (expectedValue is MetaArray)
            {
                AssertArray((MetaArray)expectedValue, (MetaArray)actualValue);
            }
            else if (expectedValue is MetaArrayLocal)
            {
                AssertArrayOfBytes((MetaArrayLocal)expectedValue, (MetaArrayLocal)actualValue);
            }
            else if (expectedValue is MetaString)
            {
                AssertArrayOfChars((MetaString)expectedValue, (MetaString)actualValue);
            }
            else if (expectedValue is MetaBool)
            {
                AssertBoolean((MetaBool)expectedValue, (MetaBool)actualValue);
            }
            else if (expectedValue is MetaSByte)
            {
                AssertByte((MetaSByte)expectedValue, (MetaSByte)actualValue);
            }
            else if (expectedValue is MetaByte)
            {
                AssertByte((MetaByte)expectedValue, (MetaByte)actualValue);
            }
            else if (expectedValue is MetaEnumInt8)
            {
                AssertByte((MetaEnumInt8)expectedValue, (MetaEnumInt8)actualValue);
            }
            else if (expectedValue is MetaStringPointer)
            {
                AssertCharPointer((MetaStringPointer)expectedValue, (MetaStringPointer)actualValue);
            }
            else if (expectedValue is MetaDataBlockPointer)
            {
                AssertDataBlockPointer((MetaDataBlockPointer)expectedValue, (MetaDataBlockPointer)actualValue);
            }
            else if (expectedValue is MetaFloat)
            {
                AssertFloat((MetaFloat)expectedValue, (MetaFloat)actualValue);
            }
            else if (expectedValue is MetaVector3)
            {
                AssertFloatVector((MetaVector3)expectedValue, (MetaVector3)actualValue);
            }
            else if (expectedValue is MetaVector4)
            {
                AssertFloatVector((MetaVector4)expectedValue, (MetaVector4)actualValue);
            }
            else if (expectedValue is MetaInt16)
            {
                AssertInt16((MetaInt16)expectedValue, (MetaInt16)actualValue);
            }
            else if (expectedValue is MetaUInt16)
            {
                AssertInt16((MetaUInt16)expectedValue, (MetaUInt16)actualValue);
            }
            else if (expectedValue is MetaFlagsInt16)
            {
                AssertInt16((MetaFlagsInt16)expectedValue, (MetaFlagsInt16)actualValue);
            }
            else if (expectedValue is MetaInt32)
            {
                AssertInt32((MetaInt32)expectedValue, (MetaInt32)actualValue);
            }
            else if (expectedValue is MetaUInt32)
            {
                AssertInt32((MetaUInt32)expectedValue, (MetaUInt32)actualValue);
            }
            else if (expectedValue is MetaEnumInt32)
            {
                AssertInt32((MetaEnumInt32)expectedValue, (MetaEnumInt32)actualValue);
            }
            else if (expectedValue is MetaFlagsInt8)
            {
                AssertInt32((MetaFlagsInt8)expectedValue, (MetaFlagsInt8)actualValue);
            }
            else if (expectedValue is MetaFlagsInt32)
            {
                AssertInt32((MetaFlagsInt32)expectedValue, (MetaFlagsInt32)actualValue);
            }
            else if (expectedValue is MetaStringHash)
            {
                AssertInt32((MetaStringHash)expectedValue, (MetaStringHash)actualValue);
            }
            else if (expectedValue is MetaGeneric)
            {
                AssertPointer((MetaGeneric)expectedValue, (MetaGeneric)actualValue);
            }
            else if (expectedValue is MetaStructure)
            {
                AssertStructure((MetaStructure)expectedValue, (MetaStructure)actualValue);
            }
            else
            {
                Assert.Fail("Illegal values type.");
            }
        }

        public void AssertArray(MetaArray expectedArray, MetaArray actualArray)
        {
            Assert.AreEqual(expectedArray.Entries?.Count, actualArray.Entries?.Count);
            if (expectedArray.Entries != null)
            {
                for (int i = 0; i < expectedArray.Entries.Count; i++)
                {
                    AssertValue(expectedArray.Entries[i], actualArray.Entries[i]);
                }
            }
        }

        public void AssertArrayOfBytes(MetaArrayLocal expectedArray, MetaArrayLocal actualArray)
        {
            switch (expectedArray)
            {
                case MetaArrayLocal<byte>:
                    CollectionAssert.AreEqual(((MetaArrayLocal<byte>)expectedArray).Value, ((MetaArrayLocal<byte>)actualArray).Value);
                    break;
                case MetaArrayLocal<ushort>:
                    CollectionAssert.AreEqual(((MetaArrayLocal<ushort>)expectedArray).Value, ((MetaArrayLocal<ushort>)actualArray).Value);
                    break;
                case MetaArrayLocal<uint>:
                    CollectionAssert.AreEqual(((MetaArrayLocal<uint>)expectedArray).Value, ((MetaArrayLocal<uint>)actualArray).Value);
                    break;
                case MetaArrayLocal<float>:
                    CollectionAssert.AreEqual(((MetaArrayLocal<float>)expectedArray).Value, ((MetaArrayLocal<float>)actualArray).Value);
                    break;
            }
        }

        public void AssertArrayOfChars(MetaString expectedArray, MetaString actualArray)
        {
            Assert.AreEqual(expectedArray.Value, actualArray.Value);
        }

        public void AssertBoolean(MetaBool expectedBoolean, MetaBool actualBoolean)
        {
            Assert.AreEqual(expectedBoolean.Value, actualBoolean.Value);
        }

        public void AssertByte(MetaSByte expectedByte, MetaSByte actualByte)
        {
            Assert.AreEqual(expectedByte.Value, actualByte.Value);
        }

        public void AssertByte(MetaByte expectedByte, MetaByte actualByte)
        {
            Assert.AreEqual(expectedByte.Value, actualByte.Value);
        }

        public void AssertByte(MetaEnumInt8 expectedByte, MetaEnumInt8 actualByte)
        {
            Assert.AreEqual(expectedByte.Value, actualByte.Value);
        }

        public void AssertCharPointer(MetaStringPointer expectedCharPointer, MetaStringPointer actualCharPointer)
        {
            Assert.AreEqual(expectedCharPointer.Value, actualCharPointer.Value);
        }

        public void AssertDataBlockPointer(MetaDataBlockPointer expectedDataBlockPointer, MetaDataBlockPointer actualDataBlockPointer)
        {
            CollectionAssert.AreEqual(expectedDataBlockPointer.Data, actualDataBlockPointer.Data);
        }

        public void AssertFloat(MetaFloat expectedFloat, MetaFloat actualFloat)
        {
            Assert.AreEqual(expectedFloat.Value, actualFloat.Value);
        }

        public void AssertFloatVector(MetaVector3 expectedFloat, MetaVector3 actualFloat)
        {
            Assert.AreEqual(expectedFloat.Value.X, actualFloat.Value.X);
            Assert.AreEqual(expectedFloat.Value.Y, actualFloat.Value.Y);
            Assert.AreEqual(expectedFloat.Value.Z, actualFloat.Value.Z);
        }

        public void AssertFloatVector(MetaVector4 expectedFloat, MetaVector4 actualFloat)
        {
            Assert.AreEqual(expectedFloat.Value.X, actualFloat.Value.X);
            Assert.AreEqual(expectedFloat.Value.Y, actualFloat.Value.Y);
            Assert.AreEqual(expectedFloat.Value.Z, actualFloat.Value.Z);
            Assert.AreEqual(expectedFloat.Value.W, actualFloat.Value.W);
        }

        public void AssertInt16(MetaInt16 expectedInt, MetaInt16 actualInt)
        {
            Assert.AreEqual(expectedInt.Value, actualInt.Value);
        }

        public void AssertInt16(MetaUInt16 expectedInt, MetaUInt16 actualInt)
        {
            Assert.AreEqual(expectedInt.Value, actualInt.Value);
        }

        public void AssertInt16(MetaFlagsInt16 expectedInt, MetaFlagsInt16 actualInt)
        {
            Assert.AreEqual(expectedInt.Value, actualInt.Value);
        }

        public void AssertInt32(MetaInt32 expectedInt, MetaInt32 actualInt)
        {
            Assert.AreEqual(expectedInt.Value, actualInt.Value);
        }

        public void AssertInt32(MetaUInt32 expectedInt, MetaUInt32 actualInt)
        {
            Assert.AreEqual(expectedInt.Value, actualInt.Value);
        }

        public void AssertInt32(MetaEnumInt32 expectedInt, MetaEnumInt32 actualInt)
        {
            Assert.AreEqual(expectedInt.Value, actualInt.Value);
        }

        public void AssertInt32(MetaFlagsInt8 expectedInt, MetaFlagsInt8 actualInt)
        {
            Assert.AreEqual(expectedInt.Value, actualInt.Value);
        }

        public void AssertInt32(MetaFlagsInt32 expectedInt, MetaFlagsInt32 actualInt)
        {
            Assert.AreEqual(expectedInt.Value, actualInt.Value);
        }

        public void AssertInt32(MetaStringHash expectedInt, MetaStringHash actualInt)
        {
            Assert.AreEqual(expectedInt.Value, actualInt.Value);
        }

        public void AssertPointer(MetaGeneric expectedPointer, MetaGeneric actualPointer)
        {
            if (expectedPointer.Value == null)
            {
                Assert.IsNull(actualPointer.Value);
            }            
            else
            {
                Assert.IsNotNull(actualPointer.Value);
                AssertValue(expectedPointer.Value, actualPointer.Value);
            }
        }

        public void AssertStructure(MetaStructure expectedStructure, MetaStructure actualStructure)
        {
            var expectedValues = expectedStructure.Values;
            var actualValues = actualStructure.Values;

            Assert.AreEqual(expectedValues.Count, actualValues.Count);
            foreach (var key in expectedValues.Keys)
            {
                AssertValue(expectedValues[key], actualValues[key]);
            }
        }


    }
}