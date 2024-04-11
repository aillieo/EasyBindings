// -----------------------------------------------------------------------
// <copyright file="SimpleReflectionImporterProvider.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AillieoUtils.EasyBindings.CodeGen
{
    using Mono.Cecil;

    internal class SimpleReflectionImporterProvider : IReflectionImporterProvider
    {
        public IReflectionImporter GetReflectionImporter(ModuleDefinition moduleDefinition)
        {
            return new SimpleReflectionImporter(moduleDefinition);
        }
    }
}
