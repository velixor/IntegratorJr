using System;

namespace IntegratorJr.Exceptions
{
    public class FunctionStringInvalidException : Exception
    {
        public FunctionStringInvalidException(string message) : base(message)
        {
        }
    }
}