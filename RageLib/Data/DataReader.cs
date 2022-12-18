// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources;
using RageLib.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace RageLib.Data
{
    /// <summary>
    /// Represents a data reader.
    /// </summary>
    public class DataReader
    {
        private readonly Stream baseStream;
        protected readonly bool endiannessEqualsHostArchitecture;
        private readonly IJenkinsDictionary jenkinsDictionary;

        /// <summary>
        /// Gets or sets the endianness of the underlying stream.
        /// </summary>
        public Endianness Endianness { get; set; }

        /// <summary>
        /// Gets the length of the underlying stream.
        /// </summary>
        public virtual long Length => baseStream.Length;

        /// <summary>
        /// Gets or sets the position within the underlying stream.
        /// </summary>
        public virtual long Position
        {
            get => baseStream.Position;
            set => baseStream.Position = value;
        }

        /// <summary>
        /// Initializes a new data reader for the specified stream.
        /// </summary>
        public DataReader(Stream stream, Endianness endianness = Endianness.LittleEndian, IJenkinsDictionary jenkinsDictionary = null)
        {
            this.baseStream = stream;
            this.Endianness = endianness;
            this.endiannessEqualsHostArchitecture = endianness.EqualsHostArchitecture();
            this.jenkinsDictionary = jenkinsDictionary ?? JenkinsDictionary.Shared;
        }

        /// <summary>
        /// Should be used to read only base types
        /// </summary>
        protected T ReadFromStream<T>() where T : unmanaged
        {
            T data = default;
            var bytesSpan = MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref data, 1));

            ReadFromStreamRaw(bytesSpan);

            // handle endianness
            if (!endiannessEqualsHostArchitecture)
                bytesSpan.Reverse();

            return data;
        }

        /// <summary>
        /// Reads a <typeparamref name="T"/> from the stream but casts it to a <see cref="Span{TReverseAs}"/>  if it's required to reverse it.
        /// Should only be used on numerics types like Vectors, Quaternion and Matrix
        /// </summary>
        protected T ReadFromStreamAs<T, TReverseAs>() where T : unmanaged where TReverseAs : unmanaged
        {
            T data = default;
            var bytesSpan = MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref data, 1));

            ReadFromStreamRaw(bytesSpan);

            // handle endianness
            if (!endiannessEqualsHostArchitecture)
            {
                var sizeOf = Unsafe.SizeOf<TReverseAs>();
                var count = bytesSpan.Length / sizeOf;

                for (int i = 0; i < count; i++)
                    bytesSpan.Slice(i * sizeOf, sizeOf).Reverse();
            }

            return data;
        }

        protected Buffer<T> ReadFromStreamBuffer<T>(int count) where T : unmanaged
        {
            Buffer<T> buffer = new Buffer<T>(count);
            ReadFromStreamRaw(buffer.BytesSpan);

            // handle endianness
            if (!endiannessEqualsHostArchitecture)
                buffer.Reverse();

            return buffer;
        }

        /// <summary>
        /// Reads data from the underlying stream. This is the only method that directly accesses
        /// the data in the underlying stream.
        /// </summary>
        protected virtual void ReadFromStreamRaw(Span<byte> span)
        {
            baseStream.Read(span);
        }

        protected virtual byte ReadByteFromStreamRaw()
        {
            return (byte)baseStream.ReadByte();
        }

        /// <summary>
        /// Reads a byte.
        /// </summary>
        public byte ReadByte()
        {
            return ReadByteFromStreamRaw();
        }

        /// <summary>
        /// Reads a sbyte.
        /// </summary>
        public sbyte ReadSignedByte()
        {
            return (sbyte)ReadByteFromStreamRaw();
        }

        /// <summary>
        /// Reads a sequence of bytes.
        /// </summary>
        public byte[] ReadBytes(int count)
        {
            byte[] array = new byte[count];
            ReadFromStreamRaw(array.AsSpan());
            return array;
        }

        /// <summary>
        /// Reads a signed 16-bit value.
        /// </summary>
        public short ReadInt16()
        {
            return ReadFromStream<short>();
        }

        /// <summary>
        /// Reads a signed 32-bit value.
        /// </summary>
        public int ReadInt32()
        {
            return ReadFromStream<int>();
        }

        /// <summary>
        /// Reads a signed 64-bit value.
        /// </summary>
        public long ReadInt64()
        {
            return ReadFromStream<long>();
        }

        /// <summary>
        /// Reads an unsigned 16-bit value.
        /// </summary>
        public ushort ReadUInt16()
        {
            return ReadFromStream<ushort>();
        }

        /// <summary>
        /// Reads an unsigned 32-bit value.
        /// </summary>
        public uint ReadUInt32()
        {
            return ReadFromStream<uint>();
        }

        /// <summary>
        /// Reads an unsigned 64-bit value.
        /// </summary>
        public ulong ReadUInt64()
        {
            return ReadFromStream<ulong>();
        }

        /// <summary>
        /// Reads a half precision floating point value.
        /// </summary>
        public Half ReadHalf()
        {
            return ReadFromStream<Half>();
        }

        /// <summary>
        /// Reads a single precision floating point value.
        /// </summary>
        public float ReadSingle()
        {
            return ReadFromStream<float>();
        }

        /// <summary>
        /// Reads a double precision floating point value.
        /// </summary>
        public double ReadDouble()
        {
            return ReadFromStream<double>();
        }

        /// <summary>
        /// Reads a string.
        /// </summary>
        public string ReadString()
        {
            // TODO: is 256 a reasonable max length for a string?
            Span<byte> span = stackalloc byte[256];

            int i = 0;
            byte c;
            while ((c = ReadByteFromStreamRaw()) != 0)
            {
                span[i] = c;
                i++;
            }

            string str = Encoding.ASCII.GetString(span.Slice(0, i));
            jenkinsDictionary?.TryAdd(str);

            return str;
        }

        /// <summary>
        /// Reads a string of known length.
        /// </summary>
        public string ReadString(int length)
        {
            using Buffer<byte> buffer = ReadFromStreamBuffer<byte>(length);
            string str = Encoding.ASCII.GetString(buffer.Span);

            jenkinsDictionary?.TryAdd(str);

            return str;
        }

        /// <summary>
        /// Reads a vector with two single precision floating point values.
        /// </summary>
        /// <returns></returns>
        public Vector2 ReadVector2()
        {
            return ReadFromStreamAs<Vector2, float>();
        }

        /// <summary>
        /// Reads a vector with three single precision floating point values.
        /// </summary>
        /// <returns></returns>
        public Vector3 ReadVector3()
        {
            return ReadFromStreamAs<Vector3, float>();
        }

        /// <summary>
        /// Reads a vector with four single precision floating point values.
        /// </summary>
        /// <returns></returns>
        public Vector4 ReadVector4()
        {
            return ReadFromStreamAs<Vector4, float>();
        }

        /// <summary>
        /// Reads a quaternion with four single precision floating point values.
        /// </summary>
        /// <returns></returns>
        public Quaternion ReadQuaternion()
        {
            return ReadFromStreamAs<Quaternion, float>();
        }

        /// <summary>
        /// Reads a matrix with sixteen single precision floating point values.
        /// </summary>
        /// <returns></returns>
        public Matrix4x4 ReadMatrix4x4()
        {
            return ReadFromStreamAs<Matrix4x4, float>();
        }

        public T[] ReadArray<T>(int count) where T : unmanaged
        {
            T[] array = new T[count];
            var bytes = MemoryMarshal.AsBytes(array.AsSpan());
            ReadFromStreamRaw(bytes);

            // handle endianness
            if (!endiannessEqualsHostArchitecture)
            {
                // If it's a struct, let it reverse its endianness
                if (typeof(IResourceStruct<T>).IsAssignableFrom(typeof(T)))
                {
                    for (int i = 0; i < count; i++)
                        array[i] = ((IResourceStruct<T>)array[i]).ReverseEndianness();
                }
                else if (Unsafe.SizeOf<T>() > 1)
                {
                    for (int i = 0; i < count; i++)
                        bytes.Slice(i * Unsafe.SizeOf<T>(), Unsafe.SizeOf<T>()).Reverse();
                }
            }

            return array;
        }

        public T ReadStruct<T>() where T : unmanaged, IResourceStruct<T>
        {
            T value = default;
            var span = MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref value, 1));

            ReadFromStreamRaw(span);
           
            // handle endianness
            if (!endiannessEqualsHostArchitecture)
                value = value.ReverseEndianness();

            return value;
        }
    }
}