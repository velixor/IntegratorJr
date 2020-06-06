using System;
using IntegratorJr.Models;

namespace IntegratorJr.IntegralSolvers
{
    public interface IIntegralSolver
    {
        double SolveIntegral(FunctionData functionData);
    }

    public abstract class BaseIntegralSolver : IIntegralSolver
    {
        public abstract double SolveIntegral(FunctionData functionData);

        protected (double a, double b, double h, Func<double, double> f) UngroupFunctionData(FunctionData functionData)
        {
            return (functionData.Left, functionData.Right, functionData.Step, functionData.Function.Func);
        }
    }
}