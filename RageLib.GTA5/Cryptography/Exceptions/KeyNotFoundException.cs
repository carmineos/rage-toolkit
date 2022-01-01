// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System;

namespace RageLib.GTA5.Cryptography.Exceptions
{
    public class KeyNotFoundException : Exception
    {
        public KeyNotFoundException() : base()
        { }

        public KeyNotFoundException(string message) : base(message)
        { }
    }
}
