using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using IntegratorJr.Models;

namespace IntegratorJr.IntegralSolvers
{
    public interface IIntegralSolver
    {
        double SolveIntegral(FunctionData functionData);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public abstract class BaseIntegralSolver : IIntegralSolver
    {
        protected double h;
        protected double xn;
        protected double x0;
        protected Func<double, double> f;

        public abstract double SolveIntegral(FunctionData functionData);

        protected (double a, double b, double h, Func<double, double> f) UngroupFunctionData(FunctionData functionData)
        {
            return (functionData.Left, functionData.Right, functionData.Step, functionData.Function.Func);
        }

        protected void Initialize(FunctionData functionData)
        {
            f = functionData.Function.Func;
            x0 = functionData.Left;
            xn = functionData.Right;
            h = functionData.Step;
        }
    }
}