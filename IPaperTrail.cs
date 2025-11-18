// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CalculatorDemo
{
    /// <summary>
    /// Interface for tracking calculation history (paper trail).
    /// Allows recording of calculation arguments and results.
    /// </summary>
    public interface IPaperTrail
    {
        /// <summary>
        /// Gets the current paper trail text containing all recorded calculations.
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Records the arguments of a calculation (e.g., "5 + 3").
        /// </summary>
        /// <param name="arguments">The calculation expression to record.</param>
        void AddArguments(string arguments);

        /// <summary>
        /// Records the result of the last calculation and appends it to the paper trail.
        /// </summary>
        /// <param name="result">The result value to record.</param>
        void AddResult(string result);

        /// <summary>
        /// Clears all recorded calculations from the paper trail.
        /// </summary>
        void Clear();
    }
}
