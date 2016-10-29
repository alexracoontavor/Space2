using System;

namespace Assets.Infrastructure.Architecture.Logger.SimpleLog
{
    [Serializable]
    public class LogEntries
    {
        public LogEntry[] Entries;

        public LogEntries(LogEntry[] entries)
        {
            Entries = entries;
        }
    }
}