// -----------------------------------------------------------------------
// <copyright file="SimpleReflectionImporter.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AillieoUtils.EasyBindings.CodeGen
{
    using System.Reflection;
    using Mono.Cecil;

    internal class SimpleReflectionImporter : DefaultReflectionImporter
    {
        public SimpleReflectionImporter(ModuleDefinition module)
            : base(module)
        {
        }

        public override AssemblyNameReference ImportReference(AssemblyName reference)
        {
            return base.ImportReference(reference);
        }
    }
}
