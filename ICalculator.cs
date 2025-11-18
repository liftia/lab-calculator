// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CalculatorDemo
{
    /// <summary>
    /// Interface defining the core calculator operations.
    /// Provides methods for performing calculations, managing display, and memory operations.
    /// </summary>
    public interface ICalculator
    {
        /// <summary>
        /// Gets the current display value.
        /// </summary>
        string Display { get; }

        /// <summary>
        /// Gets the current memory value.
        /// </summary>
        double Memory { get; }

        /// <summary>
        /// Gets the paper trail containing calculation history.
        /// </summary>
        IPaperTrail PaperTrail { get; }

        /// <summary>
        /// Processes a digit or decimal point input.
        /// </summary>
        /// <param name="c">The character to process (0-9, '.', or backspace).</param>
        void ProcessKey(char c);

        /// <summary>
        /// Processes a calculator operation.
        /// </summary>
        /// <param name="operation">The operation to perform.</param>
        void ProcessOperation(Operation operation);

        /// <summary>
        /// Clears all calculator state (display, memory, history).
        /// </summary>
        void ClearAll();

        /// <summary>
        /// Clears the current entry, resetting to the last value.
        /// </summary>
        void ClearEntry();

        /// <summary>
        /// Clears the memory value.
        /// </summary>
        void MemoryClear();

        /// <summary>
        /// Stores the current display value in memory.
        /// </summary>
        void MemoryStore();

        /// <summary>
        /// Recalls the memory value to the display.
        /// </summary>
        void MemoryRecall();

        /// <summary>
        /// Adds the current display value to memory.
        /// </summary>
        void MemoryAdd();

        /// <summary>
        /// Performs a calculation with two operands.
        /// </summary>
        /// <param name="operation">The operation to perform.</param>
        /// <param name="operand1">The first operand.</param>
        /// <param name="operand2">The second operand.</param>
        /// <returns>The result of the calculation.</returns>
        double Calculate(Operation operation, double operand1, double operand2);
    }
}
