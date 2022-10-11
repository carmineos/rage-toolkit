// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Collections.Generic;

namespace RageLib.Services
{
    /// <summary>
    /// A service with collects and resolves <see cref="Hash.Jenkins"/> hashes
    /// </summary>
    public interface IJenkinsDictionary : IDictionary<int, string>
    {
        bool TryAdd(string data);
    }
}
