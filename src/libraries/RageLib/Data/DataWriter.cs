﻿// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources;
using System;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace RageLib.Data
{
    /// <summary>
    /// Represents a data writer.
    /// </summary>
    public class DataWriter
    {
        private readonly Stream baseStream;
        protected readonly bool endiannessEqualsHostArchitecture;

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
        /// Initializes a new data writer for the specified stream.
        /// </summary>
        public DataWriter(Stream stream, Endianness endianness = Endianness.LittleEndian)
        {
            this.baseStream = stream;
            this.Endianness = endianness;
            this.endiannessEqualsHostArchitecture = endianness.EqualsHostArchitecture();
        }

        /// <summary>
        /// Writes data to the underlying stream. This is the only method that directly accesses
        /// the data in the underlying stream.
        /// </summary>
        protected virtual void WriteToStreamRaw(ReadOnlySpan<byte> value)
        {
            baseStream.Write(value);
        }

        protected virtual void WriteToStreamRaw(byte value)
        {
            baseStream.WriteByte(value);
        }

        /// <summary>
        /// Fills amount of bytes with zeroes.
        /// </summary>
        /// <param name="byteCount"></param>
        public void WritePadding(int byteCount, byte b = 0x00)
        {
            using Buffer<byte> buffer = new Buffer<byte>(byteCount);
            var span = buffer.BytesSpan;
            span.Fill(b);
            WriteToStreamRaw(span);
        }

        /// <summary>
        /// Writes a byte.
        /// </summary>
        public void Write(byte value)
        {
            WriteToStreamRaw(value);
        }

        /// <summary>
        /// Writes boolean value (single byte)
        /// </summary>
        /// <param name="value"></param>
        public void Write(bool value)
        {
            WriteToStreamRaw(value ? (byte)1 : (byte)0);
        }

        /// <summary>
        /// Writes a byte.
        /// </summary>
        public void Write(sbyte value)
        {
            WriteToStreamRaw((byte)value);
        }

        /// <summary>
        /// Writes a sequence of bytes.
        /// </summary>
        public void Write(byte[] value)
        {
            WriteToStreamRaw(value);
        }

        /// <summary>
        /// Writes a sequence of bytes.
        /// </summary>
        public void Write(ReadOnlySpan<byte> value)
        {
            WriteToStreamRaw(value);
        }

        /// <summary>
        /// Writes a signed 16-bit value.
        /// </summary>
        public void Write(short value)
        {
            WriteToStream(value);
        }

        /// <summary>
        /// Writes a signed 32-bit value.
        /// </summary>
        public void Write(int value)
        {
            WriteToStream(value);
        }

        /// <summary>
        /// Writes a signed 64-bit value.
        /// </summary>
        public void Write(long value)
        {
            WriteToStream(value);
        }

        /// <summary>
        /// Writes an unsigned 16-bit value.
        /// </summary>
        public void Write(ushort value)
        {
            WriteToStream(value);
        }

        /// <summary>
        /// Writes an unsigned 32-bit value.
        /// </summary>
        public void Write(uint value)
        {
            WriteToStream(value);
        }

        /// <summary>
        /// Writes an unsigned 64-bit value.
        /// </summary>
        public void Write(ulong value)
        {
            WriteToStream(value);
        }

        /// <summary>
        /// Writes a half precision floating point value.
        /// </summary>
        public void Write(Half value)
        {
            WriteToStream(value);
        }

        /// <summary>
        /// Writes a single precision floating point value.
        /// </summary>
        public void Write(float value)
        {
            WriteToStream(value);
        }

        /// <summary>
        /// Writes a double precision floating point value.
        /// </summary>
        public void Write(double value)
        {
            WriteToStream(value);
        }

        /// <summary>
        /// Writes a string.
        /// </summary>
        public void Write(string value)
        {
            //using Buffer<byte> buffer = new Buffer<byte>(value.Length);
            //_ = Encoding.ASCII.GetBytes(value.AsSpan(), buffer.BytesSpan);
            //WriteToStreamRaw(buffer.BytesSpan);
            
            foreach (var c in value)
                WriteToStreamRaw((byte)c);
            WriteToStreamRaw((byte)0);
        }

        /// <summary>
        /// Writes a string with fixed length (the string gets padded if shorter or trimmed if longer)
        /// </summary>
        public void Write(string value, int fixedLength)
        {
            int min = (value.Length <= fixedLength) ? value.Length : fixedLength;
            
            int i;
            for (i = 0; i < min; i++)
                WriteToStreamRaw((byte)value[i]);

            for (; i < fixedLength; i++)
                WriteToStreamRaw((byte)0);
        }

        /// <summary>
        /// Writes a vector with two single precision floating point values
        /// </summary>
        /// <param name="value"></param>
        public void Write(Vector2 value)
        {
            Write(value.X);
            Write(value.Y);
        }

        /// <summary>
        /// Writes a vector with three single precision floating point values
        /// </summary>
        /// <param name="value"></param>
        public void Write(Vector3 value)
        {
            Write(value.X);
            Write(value.Y);
            Write(value.Z);
        }

        /// <summary>
        /// Writes a vector with four single precision floating point values
        /// </summary>
        /// <param name="value"></param>
        public void Write(Vector4 value)
        {
            Write(value.X);
            Write(value.Y);
            Write(value.Z);
            Write(value.W);
        }

        /// <summary>
        /// Writes a quaternion with four single precision floating point values
        /// </summary>
        /// <param name="value"></param>
        public void Write(Quaternion value)
        {
            Write(value.X);
            Write(value.Y);
            Write(value.Z);
            Write(value.W);
        }

        /// <summary>
        /// Writes a matrix with sixteen single precision floating point values
        /// </summary>
        /// <param name="value"></param>
        public void Write(Matrix4x4 value)
        {
            Write(value.M11);
            Write(value.M12);
            Write(value.M13);
            Write(value.M14);
            Write(value.M21);
            Write(value.M22);
            Write(value.M23);
            Write(value.M24);
            Write(value.M31);
            Write(value.M32);
            Write(value.M33);
            Write(value.M34);
            Write(value.M41);
            Write(value.M42);
            Write(value.M43);
            Write(value.M44);
        }

        public void WriteArray<T>(T[] items) where T : unmanaged
        {
            var span = MemoryMarshal.AsBytes(items.AsSpan());

            if (!endiannessEqualsHostArchitecture)
            {
                // Don't invert endianness on input array!
                using Buffer<T> buffer = new Buffer<T>(items.Length);
                {
                    span.CopyTo(buffer.BytesSpan);

                    // If it's a struct, let it reverse its endianness
                    if (typeof(IResourceStruct<T>).IsAssignableFrom(typeof(T)))
                    {
                        for (int i = 0; i < buffer.Count; i++)
                            buffer.Span[i] = ((IResourceStruct<T>)buffer.Span[i]).ReverseEndianness();
                    }
                    else // If it's a primitive type
                        buffer.Reverse();

                    WriteToStreamRaw(buffer.BytesSpan);
                    return;
                }
            }

            WriteToStreamRaw(span);
        }

        protected void WriteToStream<T>(T value) where T : unmanaged
        {
            var span = MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref value, 1));

            if (!endiannessEqualsHostArchitecture)
                span.Reverse();

            WriteToStreamRaw(span);
        }

        public void WriteStruct<T>(T value) where T : unmanaged, IResourceStruct<T>
        {
            if (!endiannessEqualsHostArchitecture)
                value = value.ReverseEndianness();

            var span = MemoryMarshal.AsBytes(MemoryMarshal.CreateSpan(ref value, 1));
            WriteToStreamRaw(span);
        }
    }
}