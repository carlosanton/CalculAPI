using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorService.Server.Helpers
{
    public class OperationString
    {
        public static string ConvertArrayInText(string[] values, string operation)
        {
            var opertation = String.Empty;

            opertation = "{0}" + String.Join(operation, values);

            opertation += " = {1}";

            return opertation;
        }
    }
}
