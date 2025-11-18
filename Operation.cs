// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CalculatorDemo
{
    /// <summary>
    /// Enumeration of all supported calculator operations.
    /// </summary>
    public enum Operation
    {
        /// <summary>
        /// No operation selected.
        /// </summary>
        None,

        /// <summary>
        /// Division operation (a / b).
        /// </summary>
        Divide,

        /// <summary>
        /// Multiplication operation (a * b).
        /// </summary>
        Multiply,

        /// <summary>
        /// Subtraction operation (a - b).
        /// </summary>
        Subtract,

        /// <summary>
        /// Addition operation (a + b).
        /// </summary>
        Add,

        /// <summary>
        /// Percentage operation (a * b / 100).
        /// </summary>
        Percent,

        /// <summary>
        /// Square root operation.
        /// </summary>
        Sqrt,

        /// <summary>
        /// Reciprocal operation (1 / x).
        /// </summary>
        Reciprocal,

        /// <summary>
        /// Negation operation (-x).
        /// </summary>
        Negate
    }
}
