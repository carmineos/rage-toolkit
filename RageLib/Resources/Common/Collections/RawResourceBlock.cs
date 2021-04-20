using System;

namespace RageLib.Resources.Common
{
    /// <summary>
    /// A <see cref="ResourceSystemBlock"/> which holds an array of unmanaged type.
    /// </summary>
    public class RawResourceBlock : IResourceBlock
    {
        /// <summary>
        /// Gets the length of the data block.
        /// </summary>
        public long BlockLength => Data.Length;

        /// <summary>
        /// Gets or sets the position of the data block.
        /// </summary>
        public long BlockPosition { get; set; }

        /// <summary>
        /// The data of the block
        /// </summary>
        public byte[] Data { get; set; }

        public int Count => Data.Length;

        public RawResourceBlock()
        {
            Data = Array.Empty<byte>();
        }

        public RawResourceBlock(byte[] array)
        {
            Data = array;
        }

        /// <summary>
        /// Reads the data block.
        /// </summary>
        public void Read(ResourceDataReader reader, params object[] parameters)
        {
            int count = Convert.ToInt32(parameters[0]);

            Data = reader.ReadBytes(count);
        }

        /// <summary>
        /// Writes the data block.
        /// </summary>
        public void Write(ResourceDataWriter writer, params object[] parameters)
        {
            writer.Write(Data);
        }
    }

    public class RawResourceSystemBlock : RawResourceBlock, IResourceSystemBlock
    {
        public RawResourceSystemBlock(byte[] array) : base(array)
        {

        }

        public void Rebuild()
        {

        }

        public Tuple<long, IResourceBlock>[] GetParts()
        {
            return Array.Empty<Tuple<long, IResourceBlock>>();
        }

        public IResourceBlock[] GetReferences()
        {
            return Array.Empty<IResourceBlock>();
        }
    }

    public class RawResourceGraphicsBlock : RawResourceBlock, IResourceGraphicsBlock
    {
        public RawResourceGraphicsBlock(byte[] array) : base(array)
        {

        }
    }
}