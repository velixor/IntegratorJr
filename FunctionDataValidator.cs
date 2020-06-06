using System;
using IntegratorJr.Exceptions;
using IntegratorJr.IntegralSolvers;
using IntegratorJr.Models;
using org.mariuszgromada.math.mxparser;

namespace IntegratorJr
{
    internal class FunctionDataValidator
    {
        private FunctionData _fd;

        public void Validate(FunctionData functionData)
        {
            _fd = functionData;

            ValidateStep();
            ValidateLimits();
            ValidateFunction();
        }

        private void ValidateLimits()
        {
            if (Math.Abs(_fd.Left - _fd.Right) < GlobalConstants.Tolerance)
                throw new FunctionDataException("Пределы интегрирования не могут быть равны");
        }

        private void ValidateStep()
        {
            if (_fd.Step > _fd.Right - _fd.Left)
                throw new FunctionDataException("Шаг не может быть больше, чем расстояние между пределами интегрирования");
        }


        private void ValidateFunction()
        {
            var function = _fd.Function.FunctionString;

            var arg = new Argument("x", 0);
            var e = new Expression(function, arg);

            if (!e.checkSyntax())
                throw new FunctionStringInvalidException(e.getErrorMessage());
        }
    }
}