using CalculatorService.Server.Objects;
using CalculatorService.Shared;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;

namespace CalculatorService.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogOperations _logOperations;
        private readonly ILog _logs;

        public CalculatorController(ILogOperations logOperations, ILog logs)
        {
            _logOperations = logOperations;
            _logs = logs;
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

                _logs.SetLogs(DateTime.Now.ToString("dd/MM/yyyy"), JsonConvert.SerializeObject(new { Type = "Success", User = Request.Headers["X-Evi-Tracking-Id"], EntryData = JsonConvert.SerializeObject(sums.Addends), Operation = new Operations() { Calculation = "Sum", Operation = String.Format(Helpers.OperationString.ConvertArrayInText(sums.Addends, " + "), String.Empty, sums.Total) } }));

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

                _logs.SetLogs(DateTime.Now.ToString("dd/MM/yyyy"), JsonConvert.SerializeObject(new { Type = "Error", User = Request.Headers["X-Evi-Tracking-Id"], EntryData = JsonConvert.SerializeObject(sums.Addends), Operation = error }));

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

                _logs.SetLogs(DateTime.Now.ToString("dd/MM/yyyy"), JsonConvert.SerializeObject(new { Type = "Success", User = Request.Headers["X-Evi-Tracking-Id"], EntryData = JsonConvert.SerializeObject(new { Minuend = subtraction.Minuend, Subtrahend = subtraction.Subtrahend }), Operation = new Operations() { Calculation = "Sub", Operation = String.Format(Helpers.OperationString.ConvertArrayInText(values, " - "), String.Empty, subtraction.Total) } }));

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

                _logs.SetLogs(DateTime.Now.ToString("dd/MM/yyyy"), JsonConvert.SerializeObject(new { Type = "Error", User = Request.Headers["X-Evi-Tracking-Id"], EntryData = JsonConvert.SerializeObject(new { Minuend = subtraction.Minuend, Subtrahend = subtraction.Subtrahend }), Operation = error }));

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
                _logOperations.SetOperationsLog(Request.Headers["X-Evi-Tracking-Id"], new Operations() { Calculation = "Mult", Operation = String.Format(Helpers.OperationString.ConvertArrayInText(factor.Factors, " * "), String.Empty, factor.Total) });

                _logs.SetLogs(DateTime.Now.ToString("dd/MM/yyyy"), JsonConvert.SerializeObject(new { Type = "Success", User = Request.Headers["X-Evi-Tracking-Id"], EntryData = JsonConvert.SerializeObject(factor.Factors), Operation = new Operations() { Calculation = "Mult", Operation = String.Format(Helpers.OperationString.ConvertArrayInText(factor.Factors, " * "), String.Empty, factor.Total) } }));

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

                _logs.SetLogs(DateTime.Now.ToString("dd/MM/yyyy"), JsonConvert.SerializeObject(new { Type = "Error", User = Request.Headers["X-Evi-Tracking-Id"], EntryData = JsonConvert.SerializeObject(factor.Factors), Operation = error }));

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

                _logs.SetLogs(DateTime.Now.ToString("dd/MM/yyyy"), JsonConvert.SerializeObject(new { Type = "Success", User = Request.Headers["X-Evi-Tracking-Id"], EntryData = JsonConvert.SerializeObject(new { Dividend = division.Dividend, Divisor = division.Divisor }), Operation = new Operations() { Calculation = "Div", Operation = String.Format(Helpers.OperationString.ConvertArrayInText(values, " / "), String.Empty, division.Total) } }));

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

                _logs.SetLogs(DateTime.Now.ToString("dd/MM/yyyy"), JsonConvert.SerializeObject(new { Type = "Error", User = Request.Headers["X-Evi-Tracking-Id"], EntryData = JsonConvert.SerializeObject(new { Dividend = division.Dividend, Divisor = division.Divisor }), Operation = error }));

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

                _logs.SetLogs(DateTime.Now.ToString("dd/MM/yyyy"), JsonConvert.SerializeObject(new { Type = "Success", User = Request.Headers["X-Evi-Tracking-Id"], EntryData = JsonConvert.SerializeObject(sqrt.Number), Operation = new Operations() { Calculation = "Sqrt", Operation = String.Format(Helpers.OperationString.ConvertArrayInText(new string[] { sqrt.Number }, String.Empty), "v~", sqrt.Total) } }));

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

                _logs.SetLogs(DateTime.Now.ToString("dd/MM/yyyy"), JsonConvert.SerializeObject(new { Type = "Error", User = Request.Headers["X-Evi-Tracking-Id"], EntryData = JsonConvert.SerializeObject(sqrt.Number), Operation = error }));

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

                _logs.SetLogs(DateTime.Now.ToString("dd/MM/yyyy"), JsonConvert.SerializeObject(new { Type = "Error", User = Request.Headers["X-Evi-Tracking-Id"], EntryData = JsonConvert.SerializeObject(user), Operation = error }));

                return BadRequest(error);
            }



            _logs.SetLogs(DateTime.Now.ToString("dd/MM/yyyy"), JsonConvert.SerializeObject(new { Type = "Success", User = Request.Headers["X-Evi-Tracking-Id"], EntryData = JsonConvert.SerializeObject(user), Operation = new Operations() { Calculation = "Query", Operation = String.Empty } }));

            return Ok(new { Operations = _logOperations.OperationsLog[user.Id] });
        }

        [HttpGet]
        public IActionResult GetLogFile(string encodedDate)
        {
            using (MemoryStream mem = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(mem))
                {
                    var base64EncodedBytes = System.Convert.FromBase64String(encodedDate);
                    var date = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

                    if(_logs.Logs.ContainsKey(date))
                    {
                        var logs = _logs.GetLogs()[date];
                        if (logs.Count == 0)
                        {
                            writer.WriteLine($"No logs for date {date}");
                        }
                        else
                        {
                            foreach (var log in logs)
                            {
                                writer.WriteLine(log);
                            }
                        }
                    }
                    else
                    {
                        writer.WriteLine($"No logs for date {date}");
                    }
                }
                return File(mem.ToArray(), "text/plain");
            }
        }
    }
}
