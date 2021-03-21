using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorService.Server.Objects
{
    public class LogOperations : ILogOperations
    {
        public LogOperations()
        {
            OperationsLog = new Dictionary<string, List<Operations>>();
        }

        public IDictionary<string, List<Operations>> OperationsLog { get; private set; }

        public IDictionary<string, List<Operations>> GetOperationsLog()
        {
            return OperationsLog;
        }

        public void SetOperationsLog(string token, Operations operation)
        {
            if(!string.IsNullOrEmpty(token))
            {
                if (OperationsLog.ContainsKey(token))
                {
                    OperationsLog[token].Add(operation);
                }
                else
                {
                    OperationsLog.Add(token, new List<Operations>() { operation });
                }
            }
        }
    }
}
