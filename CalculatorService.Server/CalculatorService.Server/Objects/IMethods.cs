using CalculatorService.Shared;

namespace CalculatorService.Server.Objects
{
    public interface IMethods
    {
        void GetAddResults(Sums sums);
        void GetSubResults(Subtraction subtraction);
        void GetProductResults(Factor factor);
        void GetDivResults(Division division);
        void GetSqrtResults(Sqrt sqrt);
    }
}
