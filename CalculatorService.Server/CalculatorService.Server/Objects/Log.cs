using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorService.Server.Objects
{
    public class Log : ILog
    {
        public Log()
        {
            Logs = new Dictionary<string, List<string>>();
        }

        public IDictionary<string, List<string>> Logs { get; private set; }

        public IDictionary<string, List<string>> GetLogs()
        {
            return Logs;
        }

        public void SetLogs(string date, string log)
        {
            if (Logs.ContainsKey(date))
            {
                Logs[date].Add(log);
            }
            else
            {
                Logs.Add(date, new List<string>() { log });
            }
        }
    }
}
