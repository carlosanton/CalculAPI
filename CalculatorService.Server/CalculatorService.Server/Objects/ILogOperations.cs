using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorService.Server.Objects
{
    public interface ILogOperations
    {
        IDictionary<string, List<Operations>> OperationsLog { get; }
        IDictionary<string, List<Operations>> GetOperationsLog();
        void SetOperationsLog(string token, Operations operation);
    }
}
