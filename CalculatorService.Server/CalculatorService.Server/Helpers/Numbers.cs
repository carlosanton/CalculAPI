using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorService.Server.Helpers
{
    public class Numbers
    {
        public static bool AllNumbers(string[] values)
        {
            bool allNumbers = true;

            for(var i = 0; i < values.Length; i++)
            {
                double num;
                if(!double.TryParse(values[i], NumberStyles.Number, CultureInfo.CreateSpecificCulture("en-US"), out num))
                {
                    allNumbers = false;
                    break;
                }
            }

            return allNumbers;
        }

        public static double[] ConvertStringArrayToDoubleArray (string[] values)
        {
            List<double> convertedArray = new List<double>();

            for(var i = 0; i < values.Length; i++)
            {
                double convertedValue = 0;
                if(Double.TryParse(values[i], out convertedValue))
                {
                    convertedArray.Add(convertedValue);
                }
            }

            return convertedArray.ToArray();
        }
    }
}
