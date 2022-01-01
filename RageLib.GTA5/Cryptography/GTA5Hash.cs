// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace RageLib.GTA5.Cryptography
{
    public class GTA5Hash
    {
        public static byte[] LUT;

        static GTA5Hash()
        {
            // setup constants...
            LUT = GTA5Constants.PC_LUT;
        }
        
        public static uint CalculateHash(string text)
        {
            uint result = 0;
            for (int index = 0; index < text.Length; index++)
            {
                var temp = 1025 * (LUT[text[index]] + result);
                result = (temp >> 6) ^ temp;
            }
            return 32769 * ((9 * result >> 11) ^ 9 * result);
        }
    }
}
