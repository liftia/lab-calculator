// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CalculatorDemo
{
    /// <summary>
    /// Tracks the history of calculations performed by the calculator.
    /// Records each calculation's arguments and results in a readable format.
    /// </summary>
    public class PaperTrail : IPaperTrail
    {
        private string _text;
        private string _currentArguments;

        /// <summary>
        /// Initializes a new instance of the PaperTrail class.
        /// </summary>
        public PaperTrail()
        {
            _text = string.Empty;
            _currentArguments = string.Empty;
        }

        /// <summary>
        /// Gets the current paper trail text containing all recorded calculations.
        /// </summary>
        public string Text => _text;

        /// <summary>
        /// Records the arguments of a calculation (e.g., "5 + 3").
        /// The arguments are stored temporarily until AddResult is called.
        /// </summary>
        /// <param name="arguments">The calculation expression to record.</param>
        public void AddArguments(string arguments)
        {
            _currentArguments = arguments;
        }

        /// <summary>
        /// Records the result of the last calculation and appends it to the paper trail.
        /// Combines the stored arguments with the result in the format "args = result".
        /// </summary>
        /// <param name="result">The result value to record.</param>
        public void AddResult(string result)
        {
            _text += _currentArguments + " = " + result + "\n";
        }

        /// <summary>
        /// Clears all recorded calculations from the paper trail.
        /// </summary>
        public void Clear()
        {
            _text = string.Empty;
            _currentArguments = string.Empty;
        }
    }
}
