// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Hash;
using System.Collections.Concurrent;
using System.IO;

namespace RageLib.Services
{
    public class JenkinsDictionary : ConcurrentDictionary<int, string>, IJenkinsDictionary
    {
        public static readonly JenkinsDictionary Shared = new JenkinsDictionary();

        public bool TryAdd(string data)
        {
            return TryAdd((int)Jenkins.Hash(data), data);
        }

        public void AddFromFile(string filePath)
        {
            using (Stream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                AddFromFile(fileStream);
        }

        public void AddFromFile(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    string? name = reader.ReadLine();

                    if(name is not null)
                        TryAdd(name);
                }
            }
        }
    }
}
