// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;

namespace CalculatorDemo
{
    /// <summary>
    /// Core calculator engine that handles all calculation logic.
    /// Separates business logic from UI concerns for better testability and maintainability.
    /// </summary>
    public class CalculatorEngine : ICalculator
    {
        private readonly IPaperTrail _paperTrail;
        private Operation _lastOperation;
        private string _lastValue;
        private string _memoryValue;
        private string _display;
        private bool _eraseDisplay;

        /// <summary>
        /// Initializes a new instance of the CalculatorEngine class.
        /// </summary>
        /// <param name="paperTrail">The paper trail instance for recording calculation history.</param>
        public CalculatorEngine(IPaperTrail paperTrail)
        {
            _paperTrail = paperTrail ?? throw new ArgumentNullException(nameof(paperTrail));
            _lastOperation = Operation.None;
            _lastValue = string.Empty;
            _memoryValue = string.Empty;
            _display = string.Empty;
            _eraseDisplay = false;

            // Initialize display with 0
            ProcessKey('0');
            _eraseDisplay = true;
        }

        /// <summary>
        /// Gets the current display value.
        /// </summary>
        public string Display => string.IsNullOrEmpty(_display) ? "0" : _display;

        /// <summary>
        /// Gets or sets the current memory value.
        /// </summary>
        public double Memory
        {
            get
            {
                if (string.IsNullOrEmpty(_memoryValue))
                    return 0.0;
                return Convert.ToDouble(_memoryValue, CultureInfo.InvariantCulture);
            }
            private set
            {
                _memoryValue = value.ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets the paper trail containing calculation history.
        /// </summary>
        public IPaperTrail PaperTrail => _paperTrail;

        /// <summary>
        /// Gets the last entered value for calculations.
        /// </summary>
        private string LastValue
        {
            get
            {
                if (string.IsNullOrEmpty(_lastValue))
                    return "0";
                return _lastValue;
            }
            set { _lastValue = value; }
        }

        /// <summary>
        /// Processes a digit or decimal point input.
        /// </summary>
        /// <param name="c">The character to process (0-9, '.', or backspace '\b').</param>
        public void ProcessKey(char c)
        {
            if (_eraseDisplay)
            {
                _display = string.Empty;
                _eraseDisplay = false;
            }
            AddToDisplay(c);
        }

        /// <summary>
        /// Processes a calculator operation.
        /// </summary>
        /// <param name="operation">The operation to perform.</param>
        public void ProcessOperation(Operation operation)
        {
            switch (operation)
            {
                case Operation.Negate:
                    _lastOperation = Operation.Negate;
                    LastValue = _display;
                    CalculateResults();
                    LastValue = _display;
                    _eraseDisplay = true;
                    _lastOperation = Operation.None;
                    break;

                case Operation.Divide:
                    if (_eraseDisplay)
                    {
                        _lastOperation = Operation.Divide;
                        break;
                    }
                    CalculateResults();
                    _lastOperation = Operation.Divide;
                    LastValue = _display;
                    _eraseDisplay = true;
                    break;

                case Operation.Multiply:
                    if (_eraseDisplay)
                    {
                        _lastOperation = Operation.Multiply;
                        break;
                    }
                    CalculateResults();
                    _lastOperation = Operation.Multiply;
                    LastValue = _display;
                    _eraseDisplay = true;
                    break;

                case Operation.Subtract:
                    if (_eraseDisplay)
                    {
                        _lastOperation = Operation.Subtract;
                        break;
                    }
                    CalculateResults();
                    _lastOperation = Operation.Subtract;
                    LastValue = _display;
                    _eraseDisplay = true;
                    break;

                case Operation.Add:
                    if (_eraseDisplay)
                    {
                        _lastOperation = Operation.Add;
                        break;
                    }
                    CalculateResults();
                    _lastOperation = Operation.Add;
                    LastValue = _display;
                    _eraseDisplay = true;
                    break;

                case Operation.None: // Equals
                    if (_eraseDisplay)
                        break;
                    CalculateResults();
                    _eraseDisplay = true;
                    _lastOperation = Operation.None;
                    LastValue = _display;
                    break;

                case Operation.Sqrt:
                    _lastOperation = Operation.Sqrt;
                    LastValue = _display;
                    CalculateResults();
                    LastValue = _display;
                    _eraseDisplay = true;
                    _lastOperation = Operation.None;
                    break;

                case Operation.Percent:
                    if (_eraseDisplay)
                    {
                        _lastOperation = Operation.Percent;
                        break;
                    }
                    CalculateResults();
                    _lastOperation = Operation.Percent;
                    LastValue = _display;
                    _eraseDisplay = true;
                    break;

                case Operation.Reciprocal:
                    _lastOperation = Operation.Reciprocal;
                    LastValue = _display;
                    CalculateResults();
                    LastValue = _display;
                    _eraseDisplay = true;
                    _lastOperation = Operation.None;
                    break;
            }
        }

        /// <summary>
        /// Clears all calculator state (display, last value, history).
        /// </summary>
        public void ClearAll()
        {
            _lastOperation = Operation.None;
            _display = string.Empty;
            LastValue = string.Empty;
            _paperTrail.Clear();
        }

        /// <summary>
        /// Clears the current entry, resetting to the last value.
        /// </summary>
        public void ClearEntry()
        {
            _lastOperation = Operation.None;
            _display = LastValue;
        }

        /// <summary>
        /// Clears the memory value.
        /// </summary>
        public void MemoryClear()
        {
            Memory = 0.0;
            _memoryValue = string.Empty;
        }

        /// <summary>
        /// Stores the current display value in memory.
        /// </summary>
        public void MemoryStore()
        {
            Memory = Convert.ToDouble(_display, CultureInfo.InvariantCulture);
            _eraseDisplay = true;
        }

        /// <summary>
        /// Recalls the memory value to the display.
        /// </summary>
        public void MemoryRecall()
        {
            _display = Memory.ToString(CultureInfo.InvariantCulture);
            _eraseDisplay = false;
        }

        /// <summary>
        /// Adds the current display value to memory.
        /// </summary>
        public void MemoryAdd()
        {
            double d = Memory + Convert.ToDouble(_display, CultureInfo.InvariantCulture);
            Memory = d;
            _eraseDisplay = true;
        }

        /// <summary>
        /// Performs a calculation with two operands.
        /// This method is primarily used for unit testing.
        /// </summary>
        /// <param name="operation">The operation to perform.</param>
        /// <param name="operand1">The first operand.</param>
        /// <param name="operand2">The second operand.</param>
        /// <returns>The result of the calculation.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the result is invalid (NaN or Infinity).</exception>
        public double Calculate(Operation operation, double operand1, double operand2)
        {
            double result;

            switch (operation)
            {
                case Operation.Add:
                    result = operand1 + operand2;
                    break;

                case Operation.Subtract:
                    result = operand1 - operand2;
                    break;

                case Operation.Multiply:
                    result = operand1 * operand2;
                    break;

                case Operation.Divide:
                    result = operand1 / operand2;
                    break;

                case Operation.Percent:
                    result = (operand1 * operand2) / 100.0;
                    break;

                case Operation.Sqrt:
                    result = Math.Sqrt(operand1);
                    break;

                case Operation.Reciprocal:
                    result = 1.0 / operand1;
                    break;

                case Operation.Negate:
                    result = operand1 * -1.0;
                    break;

                default:
                    result = operand2;
                    break;
            }

            ValidateResult(result);
            return result;
        }

        /// <summary>
        /// Gets the formatted memory display text.
        /// </summary>
        /// <returns>A string representation of the memory state.</returns>
        public string GetMemoryDisplayText()
        {
            if (!string.IsNullOrEmpty(_memoryValue))
                return "Memory: " + _memoryValue;
            return "Memory: [empty]";
        }

        /// <summary>
        /// Performs the calculation based on the last operation.
        /// </summary>
        private void CalculateResults()
        {
            if (_lastOperation == Operation.None)
                return;

            double d = PerformCalculation(_lastOperation);
            _display = d.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Performs the actual calculation and records it in the paper trail.
        /// </summary>
        /// <param name="operation">The operation to perform.</param>
        /// <returns>The result of the calculation.</returns>
        private double PerformCalculation(Operation operation)
        {
            double result = 0.0;
            double lastVal = Convert.ToDouble(LastValue, CultureInfo.InvariantCulture);
            double displayVal = Convert.ToDouble(_display, CultureInfo.InvariantCulture);

            try
            {
                switch (operation)
                {
                    case Operation.Divide:
                        _paperTrail.AddArguments(LastValue + " / " + _display);
                        result = lastVal / displayVal;
                        ValidateResult(result);
                        _paperTrail.AddResult(result.ToString(CultureInfo.InvariantCulture));
                        break;

                    case Operation.Add:
                        _paperTrail.AddArguments(LastValue + " + " + _display);
                        result = lastVal + displayVal;
                        ValidateResult(result);
                        _paperTrail.AddResult(result.ToString(CultureInfo.InvariantCulture));
                        break;

                    case Operation.Multiply:
                        _paperTrail.AddArguments(LastValue + " * " + _display);
                        result = lastVal * displayVal;
                        ValidateResult(result);
                        _paperTrail.AddResult(result.ToString(CultureInfo.InvariantCulture));
                        break;

                    case Operation.Percent:
                        _paperTrail.AddArguments(LastValue + " % " + _display);
                        result = (lastVal * displayVal) / 100.0;
                        ValidateResult(result);
                        _paperTrail.AddResult(result.ToString(CultureInfo.InvariantCulture));
                        break;

                    case Operation.Subtract:
                        _paperTrail.AddArguments(LastValue + " - " + _display);
                        result = lastVal - displayVal;
                        ValidateResult(result);
                        _paperTrail.AddResult(result.ToString(CultureInfo.InvariantCulture));
                        break;

                    case Operation.Sqrt:
                        _paperTrail.AddArguments("Sqrt( " + LastValue + " )");
                        result = Math.Sqrt(lastVal);
                        ValidateResult(result);
                        _paperTrail.AddResult(result.ToString(CultureInfo.InvariantCulture));
                        break;

                    case Operation.Reciprocal:
                        _paperTrail.AddArguments("1 / " + LastValue);
                        result = 1.0 / lastVal;
                        ValidateResult(result);
                        _paperTrail.AddResult(result.ToString(CultureInfo.InvariantCulture));
                        break;

                    case Operation.Negate:
                        result = lastVal * -1.0;
                        break;
                }
            }
            catch (InvalidOperationException)
            {
                result = 0;
                _paperTrail.AddResult("Error");
                throw;
            }

            return result;
        }

        /// <summary>
        /// Validates that the result is a valid number.
        /// </summary>
        /// <param name="result">The result to validate.</param>
        /// <exception cref="InvalidOperationException">Thrown when the result is NaN or Infinity.</exception>
        private void ValidateResult(double result)
        {
            if (double.IsNegativeInfinity(result) || double.IsPositiveInfinity(result) || double.IsNaN(result))
                throw new InvalidOperationException("Illegal value");
        }

        /// <summary>
        /// Adds a character to the display.
        /// </summary>
        /// <param name="c">The character to add.</param>
        private void AddToDisplay(char c)
        {
            if (c == '.')
            {
                if (_display.IndexOf('.', 0) >= 0)
                    return;
                _display = _display + c;
            }
            else if (c >= '0' && c <= '9')
            {
                _display = _display + c;
            }
            else if (c == '\b')
            {
                if (_display.Length <= 1)
                    _display = string.Empty;
                else
                {
                    int i = _display.Length;
                    _display = _display.Remove(i - 1, 1);
                }
            }
        }
    }
}
