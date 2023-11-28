﻿// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Resources.Common;
using RageLib.Resources.GTA5;
using RageLib.Resources.GTA5.PC.Expressions;
using System.IO;

namespace RageLib.GTA5.ResourceWrappers.PC.Expressions
{
    public class ExpressionDictionaryFileWrapper_GTA5_pc
    {
        private PgDictionary64<Expression> expressionDictionary;

        public void Load(Stream stream)
        {
            var resource = new Resource7<PgDictionary64<Expression>>();
            resource.Load(stream);

            expressionDictionary = resource.ResourceData;
        }

        public void Load(string fileName)
        {
            var resource = new Resource7<PgDictionary64<Expression>>();
            resource.Load(fileName);

            expressionDictionary = resource.ResourceData;
        }

        public void Save(Stream stream)
        {
            var resource = new Resource7<PgDictionary64<Expression>>();
            resource.ResourceData = expressionDictionary;
            resource.Version = 25;
            resource.Save(stream);
        }

        public void Save(string fileName)
        {
            var resource = new Resource7<PgDictionary64<Expression>>();
            resource.ResourceData = expressionDictionary;
            resource.Version = 25;
            resource.Save(fileName);
        }
    }
}
