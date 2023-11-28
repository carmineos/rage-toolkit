﻿// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;
using RageLib.GTA5.RBF.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RageLib.GTA5.RBF
{
    public class RbfFile
    {
        private const int RBF_IDENT = 0x30464252;

        private RbfStructure current;
        private Stack<RbfStructure> stack;
        private List<RbfEntryDescription> descriptors;

        public RbfStructure Load(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                return Load(fileStream);
            }
        }

        public RbfStructure Load(Stream stream)
        {
            stack = new Stack<RbfStructure>();
            descriptors = new List<RbfEntryDescription>();

            var reader = new DataReader(stream);
            var ident = reader.ReadInt32();
            if (ident != RBF_IDENT)
                throw new Exception("The file identifier does not match.");

            while (reader.Position < reader.Length)
            {
                var descriptorIndex = reader.ReadByte();
                if (descriptorIndex == 0xFF) // close tag
                {
                    var b = reader.ReadByte();
                    if (b != 0xFF)
                        throw new Exception("Expected 0xFF but was " + b.ToString("X2"));

                    if (stack.Count > 0)
                    {
                        current = stack.Pop();
                    }
                    else
                    {
                        if (reader.Position != reader.Length)
                            throw new Exception("Expected end of stream but was not.");
                        return current;
                    }
                }
                else if (descriptorIndex == 0xFD) // bytes
                {
                    var b = reader.ReadByte();
                    if (b != 0xFF)
                        throw new Exception("Expected 0xFF but was " + b.ToString("X2"));

                    var dataLength = reader.ReadInt32();
                    var data = reader.ReadBytes(dataLength);

                    var bytesValue = new RbfBytes();
                    bytesValue.Value = data;
                    current.Children.Add(bytesValue);
                }
                else
                {
                    var dataType = reader.ReadByte();
                    if (descriptorIndex == descriptors.Count) // new descriptor + data
                    {                        
                        var nameLength = reader.ReadInt16();
                        var nameBytes = reader.ReadBytes(nameLength);
                        var name = Encoding.ASCII.GetString(nameBytes);

                        var descriptor = new RbfEntryDescription();
                        descriptor.Name = name;
                        descriptor.Type = dataType;
                        descriptors.Add(descriptor);

                        ParseElement(reader, descriptors.Count - 1);
                    }
                    else // existing descriptor + data
                    {
                        if (dataType != descriptors[descriptorIndex].Type)
                            throw new Exception("Data type does not match.");

                        ParseElement(reader, descriptorIndex);
                    }
                }
            }

            throw new Exception("Unexpected end of stream.");
        }

        private void ParseElement(DataReader reader, int descriptorIndex)
        {
            var descriptor = descriptors[descriptorIndex];
            switch (descriptor.Type)
            {
                case 0: // open element...
                    {
                        var structureValue = new RbfStructure();
                        structureValue.Name = descriptor.Name;

                        if (current != null)
                        {
                            current.Children.Add(structureValue);
                            stack.Push(current);
                        }

                        current = structureValue;

                        // 6 bytes
                        var x1 = reader.ReadInt16();
                        var x2 = reader.ReadInt16();
                        var x3 = reader.ReadInt16();
                        //if (x1 != 0)
                        //    throw new Exception("unexpected");
                        //if (x2 != 0)
                        //    throw new Exception("unexpected");
                        //if (x3 != 0)
                        //    throw new Exception("unexpected");
                        break;
                    }
                case 0x10:
                    {
                        var intValue = new RbfUint32();
                        intValue.Name = descriptor.Name;
                        intValue.Value = reader.ReadUInt32();
                        current.Children.Add(intValue);
                        break;
                    }
                case 0x20:
                    {
                        var booleanValue = new RbfBoolean();
                        booleanValue.Name = descriptor.Name;
                        booleanValue.Value = true;
                        current.Children.Add(booleanValue);
                        break;
                    }
                case 0x30:
                    {
                        var booleanValue = new RbfBoolean();
                        booleanValue.Name = descriptor.Name;
                        booleanValue.Value = false;
                        current.Children.Add(booleanValue);
                        break;
                    }
                case 0x40:
                    {
                        var floatValue = new RbfFloat();
                        floatValue.Name = descriptor.Name;
                        floatValue.Value = reader.ReadSingle();
                        current.Children.Add(floatValue);
                        break;
                    }
                case 0x50:
                    {
                        var floatVectorValue = new RbfFloat3();
                        floatVectorValue.Name = descriptor.Name;
                        floatVectorValue.X = reader.ReadSingle();
                        floatVectorValue.Y = reader.ReadSingle();
                        floatVectorValue.Z = reader.ReadSingle();
                        current.Children.Add(floatVectorValue);
                        break;
                    }
                case 0x60:
                    {
                        var valueLength = reader.ReadInt16();
                        var valueBytes = reader.ReadBytes(valueLength);
                        var value = Encoding.ASCII.GetString(valueBytes);
                        var stringValue = new RbfString();
                        stringValue.Name = descriptor.Name;
                        stringValue.Value = value;
                        current.Children.Add(stringValue);
                        break;
                    }
                default:
                    throw new Exception("Unsupported data type.");
            }
        }

        public static bool IsRBF(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                return IsRBF(stream);
        }

        public static bool IsRBF(Stream stream)
        {
            var reader = new DataReader(stream);
            var ident = reader.ReadUInt32();
            stream.Position -= 4;
            return ident == RBF_IDENT;
        }
    }
}
