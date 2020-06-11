using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using IntegratorJr.Models;
using JetBrains.Annotations;

namespace IntegratorJr.IntegralSolvers
{
    [DisplayName("Симпсона")]
    [UsedImplicitly]
    public class SimpsonSolver : BaseIntegralSolver
    {
        public override double SolveIntegral(FunctionData functionData)
        {
            Initialize(functionData);
            return h / 3 * (f(x0) + f(xn) + 2 * EvenSteps().Sum() + 4 * OddSteps().Sum());
        }

        private IEnumerable<double> EvenSteps()
        {
            var x = x0 + 2 * h;
            while (x < xn)
            {
                yield return f(x);
                x += 2 * h;
            }
        }

        private IEnumerable<double> OddSteps()
        {
            var x = x0 + h;
            while (x < xn)
            {
                yield return f(x);
                x += 2 * h;
            }
        }
    }
}