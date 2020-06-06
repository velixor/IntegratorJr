using System;

namespace IntegratorJr.Exceptions
{
    internal class FunctionDataException : Exception
    {
        public FunctionDataException(string message) : base(message)
        {
        }
    }
}