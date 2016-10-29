using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.Infrastructure.Architecture.Logger.SimpleLog
{
    /// <summary>
    /// Simple logger implementation. Generates timestamped, called-identified entries.
    /// Inherit from this and override to enable custom reporting
    /// </summary>
    public class SimpleLogger : ILogger
    {
        protected readonly List<LogEntry> Entries = new List<LogEntry>();
        public readonly int MaxEntries = 100;

        public SimpleLogger()
        {            
            Entries.Add(new LogEntry("Initializing Logger", new[] {"Initializing"}));

            //TODO - write continuously to file, delete it OnQuit; if exists on startup, treat it as error
        }

        public void Log(string text, string[] tags = null)
        {
            LogEntry(new LogEntry(text, AddTag(tags, "Log")));
            Debug.Log(text);
        }

        public void LogError(string text, string[] tags = null)
        {
            LogEntry(new LogEntry(text, AddTag(tags, "Error")));
            Debug.LogError(text);
        }

        public void LogWarning(string text, string[] tags = null)
        {
            LogEntry(new LogEntry(text, AddTag(tags, "Warning")));
            Debug.LogWarning(text);
        }

        public void LogException(Exception exception)
        {
            Debug.LogWarning(exception.Message);
            LogEntry(new LogEntry(GenerateSystemDataString(), AddTag(null, "System Data")));
            LogEntry(new LogEntry(exception.StackTrace, AddTag(null, "Exception")));
            SendLogReport();
        }

        protected virtual void LogEntry(LogEntry entry)
        {
            if (Entries.Count >= MaxEntries)
            {
                Entries.RemoveAt(0);
            }

            Entries.Add(entry);
        }

        public virtual void SendLogReport()
        {

        }

        protected virtual void ReportingFailed(Exception obj)
        {
            Debug.Log("Errors Report Failed: " + obj.Message);
        }

        protected virtual void ReportDone(object obj)
        {
            Debug.Log("Errors Report Done");
            Entries.Clear();
            //TODO - delete file
        }

        protected static string[] AddTag(string[] tags, string tag)
        {
            tags = tags ?? new string[] {};

            List<string> tagsList = tags.ToList();
            tagsList.Add(tag);
            tags = tagsList.ToArray();

            return tags;
        }

        public static string GenerateSystemDataString()
        {
            Dictionary<string, string> result =
                typeof(SystemInfo).GetProperties()
                    .ToDictionary(property => property.Name, property => property.GetValue(null, null).ToString());

            return string.Join
                (
                    ",",
                    result.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value)).ToArray()
                );
        }
    }
}