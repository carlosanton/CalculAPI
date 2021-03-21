using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorService.Shared
{
    public class Errors
    {
        public string ErrorCode { get; set; }
        public int ErrorStatus { get; set; }
        public string ErrorMessage { get; set; }
    }
}
