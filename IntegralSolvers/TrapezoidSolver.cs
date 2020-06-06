using System;
using System.ComponentModel;
using IntegratorJr.Models;
using JetBrains.Annotations;

namespace IntegratorJr.IntegralSolvers
{
    [DisplayName("трапеций")]
    [UsedImplicitly]
    public class TrapezoidSolver : IIntegralSolver
    {
        public double SolveIntegral(FunctionData functionData)
        {
            return Math.PI;
        }
    }
}