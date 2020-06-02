using System;

namespace IntegratorJr.Models
{
    public class Function
    {
        public Function(Func<double, double> func)
        {
            Func = func;
        }

        public Func<double, double> Func { get; }
    }
}