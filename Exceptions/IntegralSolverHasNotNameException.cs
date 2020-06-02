using System;

namespace IntegratorJr.Exceptions
{
    public class IntegralSolverHasNotNameException : Exception
    {
        public IntegralSolverHasNotNameException(string message) : base(message)
        {
        }
    }
}