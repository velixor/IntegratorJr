﻿using System;
using System.ComponentModel;
using IntegratorJr.Models;
using JetBrains.Annotations;

namespace IntegratorJr.IntegralSolvers
{
    [DisplayName("Симпсона")]
    [UsedImplicitly]
    public class SimpsonSolver : IIntegralSolver
    {
        public double SolveIntegral(FunctionData functionData)
        {
            return Math.PI;
        }
    }
}