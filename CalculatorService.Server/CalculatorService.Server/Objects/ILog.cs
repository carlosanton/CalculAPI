using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorService.Server.Objects
{
    public interface ILog
    {
        IDictionary<string, List<string>> Logs { get; }
        IDictionary<string, List<string>> GetLogs();
        void SetLogs(string date, string log);
    }
}
