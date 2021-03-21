using CalculatorService.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorService.Client.Services
{
    public interface IService
    {
        void Add(Sums sums, string clientID);
        void Query(User user);
        void Sqrt(Sqrt sqrt, string clientID);
        void Divide(Division division, string clientID);
        void Subtraction(Subtraction subtraction, string clientID);
        void Factor(Factor factor, string clientID);
        void GetLogFile(Date date);
    }
}
