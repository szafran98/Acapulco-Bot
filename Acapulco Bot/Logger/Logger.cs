using System;
using System.Collections.Generic;

namespace Acapulco_Bot
{
    public class Log
    {
        public string content;
        public int type;
    }

    class Logger
    {
        private List<Log> _logs = new List<Log>();

        public List<Log> GetLogs()
        {
            return _logs;
        }

        public void Append(string logContent, int logType)
        {
            _logs.Add(new Log() {
                content = $"[{DateTime.Now}] {logContent}",
                type = logType
            });
        }
    }
}
