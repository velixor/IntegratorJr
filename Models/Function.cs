using System;

namespace IntegratorJr.Models
{
    public class Function
    {
        public Function(Func<double, double> func, string functionString)
        {
            Func = func;
            FunctionString = functionString;
        }

        public string FunctionString { get; }
        public Func<double, double> Func { get; }
    }
}