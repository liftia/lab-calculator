// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CalculatorDemo.Engine;

namespace CalculatorDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// This class handles UI events and coordinates with the business logic layer (Engine).
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        /// <summary>
        /// Calculator engine for pure arithmetic operations.
        /// </summary>
        private readonly ICalculator _calculator;

        /// <summary>
        /// Paper trail for tracking calculation history.
        /// </summary>
        private readonly IPaperTrail _paper;

        /// <summary>
        /// Current pending operation.
        /// </summary>
        private Operation _lastOper;

        /// <summary>
        /// Previous operand value for binary operations.
        /// </summary>
        private string _lastVal;

        /// <summary>
        /// Memory storage value.
        /// </summary>
        private string _memVal;

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// Sets up the calculator engine and paper trail.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            _calculator = new CalculatorEngine();
            _paper = new PaperTrail();
            ProcessKey('0');
            EraseDisplay = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to erase the display on next digit input.
        /// </summary>
        private bool EraseDisplay { get; set; }

        /// <summary>
        /// Gets or sets the memory cell value.
        /// Handles conversion between string storage and double values.
        /// </summary>
        private double Memory
        {
            get
            {
                if (_memVal == string.Empty)
                    return 0.0;
                return Convert.ToDouble(_memVal);
            }
            set { _memVal = value.ToString(CultureInfo.InvariantCulture); }
        }

        /// <summary>
        /// Gets or sets the last value entered (previous operand for binary operations).
        /// Returns "0" if empty.
        /// </summary>
        private string LastValue
        {
            get
            {
                if (_lastVal == string.Empty)
                    return "0";
                return _lastVal;
            }
            set { _lastVal = value; }
        }

        /// <summary>
        /// Gets or sets the current calculator display value.
        /// </summary>
        private string Display { get; set; }

        /// <summary>
        /// Handles keyboard input for the calculator.
        /// Routes digits to ProcessKey and operators to ProcessOperation.
        /// </summary>
        private void OnWindowKeyDown(object sender, TextCompositionEventArgs e)
        {
            var s = e.Text;
            var c = (s.ToCharArray())[0];
            e.Handled = true;

            if ((c >= '0' && c <= '9') || c == '.' || c == '\b')
            {
                ProcessKey(c);
                return;
            }
            switch (c)
            {
                case '+':
                    ProcessOperation("BPlus");
                    break;
                case '-':
                    ProcessOperation("BMinus");
                    break;
                case '*':
                    ProcessOperation("BMultiply");
                    break;
                case '/':
                    ProcessOperation("BDivide");
                    break;
                case '%':
                    ProcessOperation("BPercent");
                    break;
                case '=':
                    ProcessOperation("BEqual");
                    break;
            }
        }

        /// <summary>
        /// Handles digit button clicks.
        /// Extracts the button content and processes it as a digit.
        /// </summary>
        private void DigitBtn_Click(object sender, RoutedEventArgs e)
        {
            var s = ((Button)sender).Content.ToString();
            var ids = s.ToCharArray();
            ProcessKey(ids[0]);
        }

        /// <summary>
        /// Processes a digit or decimal point input.
        /// Clears display if EraseDisplay flag is set.
        /// </summary>
        private void ProcessKey(char c)
        {
            if (EraseDisplay)
            {
                Display = string.Empty;
                EraseDisplay = false;
            }
            AddToDisplay(c);
        }

        /// <summary>
        /// Processes an operation button click.
        /// Handles binary operators, unary operators, memory operations, and clear functions.
        /// </summary>
        private void ProcessOperation(string s)
        {
            var d = 0.0;
            switch (s)
            {
                case "BPM": // +/- (negate)
                    _lastOper = Operation.Negate;
                    LastValue = Display;
                    CalcResults();
                    LastValue = Display;
                    EraseDisplay = true;
                    _lastOper = Operation.None;
                    break;
                case "BDevide": // Legacy name for divide (typo in original XAML)
                case "BDivide":
                    if (EraseDisplay)
                    {
                        _lastOper = Operation.Divide;
                        break;
                    }
                    CalcResults();
                    _lastOper = Operation.Divide;
                    LastValue = Display;
                    EraseDisplay = true;
                    break;
                case "BMultiply":
                    if (EraseDisplay)
                    {
                        _lastOper = Operation.Multiply;
                        break;
                    }
                    CalcResults();
                    _lastOper = Operation.Multiply;
                    LastValue = Display;
                    EraseDisplay = true;
                    break;
                case "BMinus":
                    if (EraseDisplay)
                    {
                        _lastOper = Operation.Subtract;
                        break;
                    }
                    CalcResults();
                    _lastOper = Operation.Subtract;
                    LastValue = Display;
                    EraseDisplay = true;
                    break;
                case "BPlus":
                    if (EraseDisplay)
                    {
                        _lastOper = Operation.Add;
                        break;
                    }
                    CalcResults();
                    _lastOper = Operation.Add;
                    LastValue = Display;
                    EraseDisplay = true;
                    break;
                case "BEqual":
                    if (EraseDisplay)
                        break;
                    CalcResults();
                    EraseDisplay = true;
                    _lastOper = Operation.None;
                    LastValue = Display;
                    break;
                case "BSqrt":
                    _lastOper = Operation.Sqrt;
                    LastValue = Display;
                    CalcResults();
                    LastValue = Display;
                    EraseDisplay = true;
                    _lastOper = Operation.None;
                    break;
                case "BPercent":
                    if (EraseDisplay)
                    {
                        _lastOper = Operation.Percent;
                        break;
                    }
                    CalcResults();
                    _lastOper = Operation.Percent;
                    LastValue = Display;
                    EraseDisplay = true;
                    break;
                case "BOneOver": // 1/x
                    _lastOper = Operation.OneX;
                    LastValue = Display;
                    CalcResults();
                    LastValue = Display;
                    EraseDisplay = true;
                    _lastOper = Operation.None;
                    break;
                case "BC": // Clear All
                    _lastOper = Operation.None;
                    Display = LastValue = string.Empty;
                    _paper.Clear();
                    PaperBox.Text = _paper.TrailText;
                    UpdateDisplay();
                    break;
                case "BCE": // Clear Entry
                    _lastOper = Operation.None;
                    Display = LastValue;
                    UpdateDisplay();
                    break;
                case "BMemClear":
                    Memory = 0.0F;
                    DisplayMemory();
                    break;
                case "BMemSave":
                    Memory = Convert.ToDouble(Display);
                    DisplayMemory();
                    EraseDisplay = true;
                    break;
                case "BMemRecall":
                    Display = Memory.ToString(CultureInfo.InvariantCulture);
                    UpdateDisplay();
                    EraseDisplay = false;
                    break;
                case "BMemPlus":
                    d = Memory + Convert.ToDouble(Display);
                    Memory = d;
                    DisplayMemory();
                    EraseDisplay = true;
                    break;
            }
        }

        /// <summary>
        /// Handles operator button clicks.
        /// Routes to ProcessOperation with the button name.
        /// </summary>
        private void OperBtn_Click(object sender, RoutedEventArgs e)
        {
            ProcessOperation(((Button)sender).Name);
        }

        /// <summary>
        /// Performs the calculation using the calculator engine.
        /// Delegates arithmetic operations to the engine and updates the paper trail.
        /// </summary>
        private double Calc(Operation lastOper)
        {
            var d = 0.0;
            var lastVal = Convert.ToDouble(LastValue);
            var displayVal = Convert.ToDouble(Display);

            try
            {
                switch (lastOper)
                {
                    case Operation.Divide:
                        _paper.AddArguments(LastValue + " / " + Display);
                        d = _calculator.Divide(lastVal, displayVal);
                        break;
                    case Operation.Add:
                        _paper.AddArguments(LastValue + " + " + Display);
                        d = _calculator.Add(lastVal, displayVal);
                        break;
                    case Operation.Multiply:
                        _paper.AddArguments(LastValue + " * " + Display);
                        d = _calculator.Multiply(lastVal, displayVal);
                        break;
                    case Operation.Percent:
                        _paper.AddArguments(LastValue + " % " + Display);
                        d = _calculator.Percent(lastVal, displayVal);
                        break;
                    case Operation.Subtract:
                        _paper.AddArguments(LastValue + " - " + Display);
                        d = _calculator.Subtract(lastVal, displayVal);
                        break;
                    case Operation.Sqrt:
                        _paper.AddArguments("Sqrt( " + LastValue + " )");
                        d = _calculator.SquareRoot(lastVal);
                        break;
                    case Operation.OneX:
                        _paper.AddArguments("1 / " + LastValue);
                        d = _calculator.Reciprocal(lastVal);
                        break;
                    case Operation.Negate:
                        d = _calculator.Negate(lastVal);
                        break;
                }

                // Validate result using engine's validation
                if (!_calculator.IsValidResult(d))
                    throw new Exception("Illegal value");

                // Update paper trail for non-negate operations
                if (lastOper != Operation.Negate)
                {
                    _paper.AddResult(d.ToString(CultureInfo.InvariantCulture));
                    PaperBox.Text = _paper.TrailText;
                }
            }
            catch
            {
                d = 0;
                var parent = (Window)MyPanel.Parent;
                _paper.AddResult("Error");
                PaperBox.Text = _paper.TrailText;
                MessageBox.Show(parent, "Operation cannot be performed", parent.Title);
            }

            return d;
        }

        /// <summary>
        /// Updates the memory display indicator.
        /// </summary>
        private void DisplayMemory()
        {
            if (_memVal != string.Empty)
                BMemBox.Text = "Memory: " + _memVal;
            else
                BMemBox.Text = "Memory: [empty]";
        }

        /// <summary>
        /// Orchestrates a calculation if there is a pending operation.
        /// Updates the display with the result.
        /// </summary>
        private void CalcResults()
        {
            double d;
            if (_lastOper == Operation.None)
                return;

            d = Calc(_lastOper);
            Display = d.ToString(CultureInfo.InvariantCulture);

            UpdateDisplay();
        }

        /// <summary>
        /// Updates the display text box.
        /// Shows "0" if display is empty.
        /// </summary>
        private void UpdateDisplay()
        {
            DisplayBox.Text = Display == string.Empty ? "0" : Display;
        }

        /// <summary>
        /// Adds a character to the display.
        /// Handles digits, decimal point, and backspace.
        /// </summary>
        private void AddToDisplay(char c)
        {
            if (c == '.')
            {
                if (Display.IndexOf('.', 0) >= 0)
                    return;
                Display = Display + c;
            }
            else
            {
                if (c >= '0' && c <= '9')
                {
                    Display = Display + c;
                }
                else if (c == '\b')
                {
                    if (Display.Length <= 1)
                        Display = string.Empty;
                    else
                    {
                        var i = Display.Length;
                        Display = Display.Remove(i - 1, 1);
                    }
                }
            }

            UpdateDisplay();
        }

        /// <summary>
        /// Handles the About menu item click.
        /// </summary>
        private void OnMenuAbout(object sender, RoutedEventArgs e)
        {
            var parent = (Window)MyPanel.Parent;
            MessageBox.Show(parent, parent.Title + " - By Jossef Goldberg ", parent.Title, MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        /// <summary>
        /// Handles the Exit menu item click.
        /// </summary>
        private void OnMenuExit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles the Standard menu item click.
        /// </summary>
        private void OnMenuStandard(object sender, RoutedEventArgs e)
        {
            StandardMenu.IsChecked = true;
        }

        /// <summary>
        /// Handles the Scientific menu item click.
        /// Currently not implemented.
        /// </summary>
        private void OnMenuScientific(object sender, RoutedEventArgs e)
        {
            // Scientific mode not implemented
        }
    }
}
