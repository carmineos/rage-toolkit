// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RageLib.Services
{
    /// <summary>
    /// A service with collects and resolves <see cref="Hash.Jenkins"/> hashes
    /// </summary>
    public interface IJenkinsDictionary : IDictionary<int, string>, IJenkinsResolver, IStringCollector
    {
    }

    public interface IJenkinsResolver
    {
        bool TryGetValue(int key, [MaybeNullWhen(false)] out string value);
    }

    public interface IStringCollector
    {
        bool TryAdd(string data);
    }
}
