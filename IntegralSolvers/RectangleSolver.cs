using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using IntegratorJr.Models;
using JetBrains.Annotations;

namespace IntegratorJr.IntegralSolvers
{
    [DisplayName("средних")]
    [UsedImplicitly]
    public class RectangleSolver : BaseIntegralSolver
    {
        public override double SolveIntegral(FunctionData fd)
        {
            Initialize(fd);
            return fd.Step * CalcStepMiddles(fd).Sum();
        }

        private IEnumerable<double> CalcStepMiddles(FunctionData fd)
        {
            var x = fd.Left;
            while (x < fd.Right)
            {
                yield return f(x + h / 2);
                x += h;
            }
        }
    }
}