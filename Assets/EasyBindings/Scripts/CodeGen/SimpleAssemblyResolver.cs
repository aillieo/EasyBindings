// -----------------------------------------------------------------------
// <copyright file="SimpleAssemblyResolver.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AillieoUtils.EasyBindings.CodeGen
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using Mono.Cecil;
    using Unity.CompilationPipeline.Common.ILPostProcessing;

    internal class SimpleAssemblyResolver : IAssemblyResolver
    {
        private readonly string[] assemblyReferences;

        private readonly Dictionary<string, AssemblyDefinition> assemblyCache = new Dictionary<string, AssemblyDefinition>();

        private readonly ICompiledAssembly compiledAssembly;
        private readonly Dictionary<string, string> foundFiles = new Dictionary<string, string>();

        private AssemblyDefinition selfAssembly;

        public SimpleAssemblyResolver(ICompiledAssembly compiledAssembly)
        {
            this.compiledAssembly = compiledAssembly;
            this.assemblyReferences = compiledAssembly.References;
        }

        public void Dispose()
        {
            this.assemblyCache.Clear();
            this.foundFiles.Clear();
        }

        public AssemblyDefinition Resolve(AssemblyNameReference name)
        {
            return this.Resolve(name, new ReaderParameters(ReadingMode.Deferred));
        }

        public AssemblyDefinition Resolve(AssemblyNameReference name, ReaderParameters parameters)
        {
            lock (this.assemblyCache)
            {
                if (name.Name == this.compiledAssembly.Name)
                {
                    return this.selfAssembly;
                }

                var fileName = this.FindFileWithCache(name);
                if (fileName == null)
                {
                    return null;
                }

                var lastWriteTime = File.GetLastWriteTime(fileName);
                var cacheKey = $"{fileName}{lastWriteTime}";
                if (this.assemblyCache.TryGetValue(cacheKey, out var result))
                {
                    return result;
                }

                parameters.AssemblyResolver = this;

                var ms = MemoryStreamFor(fileName);
                var pdb = $"{fileName}.pdb";
                if (File.Exists(pdb))
                {
                    parameters.SymbolStream = MemoryStreamFor(pdb);
                }

                var assemblyDefinition = AssemblyDefinition.ReadAssembly(ms, parameters);
                this.assemblyCache.Add(cacheKey, assemblyDefinition);

                return assemblyDefinition;
            }
        }

        private static MemoryStream MemoryStreamFor(string fileName)
        {
            return Retry(10, TimeSpan.FromSeconds(1), () =>
            {
                using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    var byteArray = new byte[fileStream.Length];
                    var readLength = fileStream.Read(byteArray, 0, (int)fileStream.Length);
                    if (readLength != fileStream.Length)
                    {
                        throw new InvalidOperationException("File read length is not full length of file.");
                    }

                    return new MemoryStream(byteArray);
                }
            });
        }

        private static MemoryStream Retry(int retryCount, TimeSpan waitTime, Func<MemoryStream> func)
        {
            try
            {
                return func();
            }
            catch (IOException)
            {
                if (retryCount == 0)
                {
                    throw;
                }

                Logger.Error($"Caught IO Exception, trying {retryCount} more times");
                Thread.Sleep(waitTime);

                return Retry(retryCount - 1, waitTime, func);
            }
        }

        private string FindFileWithCache(AssemblyNameReference name)
        {
            lock (this.foundFiles)
            {
                if (!this.foundFiles.TryGetValue(name.Name, out var file))
                {
                    file = this.FindFile(name);
                    this.foundFiles[name.Name] = file;
                }

                return file;
            }
        }

        private string FindFile(AssemblyNameReference name)
        {
            var fileName = this.assemblyReferences.FirstOrDefault(r => Path.GetFileName(r) == $"{name.Name}.dll");
            if (fileName != null)
            {
                return fileName;
            }

            // perhaps the type comes from an exe instead
            fileName = this.assemblyReferences.FirstOrDefault(r => Path.GetFileName(r) == $"{name.Name}.exe");
            if (fileName != null)
            {
                return fileName;
            }

            // Unfortunately the current ICompiledAssembly API only provides direct references.
            // It is very much possible that a postprocessor ends up investigating a type in a directly
            // referenced assembly, that contains a field that is not in a directly referenced assembly.
            // if we don't do anything special for that situation, it will fail to resolve.  We should fix this
            // in the ILPostProcessing API. As a workaround, we rely on the fact here that the indirect references
            // are always located next to direct references, so we search in all directories of direct references we
            // got passed, and if we find the file in there, we resolve to it.
            return this.assemblyReferences
                .Select(Path.GetDirectoryName)
                .Distinct()
                .Select(parentDir => Path.Combine(parentDir, $"{name.Name}.dll"))
                .FirstOrDefault(File.Exists);
        }

        public void AddAssemblyDefinitionBeingOperatedOn(AssemblyDefinition assemblyDefinition)
        {
            this.selfAssembly = assemblyDefinition;
        }
    }
}
