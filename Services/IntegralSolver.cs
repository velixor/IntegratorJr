using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegratorJr.IntegralSolvers;
using IntegratorJr.Models;

namespace IntegratorJr.Services
{
    internal class IntegralSolver
    {
        private readonly IntegralSolutionBuilder _integralSolutionBuilder;

        public IntegralSolver()
        {
            _integralSolutionBuilder = new IntegralSolutionBuilder();
        }

        public Task<IEnumerable<IntegralSolution>> BuildIntegralSolutionsAsync(FunctionData functionData)
        {
            return Task.Run(() => BuildIntegralSolutions(functionData));
        }

        private IEnumerable<IntegralSolution> BuildIntegralSolutions(FunctionData f)
        {
            var integralSolvers = GetIntegralSolvers();
            
            return integralSolvers.Select(x => _integralSolutionBuilder.BuildIntegralSolution(x, f));
        }

        private IEnumerable<IIntegralSolver> GetIntegralSolvers()
        {
            var integralSolverTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(IIntegralSolver).IsAssignableFrom(x) && !x.IsInterface);

            return integralSolverTypes
                .Select(Activator.CreateInstance)
                .Cast<IIntegralSolver>();
        }
    }
}