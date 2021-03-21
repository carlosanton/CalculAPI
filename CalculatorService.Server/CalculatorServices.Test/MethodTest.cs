using CalculatorService.Shared;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorServices.Test
{
    public class MethodTest
    {
        [TestCase(new object[] { "1", "-67" }, "-66")]
        [TestCase(new object[] { "1", "500", "4" }, "505")]
        [TestCase(new object[] { "1", "5", "b", "100" }, "Some of the elements are not numerics")]
        public void AddResultsTest(object[] values, string expected)
        {
            try
            {
                string[] arrayString = Array.ConvertAll<object, string>(values, CalculatorService.Server.Helpers.Objects.ConvertObjectToString);

                Sums sums = new Sums
                {
                    Addends = arrayString
                };

                new CalculatorService.Server.Objects.Methods().GetAddResults(sums);

                Assert.AreEqual(Convert.ToDouble(expected), sums.Total);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(expected, ex.Message);
            }
        }

        [TestCase("1", "-67", "-66")]
        [TestCase("3", "-7", "-4")]
        [TestCase("7", "4", "3")]
        [TestCase("5", "b", "Some of the elements are not numerics")]
        public void SubResultsTest(string minuend, string subtrahend, string expected)
        {
            try
            {
                Subtraction subtraction = new Subtraction
                {
                    Minuend = minuend,
                    Subtrahend = subtrahend
                };

                new CalculatorService.Server.Objects.Methods().GetSubResults(subtraction);

                Assert.AreEqual(Convert.ToDouble(expected), subtraction.Total);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(expected, ex.Message);
            }
        }

        [TestCase(new object[] { "8", "3", "2" }, "48")]
        [TestCase(new object[] { "9", "-1" }, "-9")]
        [TestCase(new object[] { "1", "5", "b", "100" }, "Some of the elements are not numerics")]
        public void ProductResultsTest(object[] values, string expected)
        {
            try
            {
                string[] arrayString = Array.ConvertAll<object, string>(values, CalculatorService.Server.Helpers.Objects.ConvertObjectToString);

                Factor factor = new Factor
                {
                    Factors = arrayString
                };

                new CalculatorService.Server.Objects.Methods().GetProductResults(factor);

                Assert.AreEqual(Convert.ToDouble(expected), factor.Total);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(expected, ex.Message);
            }
        }

        [TestCase("11", "2", "5", "1")]
        [TestCase("5", "b", "Some of the elements are not numerics", null)]
        public void DivResultsTest(string dividend, string divisor, string expectedQuotient, string expectedRemainder)
        {
            try
            {
                Division division = new Division
                {
                    Dividend = dividend,
                    Divisor = divisor
                };

                new CalculatorService.Server.Objects.Methods().GetDivResults(division);

                Assert.AreEqual(Convert.ToDouble(expectedQuotient), division.Quotient);

                Assert.AreEqual(Convert.ToDouble(expectedRemainder), division.Remainder);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(expectedQuotient, ex.Message);
            }
        }

        [TestCase("16", "4")]
        [TestCase("b", "Some of the elements are not numerics")]
        public void SqrtResultsTest(string number, string expected)
        {
            try
            {
                Sqrt sqrt = new Sqrt
                {
                    Number = number
                };

                new CalculatorService.Server.Objects.Methods().GetSqrtResults(sqrt);

                Assert.AreEqual(Convert.ToDouble(expected), sqrt.Total);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(expected, ex.Message);
            }
        }
    }
}
