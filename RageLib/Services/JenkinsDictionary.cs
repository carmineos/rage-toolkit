using RageLib.Hash;
using System.Collections.Generic;
using System.IO;

namespace RageLib.Services
{
    public class JenkinsDictionary : Dictionary<uint, string>, IHashDictionary<uint, string>
    {
        public bool TryAdd(string data)
        {
            return this.TryAdd(Jenkins.Hash(data), data);
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
                    string name = reader.ReadLine();
                    TryAdd(name);
                }
            }
        }
    }
}
