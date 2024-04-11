// -----------------------------------------------------------------------
// <copyright file="NotifyOnValueChangedProcessor.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AillieoUtils.EasyBindings.CodeGen
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Mono.Cecil;
    using Mono.Cecil.Cil;
    using Mono.Collections.Generic;
    using Unity.CompilationPipeline.Common.ILPostProcessing;
    using MethodBody = Mono.Cecil.Cil.MethodBody;

    internal class NotifyOnValueChangedProcessor : ILPostProcessor
    {
        public override ILPostProcessor GetInstance() => this;

        public override bool WillProcess(ICompiledAssembly compiledAssembly)
        {
            return true;
        }

        public override ILPostProcessResult Process(ICompiledAssembly compiledAssembly)
        {
            if (!this.WillProcess(compiledAssembly))
            {
                return null;
            }

            var assemblyDefinition = AssemblyDefinitionFor(compiledAssembly);

            if (assemblyDefinition == null)
            {
                return null;
            }

            try
            {
                var bindableObjects = assemblyDefinition
                    .MainModule
                    .GetTypes()
                    .Where(t => t.BaseType != null && t.BaseType.FullName == typeof(BindableObject).FullName);

                var properties = bindableObjects
                    .SelectMany(t => t.Properties)
                    .Where(HasNotifyOnValueChangedAttribute)
                    .Where(IsAutoImplementedProperty);

                var anyInjected = false;

                foreach (var property in properties)
                {
                    InjectProperty(assemblyDefinition, property);
                    anyInjected = true;
                }

                if (!anyInjected)
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Logger.Error($"{e.Message}\n{e.StackTrace}");
            }

            using (MemoryStream pe = new MemoryStream())
            {
                using (MemoryStream pdb = new MemoryStream())
                {
                    var writerParameters = new WriterParameters
                    {
                        SymbolWriterProvider = new PortablePdbWriterProvider(),
                        SymbolStream = pdb,
                        WriteSymbols = true,
                    };

                    assemblyDefinition?.Write(pe, writerParameters);

                    var inMemoryAssembly = new InMemoryAssembly(pe.ToArray(), pdb.ToArray());
                    return new ILPostProcessResult(inMemoryAssembly, Logger.RetrieveAllMessages());
                }
            }
        }

        private static AssemblyDefinition AssemblyDefinitionFor(ICompiledAssembly compiledAssembly)
        {
            var assemblyResolver = new SimpleAssemblyResolver(compiledAssembly);
            var readerParameters = new ReaderParameters
            {
                SymbolStream = new MemoryStream(compiledAssembly.InMemoryAssembly.PdbData),
                SymbolReaderProvider = new PortablePdbReaderProvider(),
                AssemblyResolver = assemblyResolver,
                ReflectionImporterProvider = new SimpleReflectionImporterProvider(),
                ReadingMode = ReadingMode.Immediate,
            };

            var assemblyDefinition =
                AssemblyDefinition.ReadAssembly(
                    new MemoryStream(compiledAssembly.InMemoryAssembly.PeData),
                    readerParameters);

            assemblyResolver.AddAssemblyDefinitionBeingOperatedOn(assemblyDefinition);

            return assemblyDefinition;
        }

        private static void InjectProperty(AssemblyDefinition assembly, PropertyDefinition property)
        {
            // 创建属性 set 方法的新方法体
            MethodBody setMethodBody = new MethodBody(property.SetMethod);
            ILProcessor processor = setMethodBody.GetILProcessor();

            processor.Clear();

            // 创建字段引用
            FieldDefinition backingField = GetBackingField(property);
            FieldReference backingFieldRef = assembly.MainModule.ImportReference(backingField);

            // 加载当前实例到堆栈上
            processor.Emit(OpCodes.Ldarg_0);

            // 将当前实例转换为 BindableObject 类型
            TypeReference bindableObjectTypeRef = assembly.MainModule.ImportReference(typeof(BindableObject));
            processor.Emit(OpCodes.Castclass, bindableObjectTypeRef);

            // 加载字段的引用
            processor.Emit(OpCodes.Ldarg_0);
            processor.Emit(OpCodes.Ldflda, backingFieldRef);

            // 加载新值到堆栈上
            processor.Emit(OpCodes.Ldarg_1);

            // 加载属性名到堆栈上
            processor.Emit(OpCodes.Ldstr, property.Name);

            // 调用 SetValue 方法
            MethodReference setValueMethodRef = CreateSetValueMethodReference(bindableObjectTypeRef, backingFieldRef.FieldType);
            processor.Emit(OpCodes.Callvirt, setValueMethodRef);

            // 丢弃返回值
            processor.Emit(OpCodes.Pop);

            // 返回
            processor.Emit(OpCodes.Ret);

            // 替换原有的 set 方法
            property.SetMethod.Body = setMethodBody;
        }

        private static bool HasNotifyOnValueChangedAttribute(PropertyDefinition property)
        {
            return property.CustomAttributes.Any(attr => attr.AttributeType.FullName == typeof(NotifyOnValueChangedAttribute).FullName);
        }

        private static bool IsAutoImplementedProperty(PropertyDefinition property)
        {
            var setMethodBody = property.SetMethod.Body;
            if (setMethodBody.Instructions.Count < 4)
            {
                return false;
            }

            if (setMethodBody.Instructions[0].OpCode != OpCodes.Ldarg_0)
            {
                return false;
            }

            if (setMethodBody.Instructions[1].OpCode != OpCodes.Ldarg_1)
            {
                return false;
            }

            if (setMethodBody.Instructions[2].OpCode != OpCodes.Stfld)
            {
                return false;
            }

            var operand = setMethodBody.Instructions[2].Operand;
            if (!(operand is FieldDefinition fieldDefinition))
            {
                return false;
            }

            if (fieldDefinition.Name != $"<{property.Name}>k__BackingField")
            {
                return false;
            }

            if (setMethodBody.Instructions[3].OpCode != OpCodes.Ret)
            {
                return false;
            }

            return true;
        }

        private static FieldDefinition GetBackingField(PropertyDefinition property)
        {
            if (property.SetMethod != null)
            {
                MethodBody setMethodBody = property.SetMethod.Body;
                Collection<Instruction> instructions = setMethodBody.Instructions;

                // 寻找存储字段的指令
                foreach (var instruction in instructions)
                {
                    if (instruction.OpCode == OpCodes.Stfld || instruction.OpCode == OpCodes.Stsfld)
                    {
                        if (instruction.Operand is FieldDefinition associatedField)
                        {
                            return associatedField;
                        }

                        return null;
                    }
                }
            }

            return null;
        }

        private static MethodReference CreateSetValueMethodReference(TypeReference targetType, TypeReference genericArgumentType)
        {
            MethodReference genericMethod = targetType.Module.ImportReference(typeof(BindableObject).GetMethod("SetValue", BindingFlags.NonPublic | BindingFlags.Instance));

            GenericInstanceMethod setValueGenericInstance = new GenericInstanceMethod(genericMethod);
            setValueGenericInstance.GenericArguments.Add(genericArgumentType);

            MethodReference setValueMethodRef = targetType.Module.ImportReference(setValueGenericInstance);

            return setValueMethodRef;
        }
    }
}
