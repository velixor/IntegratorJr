using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using IntegratorJr.Models;
using JetBrains.Annotations;

namespace IntegratorJr.IntegralSolvers
{
    [DisplayName("трапеций")]
    [UsedImplicitly]
    public class TrapezoidSolver : BaseIntegralSolver
    {
        public override double SolveIntegral(FunctionData fd)
        {
            Initialize(fd);
            
            return h * ((f(x0) + f(xn)) / 2 + StepValues().Sum());
        }

        private IEnumerable<double> StepValues()
        {
            var x = x0 + h;
            while (x < xn)
            {
                yield return f(x);
                x += h;
            }
        }
    }
}