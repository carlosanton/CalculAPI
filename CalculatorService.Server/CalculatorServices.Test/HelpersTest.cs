using NUnit.Framework;
using System;

namespace CalculatorServices.Test
{
    public class HelpersTest
    {
        [TestCase(new object[] { "1", "-67" }, ExpectedResult = true)]
        [TestCase(new object[] { "1", "500", "4" }, ExpectedResult = true)]
        [TestCase(new object[] { "1", "5", "b", "100" }, ExpectedResult = false)]
        public bool AllNumericsTest(object[] values)
        {
            string[] result = Array.ConvertAll<object, string>(values, CalculatorService.Server.Helpers.Objects.ConvertObjectToString);
            var testResult = CalculatorService.Server.Helpers.Numbers.AllNumbers(result);

            return testResult;
        }

        [TestCase(new object[] { "1", "-67" }, ExpectedResult = new double[] { 1, -67 })]
        [TestCase(new object[] { "1", "500", "4" }, ExpectedResult = new double[] { 1, 500, 4 })]
        public double[] StringArrayToDoubleArrayTest(object[] values)
        {
            string[] result = Array.ConvertAll<object, string>(values, CalculatorService.Server.Helpers.Objects.ConvertObjectToString);
            var testResult = CalculatorService.Server.Helpers.Numbers.ConvertStringArrayToDoubleArray(result);

            return testResult;
        }

        [TestCase(new object[] { "2", "2" })]
        public void ArrayInTextToAddTest(object[] values)
        {
            string[] result = Array.ConvertAll<object, string>(values, CalculatorService.Server.Helpers.Objects.ConvertObjectToString);
            var message = String.Format(CalculatorService.Server.Helpers.OperationString.ConvertArrayInText(result, " + "), String.Empty, 4);

            Assert.AreEqual("2 + 2 = 4", message);
        }

        [TestCase("3", "-7")]
        public void ArrayInTextToSubTest(string minuend, string subtrahend)
        {
            string[] values = new string[] { minuend, (Convert.ToDecimal(subtrahend) < 0 ? (Convert.ToDecimal(subtrahend) * -1).ToString() : subtrahend) };

            string[] result = Array.ConvertAll<object, string>(values, CalculatorService.Server.Helpers.Objects.ConvertObjectToString);
            var message = String.Format(CalculatorService.Server.Helpers.OperationString.ConvertArrayInText(result, " - "), String.Empty, -4);

            Assert.AreEqual("3 - 7 = -4", message);
        }

        [TestCase(new object[] { "8", "3", "2" })]
        public void ArrayInTextToFactorTest(object[] values)
        {
            string[] result = Array.ConvertAll<object, string>(values, CalculatorService.Server.Helpers.Objects.ConvertObjectToString);
            var message = String.Format(CalculatorService.Server.Helpers.OperationString.ConvertArrayInText(result, " * "), String.Empty, 48);

            Assert.AreEqual("8 * 3 * 2 = 48", message);
        }

        [TestCase("11", "2")]
        public void ArrayInTextToDivTest(string dividend, string divisor)
        {
            string[] values = new string[] { dividend, divisor };

            string[] result = Array.ConvertAll<object, string>(values, CalculatorService.Server.Helpers.Objects.ConvertObjectToString);
            var message = String.Format(CalculatorService.Server.Helpers.OperationString.ConvertArrayInText(result, " / "), String.Empty, 5.5);

            Assert.AreEqual("11 / 2 = 5,5", message);
        }

        [TestCase("16")]
        public void ArrayInTextToSqrtTest(string number)
        {
            string[] values = new string[] { number };

            string[] result = Array.ConvertAll<object, string>(values, CalculatorService.Server.Helpers.Objects.ConvertObjectToString);
            var message = String.Format(CalculatorService.Server.Helpers.OperationString.ConvertArrayInText(result, String.Empty), "v~", 4);

            Assert.AreEqual("v~16 = 4", message);
        }
    }
}
