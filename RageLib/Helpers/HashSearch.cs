// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace RageLib.Helpers
{
    public static class HashSearch
    {
        private const int BLOCK_LENGTH = 1048576;

        public static byte[] SearchHash(Stream stream, byte[] hash, int alignment = 1, int length = 32)
        {
            return SearchHashes(stream, new List<byte[]> { hash }, alignment, length)[0];
        }

        public static byte[][] SearchHashes(Stream stream, IList<byte[]> hashes, int alignment = 1, int length = 32)
        {
            var buf = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(buf, 0, buf.Length);

            var result = new byte[hashes.Count][];           

            Parallel.For(0, (int)(stream.Length / BLOCK_LENGTH), (int k) => {

                var tmp = new byte[length];

                var hashProvider = SHA1.Create();
                //var buffer = new byte[length];
                for (int i = 0; i < (BLOCK_LENGTH / alignment); i++)
                {
                    var position = k * BLOCK_LENGTH + i * alignment;
                    if (position >= stream.Length)
                        continue;



                    //lock (stream)
                    //{
                    //    stream.Position = position;
                    //    stream.Read(buffer, 0, length);
                    //}
                    for (int t = 0; t < length; t++)
                    {
                        tmp[t] = buf[position + t];
                    }


                    var hash = hashProvider.ComputeHash(tmp);
                    for (int j = 0; j < hashes.Count; j++)
                        if (hash.SequenceEqual(hashes[j]))
                            result[j] = (byte[])tmp.Clone();
                }
                

            });

            return result;
        }
    }
}