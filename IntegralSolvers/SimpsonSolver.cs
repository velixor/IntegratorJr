using System;
using System.ComponentModel;
using IntegratorJr.Models;
using JetBrains.Annotations;

namespace IntegratorJr.IntegralSolvers
{
    [DisplayName("Метод Симпсона"), UsedImplicitly]
    public class SimpsonSolver : IIntegralSolver
    {
        // TODO
        public double SolveIntegral(FunctionData functionData)
        {
            return Math.PI;
        }
    }
}