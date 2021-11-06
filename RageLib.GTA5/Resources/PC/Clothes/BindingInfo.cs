/*
    Copyright(c) 2017 Neodymium

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

using RageLib.Data;
using System.Numerics;

namespace RageLib.Resources.GTA5.PC.Clothes
{
    // rage__characterClothController__BindingInfo
    public struct BindingInfo : IResourceStruct<BindingInfo>
    {
        // structure data
        public Vector4 Weights;
        public uint BlendIndex0;
        public uint BlendIndex1;
        public uint BlendIndex2;
        public uint BlendIndex3;

        public BindingInfo ReverseEndianness()
        {
            return new BindingInfo()
            {
                Weights = EndiannessExtensions.ReverseEndianness(Weights),
                BlendIndex0 = EndiannessExtensions.ReverseEndianness(BlendIndex0),
                BlendIndex1 = EndiannessExtensions.ReverseEndianness(BlendIndex1),
                BlendIndex2 = EndiannessExtensions.ReverseEndianness(BlendIndex2),
                BlendIndex3 = EndiannessExtensions.ReverseEndianness(BlendIndex3),
            };
        }
    }
}
