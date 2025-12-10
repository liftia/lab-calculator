// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CalculatorDemo.Engine
{
    /// <summary>
    /// Defines the calculator operations for binary and unary calculations.
    /// Used to track the pending operation state in the calculator.
    /// </summary>
    public enum Operation
    {
        /// <summary>No operation pending.</summary>
        None,

        /// <summary>Division operation (a / b).</summary>
        Divide,

        /// <summary>Multiplication operation (a * b).</summary>
        Multiply,

        /// <summary>Subtraction operation (a - b).</summary>
        Subtract,

        /// <summary>Addition operation (a + b).</summary>
        Add,

        /// <summary>Percentage operation ((a * b) / 100).</summary>
        Percent,

        /// <summary>Square root operation (sqrt(a)).</summary>
        Sqrt,

        /// <summary>Reciprocal operation (1 / a).</summary>
        OneX,

        /// <summary>Negation operation (a * -1).</summary>
        Negate
    }
}
