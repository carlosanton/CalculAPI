using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorService.Server.Objects
{
    public class Operations
    {
        public string Operation { get; set; }
        public string Calculation { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
