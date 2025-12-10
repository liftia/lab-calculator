// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CalculatorDemo.Engine
{
    /// <summary>
    /// Manages calculation history trail without UI dependencies.
    /// Uses an accumulator pattern for testability - UI reads TrailText property to update display.
    /// </summary>
    public interface IPaperTrail
    {
        /// <summary>
        /// Gets the accumulated trail text containing all calculation history.
        /// Format: "operands = result\n" for each completed calculation.
        /// </summary>
        string TrailText { get; }

        /// <summary>
        /// Sets the pending operation arguments (e.g., "5 + 3").
        /// These arguments will be combined with the result when AddResult is called.
        /// </summary>
        /// <param name="arguments">The operation expression string.</param>
        void AddArguments(string arguments);

        /// <summary>
        /// Completes the operation by appending "arguments = result" to the trail.
        /// </summary>
        /// <param name="result">The result of the calculation.</param>
        void AddResult(string result);

        /// <summary>
        /// Clears all history and pending arguments.
        /// </summary>
        void Clear();
    }
}
