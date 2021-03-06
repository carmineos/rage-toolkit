using System;
using System.IO;
using System.Linq;
using System.Numerics;
using RageLib.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RageLib.GTA5.Tests.Data
{
    [TestClass]
    public class BigEndiannessTests
    {
        private const string fileName = "data.bin";

        [TestMethod]
        public void AssertEndiannes()
        {
            AssertByte(byte.MaxValue);
            AssertInt16(short.MaxValue);
            AssertInt32(int.MaxValue);
            AssertInt64(long.MaxValue);

            AssertUInt16(ushort.MaxValue);
            AssertUInt32(uint.MaxValue);
            AssertUInt64(ulong.MaxValue);

            AssertSingle(float.MaxValue);
            AssertDouble(double.MaxValue);
            AssertVector2(new Vector2(1.0f, 2.0f));
            AssertVector3(new Vector3());
            AssertVector4(new Vector4(1.0f, 2.0f, 3.0f, 4.0f));
            AssertQuaternion(new Quaternion(1.0f, 1.0f, 1.0f, 1.0f));
            AssertMatrix4x4(Matrix4x4.Identity);

            AssertRawBytes(Array.ConvertAll<int,byte>(Enumerable.Range(0, 255).ToArray(), (item) => unchecked((byte)item)));
            AssertArray<int>(Enumerable.Range(0, 100).ToArray());
           
            AssertString("helloworld");
        }

        public void AssertByte(byte expected)
        {
            byte actual;

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                DataWriter writer = new DataWriter(fileStream, Endianess.BigEndian);
                writer.Write(expected);
            }

            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                DataReader reader = new DataReader(fileStream, Endianess.BigEndian);
                actual = reader.ReadByte();
            }

            Assert.AreEqual(expected, actual);
        }

        public void AssertInt16(short expected)
        {
            short actual;

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                DataWriter writer = new DataWriter(fileStream, Endianess.BigEndian);
                writer.Write(expected);
            }

            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                DataReader reader = new DataReader(fileStream, Endianess.BigEndian);
                actual = reader.ReadInt16();
            }

            Assert.AreEqual(expected, actual);
        }

        public void AssertInt32(int expected)
        {
            int actual;

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                DataWriter writer = new DataWriter(fileStream, Endianess.BigEndian);
                writer.Write(expected);
            }

            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                DataReader reader = new DataReader(fileStream, Endianess.BigEndian);
                actual = reader.ReadInt32();
            }

            Assert.AreEqual(expected, actual);
        }

        public void AssertInt64(long expected)
        {
            long actual;

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                DataWriter writer = new DataWriter(fileStream, Endianess.BigEndian);
                writer.Write(expected);
            }

            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                DataReader reader = new DataReader(fileStream, Endianess.BigEndian);
                actual = reader.ReadInt64();
            }

            Assert.AreEqual(expected, actual);
        }

        public void AssertUInt16(ushort expected)
        {
            ushort actual;

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                DataWriter writer = new DataWriter(fileStream, Endianess.BigEndian);
                writer.Write(expected);
            }

            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                DataReader reader = new DataReader(fileStream, Endianess.BigEndian);
                actual = reader.ReadUInt16();
            }

            Assert.AreEqual(expected, actual);
        }

        public void AssertUInt32(uint expected)
        {
            uint actual;

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                DataWriter writer = new DataWriter(fileStream, Endianess.BigEndian);
                writer.Write(expected);
            }

            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                DataReader reader = new DataReader(fileStream, Endianess.BigEndian);
                actual = reader.ReadUInt32();
            }

            Assert.AreEqual(expected, actual);
        }

        public void AssertUInt64(ulong expected)
        {
            ulong actual;

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                DataWriter writer = new DataWriter(fileStream, Endianess.BigEndian);
                writer.Write(expected);
            }

            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                DataReader reader = new DataReader(fileStream, Endianess.BigEndian);
                actual = reader.ReadUInt64();
            }

            Assert.AreEqual(expected, actual);
        }

        public void AssertSingle(float expected)
        {
            float actual;

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                DataWriter writer = new DataWriter(fileStream, Endianess.BigEndian);
                writer.Write(expected);
            }

            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                DataReader reader = new DataReader(fileStream, Endianess.BigEndian);
                actual = reader.ReadSingle();
            }

            Assert.AreEqual(expected, actual);
        }

        public void AssertDouble(double expected)
        {
            double actual;

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                DataWriter writer = new DataWriter(fileStream, Endianess.BigEndian);
                writer.Write(expected);
            }

            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                DataReader reader = new DataReader(fileStream, Endianess.BigEndian);
                actual = reader.ReadDouble();
            }

            Assert.AreEqual(expected, actual);
        }

        public void AssertVector2(Vector2 expected)
        {
            Vector2 actual;

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                DataWriter writer = new DataWriter(fileStream, Endianess.BigEndian);
                writer.Write(expected);
            }

            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                DataReader reader = new DataReader(fileStream, Endianess.BigEndian);
                actual = reader.ReadVector2();
            }

            Assert.AreEqual(expected, actual);
        }

        public void AssertVector3(Vector3 expected)
        {
            Vector3 actual;

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                DataWriter writer = new DataWriter(fileStream, Endianess.BigEndian);
                writer.Write(expected);
            }

            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                DataReader reader = new DataReader(fileStream, Endianess.BigEndian);
                actual = reader.ReadVector3();
            }

            Assert.AreEqual(expected, actual);
        }

        public void AssertVector4(Vector4 expected)
        {
            Vector4 actual;

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                DataWriter writer = new DataWriter(fileStream, Endianess.BigEndian);
                writer.Write(expected);
            }

            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                DataReader reader = new DataReader(fileStream, Endianess.BigEndian);
                actual = reader.ReadVector4();
            }

            Assert.AreEqual(expected, actual);
        }

        public void AssertQuaternion(Quaternion expected)
        {
            Quaternion actual;

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                DataWriter writer = new DataWriter(fileStream, Endianess.BigEndian);
                writer.Write(expected);
            }

            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                DataReader reader = new DataReader(fileStream, Endianess.BigEndian);
                actual = reader.ReadQuaternion();
            }

            Assert.AreEqual(expected, actual);
        }

        public void AssertMatrix4x4(Matrix4x4 expected)
        {
            Matrix4x4 actual;

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                DataWriter writer = new DataWriter(fileStream, Endianess.BigEndian);
                writer.Write(expected);
            }

            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                DataReader reader = new DataReader(fileStream, Endianess.BigEndian);
                actual = reader.ReadMatrix4x4();
            }

            Assert.AreEqual(expected, actual);
        }

        public void AssertString(string expected)
        {
            string actual;

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                DataWriter writer = new DataWriter(fileStream, Endianess.BigEndian);
                writer.Write(expected);
            }

            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                DataReader reader = new DataReader(fileStream, Endianess.BigEndian);
                actual = reader.ReadString();
            }

            Assert.AreEqual(expected, actual);
        }

        public void AssertRawBytes(byte[] expected)
        {
            byte[] actual;

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                DataWriter writer = new DataWriter(fileStream, Endianess.BigEndian);
                writer.WriteArray(expected);
            }

            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                DataReader reader = new DataReader(fileStream, Endianess.BigEndian);
                actual = reader.ReadBytes(expected.Length);
            }

            CollectionAssert.AreEqual(expected, actual);
        }

        public void AssertArray<T>(T[] expected) where T : unmanaged
        {
            T[] actual;

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                DataWriter writer = new DataWriter(fileStream, Endianess.BigEndian);
                writer.WriteArray(expected);
            }

            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                DataReader reader = new DataReader(fileStream, Endianess.BigEndian);
                actual = reader.ReadArray<T>(expected.Length);
            }

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
