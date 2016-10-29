using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Assets.Infrastructure.Architecture.Logger.SimpleLog
{
    [Serializable]
    public class LogEntry
    {
        public string CallerClass;
        public string CallerMethod;
        public string Message;
        public Dictionary<string, object> SystemInfo;
        public string[] Tags;
        public string Timestamp;

        public LogEntry(string message, string[] tags)
        {
            Tags = tags ?? new string[0];
            Message = message;
            Timestamp = DateTime.Now.ToLongTimeString();

            StackTrace stackTrace = new StackTrace();
            StackFrame frame = stackTrace.GetFrame(2);

            if (frame == null)
            {
                CallerClass = "Could not get stack frame 2";
                CallerMethod = "stackTrace dump: " + stackTrace;
                return;
            }

            MethodBase method = frame.GetMethod();
            Type methodCallerType = method.DeclaringType;
            CallerClass = methodCallerType != null ? methodCallerType.ToString() : "Unknown";
            CallerMethod = method.Name;
        }

        public LogEntry(string message, string tag) : this(message, new[] {tag})
        {
        }
    }
}