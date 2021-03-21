using CalculatorService.Shared;
using System;
using System.Linq;

namespace CalculatorService.Server.Objects
{
    public class Methods : IMethods
    {
        public void GetAddResults(Sums sums)
        {
            try
            {
                // Check if the values are numbers
                var allNumbers = CalculatorService.Server.Helpers.Numbers.AllNumbers(sums.Addends);
                if (allNumbers)
                {
                    var valuesToOperate = CalculatorService.Server.Helpers.Numbers.ConvertStringArrayToDoubleArray(sums.Addends);

                    var result = valuesToOperate.Sum(x => x);

                    sums.Total = result;
                }
                else
                {
                    throw new Exception("Some of the elements are not numerics");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void GetSubResults(Subtraction subtraction)
        {
            try
            {
                string[] values = new string[] { subtraction.Minuend, subtraction.Subtrahend };
                var allNumbers = CalculatorService.Server.Helpers.Numbers.AllNumbers(values);
                if (allNumbers)
                {
                    var valuesToOperate = CalculatorService.Server.Helpers.Numbers.ConvertStringArrayToDoubleArray(values);
                    // If subtrahend < 0, change it to positive number
                    valuesToOperate[1] = valuesToOperate[1] < 0 ? valuesToOperate[1] * -1 : valuesToOperate[1];

                    double result = valuesToOperate[0] - valuesToOperate[1];

                    subtraction.Total = result;
                }
                else
                {
                    throw new Exception("Some of the elements are not numerics");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void GetProductResults(Factor factor)
        {
            try
            {
                // Check if the values are numbers
                var allNumbers = CalculatorService.Server.Helpers.Numbers.AllNumbers(factor.Factors);
                if (allNumbers)
                {
                    var valuesToOperate = CalculatorService.Server.Helpers.Numbers.ConvertStringArrayToDoubleArray(factor.Factors);

                    double result = 1;

                    for (var i = 0; i < valuesToOperate.Length; i++)
                    {
                        result = result * valuesToOperate[i];
                    }

                    factor.Total = result;
                }
                else
                {
                    throw new Exception("Some of the elements are not numerics");
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void GetDivResults(Division division)
        {
            try
            {
                string[] values = new string[] { division.Dividend, division.Divisor };
                var allNumbers = CalculatorService.Server.Helpers.Numbers.AllNumbers(values);
                if (allNumbers)
                {
                    var valuesToOperate = CalculatorService.Server.Helpers.Numbers.ConvertStringArrayToDoubleArray(values);

                    if (valuesToOperate[1] == 0)
                    {
                        throw new Exception("Cannot divide by 0");
                    }

                    double result = valuesToOperate[0] / valuesToOperate[1];

                    division.Total = result;
                    division.Quotient = (int)result;
                    division.Remainder = (int)(valuesToOperate[0] % valuesToOperate[1]);
                }
                else
                {
                    throw new Exception("Some of the elements are not numerics");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void GetSqrtResults(Sqrt sqrt)
        {
            try
            {
                string[] values = new string[] { sqrt.Number };
                // Check if the values are numbers
                var allNumbers = CalculatorService.Server.Helpers.Numbers.AllNumbers(values);
                if (allNumbers)
                {
                    if (Convert.ToDouble(sqrt.Number) >= 0)
                    {
                        var result = Math.Sqrt(Convert.ToDouble(sqrt.Number));

                        sqrt.Total = result;
                    }
                    else
                    {
                        throw new Exception("The value cannot be negative");
                    }
                }
                else
                {
                    throw new Exception("Some of the elements are not numerics");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
