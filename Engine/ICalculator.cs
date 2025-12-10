// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CalculatorDemo.Engine
{
    /// <summary>
    /// Defines pure arithmetic operations for the calculator.
    /// All methods are stateless and have no side effects, making them easily testable.
    /// </summary>
    public interface ICalculator
    {
        /// <summary>
        /// Adds two numbers.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        /// <returns>The sum of a and b.</returns>
        double Add(double a, double b);

        /// <summary>
        /// Subtracts the second number from the first.
        /// </summary>
        /// <param name="a">First operand (minuend).</param>
        /// <param name="b">Second operand (subtrahend).</param>
        /// <returns>The difference (a - b).</returns>
        double Subtract(double a, double b);

        /// <summary>
        /// Multiplies two numbers.
        /// </summary>
        /// <param name="a">First operand.</param>
        /// <param name="b">Second operand.</param>
        /// <returns>The product of a and b.</returns>
        double Multiply(double a, double b);

        /// <summary>
        /// Divides the first number by the second.
        /// </summary>
        /// <param name="a">Dividend.</param>
        /// <param name="b">Divisor.</param>
        /// <returns>The quotient (a / b). Returns Infinity if b is zero.</returns>
        double Divide(double a, double b);

        /// <summary>
        /// Calculates the percentage of a number.
        /// </summary>
        /// <param name="a">Base value.</param>
        /// <param name="b">Percentage value.</param>
        /// <returns>The result of (a * b) / 100.</returns>
        /// <remarks>
        /// This differs from Windows Calculator behavior but provides more intuitive results.
        /// For example: Percent(200, 50) returns 100 (50% of 200).
        /// </remarks>
        double Percent(double a, double b);

        /// <summary>
        /// Calculates the square root of a number.
        /// </summary>
        /// <param name="value">The value to calculate square root for.</param>
        /// <returns>The square root of the value. Returns NaN for negative numbers.</returns>
        double SquareRoot(double value);

        /// <summary>
        /// Calculates the reciprocal (1/x) of a number.
        /// </summary>
        /// <param name="value">The value to calculate reciprocal for.</param>
        /// <returns>The reciprocal (1 / value). Returns Infinity if value is zero.</returns>
        double Reciprocal(double value);

        /// <summary>
        /// Negates a number (changes its sign).
        /// </summary>
        /// <param name="value">The value to negate.</param>
        /// <returns>The negated value (value * -1).</returns>
        double Negate(double value);

        /// <summary>
        /// Validates if a result is a valid number (not NaN or Infinity).
        /// </summary>
        /// <param name="result">The result to validate.</param>
        /// <returns>True if the result is a valid finite number; otherwise, false.</returns>
        bool IsValidResult(double result);
    }
}
