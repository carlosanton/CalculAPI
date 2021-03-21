namespace CalculatorService.Shared
{
    public class Division : Totals
    {
        public string Dividend { get; set; }
        public string Divisor { get; set; }
        public int Quotient { get; set; }
        public int Remainder { get; set; }
    }
}
