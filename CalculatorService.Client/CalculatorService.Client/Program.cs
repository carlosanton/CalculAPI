using CalculatorService.Client.Services;
using CalculatorService.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CalculatorService.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> listOptions = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "x" };
            var menuOption = String.Empty;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter a client id (can be empty):");
                var clientID = Console.ReadLine();

                do
                {
                    Console.WriteLine("Choose an option:");
                    Console.WriteLine("1) Add");
                    Console.WriteLine("2) Subtraction");
                    Console.WriteLine("3) Factor");
                    Console.WriteLine("4) Division");
                    Console.WriteLine("5) Square");
                    Console.WriteLine("6) Query");
                    Console.WriteLine("7) Download log file by date");
                    Console.WriteLine("x) Exit");
                    Console.Write("\r\nSelect an option: ");
                    menuOption = Console.ReadLine();

                    switch (menuOption)
                    {
                        case "1":
                            Add(clientID);
                            Console.ReadLine();
                            break;
                        case "2":
                            Subtraction(clientID);
                            Console.ReadLine();
                            break;
                        case "3":
                            Factor(clientID);
                            Console.ReadLine();
                            break;
                        case "4":
                            Division(clientID);
                            Console.ReadLine();
                            break;
                        case "5":
                            Square(clientID);
                            Console.ReadLine();
                            break;
                        case "6":
                            Query(clientID);
                            Console.ReadLine();
                            break;
                        case "7":
                            DownloadFile();
                            Console.ReadLine();
                            break;
                        case "x":
                            break;
                        default:
                            Console.WriteLine("Please, select an option from menu");
                            break;
                    }
                } while (!listOptions.Contains(menuOption));
                
            } while (menuOption.ToLower() != "x");
        }

        private static void DownloadFile()
        {
            var yearSearch = String.Empty;
            var monthSearch = String.Empty;
            var daySearch = String.Empty;
            DateTime tryDate = DateTime.Now;
            bool isCorrectYear = false;
            bool isCorrectMonth = false;
            bool isCorrectDay = false;
            do
            {
                Console.Write("\r\nEnter year (yyyy): ");
                yearSearch = Console.ReadLine();

                bool isNumeric = int.TryParse(yearSearch, out _);

                if ((yearSearch.Length == 4 && isNumeric) && isNumeric)
                {
                    isCorrectYear = true;
                }

                if (!isNumeric)
                {
                    Console.WriteLine("The year must be a numeric value");
                }

            } while (!isCorrectYear);

            do
            {
                Console.Write("\r\nEnter month (mm): ");
                monthSearch = Console.ReadLine();

                bool isNumeric = int.TryParse(monthSearch, out _);

                if ((monthSearch.Length == 1 || monthSearch.Length == 2) && isNumeric)
                {
                    isCorrectMonth = true;
                }

                if (!isNumeric)
                {
                    Console.WriteLine("The month must be a numeric value");
                }

            } while (!isCorrectMonth);

            do
            {
                Console.Write("\r\nEnter day (dd): ");
                daySearch = Console.ReadLine();

                bool isNumeric = int.TryParse(daySearch, out _);

                if ((daySearch.Length == 1 || daySearch.Length == 2) && isNumeric)
                {
                    isCorrectDay = true;
                }

                if (!isNumeric)
                {
                    Console.WriteLine("The day must be a numeric value");
                }

            } while (!isCorrectDay);

            Date date = new Date
            {
                Year = yearSearch,
                Month = monthSearch.Length == 1 ? "0" + monthSearch : monthSearch,
                Day = daySearch.Length == 1 ? "0" + daySearch : daySearch
            };

            IService _service = new Service();
            _service.GetLogFile(date);
        }

        private static void Factor(string clientID)
        {
            int numberOfElements = QuantityOfNumbers();

            Factor factor = new Factor();
            factor.Factors = GetListOfElements(numberOfElements).ToArray();

            IService _service = new Service();
            _service.Factor(factor, clientID);
        }

        private static void Subtraction(string clientID)
        {
            var minuend = String.Empty;
            var subtrahend = String.Empty;
            int tryNumber = 0;
            bool isNumeric = false;
            do
            {
                Console.Write("\r\nEnter minuend: ");
                minuend = Console.ReadLine();

                isNumeric = int.TryParse(minuend, out tryNumber);

                if (!isNumeric)
                {
                    Console.WriteLine("The minuend must be a numeric value");
                }

            } while (!isNumeric);
            do
            {
                Console.Write("\r\nEnter subtrahend: ");
                subtrahend = Console.ReadLine();

                isNumeric = int.TryParse(subtrahend, out tryNumber);

                if (!isNumeric)
                {
                    Console.WriteLine("The subtrahend must be a numeric value");
                }

            } while (!isNumeric);

            Subtraction subtraction = new Subtraction
            {
                Minuend = minuend,
                Subtrahend = subtrahend
            };

            IService _service = new Service();
            _service.Subtraction(subtraction, clientID);
        }

        private static void Division(string clientID)
        {
            var dividend = String.Empty;
            var divisor = String.Empty;
            int tryNumber = 0;
            bool isNumeric = false;
            do
            {
                Console.Write("\r\nEnter dividend: ");
                dividend = Console.ReadLine();

                isNumeric = int.TryParse(dividend, out tryNumber);

                if (!isNumeric)
                {
                    Console.WriteLine("The dividend must be a numeric value");
                }

            } while (!isNumeric);
            do
            {
                Console.Write("\r\nEnter divisor: ");
                divisor = Console.ReadLine();

                isNumeric = int.TryParse(divisor, out tryNumber);

                if (!isNumeric)
                {
                    Console.WriteLine("The divisor must be a numeric value");
                }

            } while (!isNumeric);

            Division division = new Division
            {
                Dividend = dividend,
                Divisor = divisor
            };

            IService _service = new Service();
            _service.Divide(division, clientID);
        }

        private static void Square(string clientID)
        {
            var numberSqrt = String.Empty;
            int sqrtNumber = 0;
            bool isNumeric = false;
            do
            {
                Console.Write("\r\nEnter number: ");
                numberSqrt = Console.ReadLine();

                isNumeric = int.TryParse(numberSqrt, out sqrtNumber);

                if (!isNumeric)
                {
                    Console.WriteLine("The data must be a numeric value");
                }
                if (sqrtNumber <= 0)
                {
                    Console.WriteLine("The data must be grater than 0");
                }

            } while (!isNumeric || sqrtNumber <= 0);

            Sqrt sqrt = new Sqrt
            {
                Number = numberSqrt
            };

            IService _service = new Service();
            _service.Sqrt(sqrt, clientID);
        }

        private static void Query(string clientID)
        {
            User user = new User
            {
                Id = clientID
            };
            IService _service = new Service();
            _service.Query(user);
        }

        private static void Add(string clientID)
        {
            int numberOfElements = QuantityOfNumbers();

            Sums sums = new Sums();
            sums.Addends = GetListOfElements(numberOfElements).ToArray();

            IService _service = new Service();
            _service.Add(sums, clientID);
        }

        private static int QuantityOfNumbers()
        {
            var numbers = String.Empty;
            int numberOfElements = 0;
            bool isNumeric = false;
            do
            {
                Console.Write("\r\nHow many numbers do you want to use: ");
                numbers = Console.ReadLine();

                isNumeric = int.TryParse(numbers, out numberOfElements);

                if (!isNumeric)
                {
                    Console.WriteLine("The data must be a numeric value");
                }
                if (numberOfElements <= 0)
                {
                    Console.WriteLine("The data must be grater than 0");
                }

            } while (!isNumeric || numberOfElements <= 0);

            return numberOfElements;
        }

        private static List<string> GetListOfElements(int numberOfElements)
        {
            List<string> elements = new List<string>();
            for (var i = 0; i < numberOfElements; i++)
            {
                Console.WriteLine($"Introduce element {(i + 1)}: ");
                elements.Add(Console.ReadLine());
            }

            return elements;
        }
    }
}
