// -----------------------------------------------------------------------
// <copyright file="Logger.cs" company="AillieoTech">
// Copyright (c) AillieoTech. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AillieoUtils.EasyBindings.CodeGen
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Unity.CompilationPipeline.Common.Diagnostics;

    internal class Logger
    {
        private static readonly List<DiagnosticMessage> messages = new List<DiagnosticMessage>();

        private Logger()
        {
        }

        public static void Warning(string text)
        {
            var frame = new StackFrame(1);
            messages.Add(new DiagnosticMessage()
            {
                DiagnosticType = DiagnosticType.Warning,
                MessageData = text,
                File = frame.GetFileName(),
                Line = frame.GetFileLineNumber(),
                Column = frame.GetFileColumnNumber(),
            });
        }

        public static void Error(string text)
        {
            var frame = new StackFrame(1);
            messages.Add(new DiagnosticMessage()
            {
                DiagnosticType = DiagnosticType.Error,
                MessageData = text,
                File = frame.GetFileName(),
                Line = frame.GetFileLineNumber(),
                Column = frame.GetFileColumnNumber(),
            });
        }

        internal static List<DiagnosticMessage> RetrieveAllMessages()
        {
            var results = new List<DiagnosticMessage>();
            results.AddRange(messages);
            messages.Clear();
            return results;
        }
    }
}
