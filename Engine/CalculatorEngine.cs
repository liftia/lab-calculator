// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace CalculatorDemo.Engine
{
    /// <summary>
    /// Pure calculation engine implementing arithmetic operations.
    /// This class has no UI dependencies and can be easily unit tested.
    /// All operations are stateless and deterministic.
    /// </summary>
    public class CalculatorEngine : ICalculator
    {
        /// <inheritdoc/>
        public double Add(double a, double b)
        {
            return a + b;
        }

        /// <inheritdoc/>
        public double Subtract(double a, double b)
        {
            return a - b;
        }

        /// <inheritdoc/>
        public double Multiply(double a, double b)
        {
            return a * b;
        }

        /// <inheritdoc/>
        public double Divide(double a, double b)
        {
            return a / b;
        }

        /// <inheritdoc/>
        public double Percent(double a, double b)
        {
            return (a * b) / 100.0;
        }

        /// <inheritdoc/>
        public double SquareRoot(double value)
        {
            return Math.Sqrt(value);
        }

        /// <inheritdoc/>
        public double Reciprocal(double value)
        {
            return 1.0 / value;
        }

        /// <inheritdoc/>
        public double Negate(double value)
        {
            return value * -1.0;
        }

        /// <inheritdoc/>
        public bool IsValidResult(double result)
        {
            return !double.IsNaN(result)
                && !double.IsNegativeInfinity(result)
                && !double.IsPositiveInfinity(result);
        }
    }
}
