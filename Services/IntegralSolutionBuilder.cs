using System.ComponentModel;
using System.Linq;
using System.Threading;
using IntegratorJr.Exceptions;
using IntegratorJr.IntegralSolvers;
using IntegratorJr.Models;

namespace IntegratorJr.Services
{
    public class IntegralSolutionBuilder
    {
        public IntegralSolution BuildIntegralSolution(IIntegralSolver integralSolver, FunctionData functionData)
        {
            return new IntegralSolution
            {
                Value = integralSolver.SolveIntegral(functionData),
                Name = GetNameOfIntegralSolver(integralSolver)
            };
        }

        private string GetNameOfIntegralSolver(IIntegralSolver integralSolver)
        {
            return integralSolver
                    .GetType()
                    .GetCustomAttributes(typeof(DisplayNameAttribute), true)
                    .Cast<DisplayNameAttribute>().SingleOrDefault()
                    ?.DisplayName
                ?? throw new IntegralSolverHasNotNameException($"{integralSolver.GetType().Name} не имеет атрибута DisplayName");
        }
    }
}