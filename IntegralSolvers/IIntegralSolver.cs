using IntegratorJr.Models;

namespace IntegratorJr.IntegralSolvers
{
    public interface IIntegralSolver
    {
        double SolveIntegral(FunctionData functionData);
    }
}