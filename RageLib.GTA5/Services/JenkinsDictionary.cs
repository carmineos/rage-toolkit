using RageLib.Hash;
using RageLib.Services;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RageLib.GTA5.Services
{
    public class JenkinsDictionary : IHashDictionary<uint, string>
    {
        private readonly ConcurrentDictionary<uint, string> _index = new ConcurrentDictionary<uint, string>();

        public ICollection<uint> Keys => _index.Keys;
        public ICollection<string> Values => _index.Values;
        public int Count => _index.Count;
        public bool IsEmpty => _index.IsEmpty;

        public void Clear() => _index.Clear();

        public bool TryAdd(string data)
        {
            return _index.TryAdd(Jenkins.Hash(data), data);
        }

        public bool TryGetValue(uint hash, out string data)
        {
            return _index.TryGetValue(hash, out data);
        }
    }
}
