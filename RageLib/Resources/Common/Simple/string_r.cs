// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.Resources.Common.Simple
{
    /// <summary>
    /// Represents a string that can be referenced in a resource structure.
    /// </summary>
    public class string_r : ResourceSystemBlock
    {
        /// <summary>
        /// Gets the length of the string.
        /// </summary>
        public override long BlockLength
        {
            get { return Value.Length + 1; }
        }

        /// <summary>
        /// Gets or sets the string value.
        /// </summary>
        public string Value { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            Value = reader.ReadString();
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            writer.Write(Value);
        }

        public static explicit operator string(string_r value)
        {
            return value.Value;
        }

        public static explicit operator string_r(string value)
        {
            var x = new string_r();
            x.Value = value;
            return x;
        }
    }
}
