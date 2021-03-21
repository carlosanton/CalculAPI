using CalculatorService.Server.Objects;
using CalculatorService.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorService.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogOperations _logOperations;

        public CalculatorController(ILogOperations logOperations)
        {
            _logOperations = logOperations;
        }

        [HttpPost("add")]
        public IActionResult Add(Sums sums)
        {
            try
            {
                // Methods
                IMethods methods = new Methods();
                methods.GetAddResults(sums);

                // If have tracking-id, save log
                _logOperations.SetOperationsLog(Request.Headers["X-Evi-Tracking-Id"], new Operations() { Calculation = "Sum", Operation = String.Format(Helpers.OperationString.ConvertArrayInText(sums.Addends, " + "), String.Empty, sums.Total) });

                return Ok(new { Sum = sums.Total });
            }
            catch (Exception ex)
            {
                Errors error = new Errors
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = "Internal Server Error",
                    ErrorStatus = 400
                };

                return BadRequest(error);
            }
        }

        [HttpPost("sub")]
        public IActionResult Subtraction(Subtraction subtraction)
        {
            try
            {
                // Methods
                IMethods methods = new Methods();
                methods.GetSubResults(subtraction);

                string[] values = new string[] { subtraction.Minuend, (Convert.ToDecimal(subtraction.Subtrahend) < 0 ? (Convert.ToDecimal(subtraction.Subtrahend) * -1).ToString() : subtraction.Subtrahend) };

                // If have tracking-id, save log
                _logOperations.SetOperationsLog(Request.Headers["X-Evi-Tracking-Id"], new Operations() { Calculation = "Sub", Operation = String.Format(Helpers.OperationString.ConvertArrayInText(values, " - "), String.Empty, subtraction.Total) });

                return Ok(new { Difference = subtraction.Total });
            }
            catch (Exception ex)
            {
                Errors error = new Errors
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = "Internal Server Error",
                    ErrorStatus = 400
                };

                return BadRequest(error);
            }
        }

        [HttpPost("mult")]
        public IActionResult Product(Factor factor)
        {
            try
            {
                // Methods
                IMethods methods = new Methods();
                methods.GetProductResults(factor);
                
                // If have tracking-id, save log
                _logOperations.SetOperationsLog(Request.Headers["X-Evi-Tracking-Id"], new Operations() { Calculation = "Mul", Operation = String.Format(Helpers.OperationString.ConvertArrayInText(factor.Factors, " * "), String.Empty, factor.Total) });

                return Ok(new { Product = factor.Total });
            }
            catch (Exception ex)
            {
                Errors error = new Errors
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = "Internal Server Error",
                    ErrorStatus = 400
                };

                return BadRequest(error);
            }
        }

        [HttpPost("div")]
        public IActionResult Division(Division division)
        {
            try
            {
                // Methods
                IMethods methods = new Methods();
                methods.GetDivResults(division);

                string[] values = new string[] { division.Dividend, division.Divisor };

                // If have tracking-id, save log
                _logOperations.SetOperationsLog(Request.Headers["X-Evi-Tracking-Id"], new Operations() { Calculation = "Div", Operation = String.Format(Helpers.OperationString.ConvertArrayInText(values, " / "), String.Empty, division.Total) });

                return Ok(new { Quotient = division.Quotient, Remainder = division.Remainder });
            }
            catch (Exception ex)
            {
                Errors error = new Errors
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = "Internal Server Error",
                    ErrorStatus = 400
                };

                return BadRequest(error);
            }
        }

        [HttpPost("sqrt")]
        public IActionResult Sqrt(Sqrt sqrt)
        {
            try
            {
                // Methods
                IMethods methods = new Methods();
                methods.GetSqrtResults(sqrt);

                // If have tracking-id, save log
                _logOperations.SetOperationsLog(Request.Headers["X-Evi-Tracking-Id"], new Operations() { Calculation = "Sqrt", Operation = String.Format(Helpers.OperationString.ConvertArrayInText(new string[] { sqrt.Number }, String.Empty), "v~", sqrt.Total) });

                return Ok(new { Square = sqrt.Total });
            }
            catch (Exception ex)
            {
                Errors error = new Errors
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = "Internal Server Error",
                    ErrorStatus = 400
                };

                return BadRequest(error);
            }
        }

        [HttpPost("query")]
        public IActionResult Query(User user)
        {
            var userLog = _logOperations.OperationsLog.Count > 0 && _logOperations.OperationsLog.ContainsKey(user.Id) ? _logOperations.OperationsLog[user.Id] : null;

            if (userLog == null)
            {
                Errors error = new Errors
                {
                    ErrorMessage = $"No logs found for user: '{user.Id}'",
                    ErrorCode = "Internal Server Error",
                    ErrorStatus = 400
                };

                return BadRequest(error);
            }

            return Ok(new { Operations = _logOperations.OperationsLog[user.Id] });
        }
    }
}
