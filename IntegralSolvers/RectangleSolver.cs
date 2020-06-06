using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using IntegratorJr.Models;
using JetBrains.Annotations;

namespace IntegratorJr.IntegralSolvers
{
    [DisplayName("средних")]
    [UsedImplicitly]
    public class RectangleSolver : BaseIntegralSolver
    {
        public override double SolveIntegral(FunctionData functionData)
        {
            var (a, b, h, f) = UngroupFunctionData(functionData);

            var (wholeSegments, tail) = GetWholeSegmentsAndTail(a, b, h);

            var mainArea = CalcMainArea(wholeSegments, h, f);
            var tailArea = CalcTailAreaIfExist(tail, b, f);

            return mainArea + tailArea;
        }

        private (IEnumerable<double>, double) GetWholeSegmentsAndTail(double a, double b, double h)
        {
            var segments = GetSegments(a, b, h).ToArray();

            var wholeSegments = segments.Take(segments.Length - 1);

            return (wholeSegments, segments.Last());
        }

        private static IEnumerable<double> GetSegments(double a, double b, double h)
        {
            var x = a;

            while (x <= b)
            {
                yield return x;

                x += h;
            }
        }

        private static double CalcMainArea(IEnumerable<double> wholeSegments, double h, Func<double, double> f)
        {
            var middlesOfSegments = wholeSegments
                .Select(x => x + h / 2);

            return h * middlesOfSegments
                .Select(f)
                .Sum();
        }

        private double CalcTailAreaIfExist(double last, double b, Func<double, double> f)
        {
            if (Math.Abs(last - b) < GlobalConstants.Tolerance) return 0;

            var h = b - last;
            return f(last + h / 2) * h;
        }
    }
}