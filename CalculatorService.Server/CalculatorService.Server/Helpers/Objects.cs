using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorService.Server.Helpers
{
    public class Objects
    {
        public static string ConvertObjectToString(object obj)
        {
            return obj?.ToString() ?? string.Empty;
        }
    }
}
