// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Text;

namespace RageLib.Resources.Common
{
    public class string32_r : ResourceSystemBlock
    {
        public override long BlockLength => 0x20;

        /// <summary>
        /// Gets or sets the string value.
        /// </summary>
        public string Value { get; set; }

        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            Value = reader.ReadString(32).Trim('\0');
        }

        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            writer.Write(Value, 32);
        }

        public static explicit operator string(string32_r value)
        {
            return value.Value;
        }

        public static explicit operator string32_r(string value)
        {
            var x = new string32_r();
            x.Value = value;
            return x;
        }
    }
}
