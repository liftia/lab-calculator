// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CalculatorDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// This class handles UI events and delegates calculation logic to CalculatorEngine.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private readonly ICalculator _calculator;
        private readonly IPaperTrail _paperTrail;

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            _paperTrail = new PaperTrail();
            _calculator = new CalculatorEngine(_paperTrail);

            UpdateDisplay();
            DisplayMemory();
        }

        /// <summary>
        /// Handles keyboard input for the calculator.
        /// </summary>
        private void OnWindowKeyDown(object sender, TextCompositionEventArgs e)
        {
            var s = e.Text;
            var c = (s.ToCharArray())[0];
            e.Handled = true;

            if ((c >= '0' && c <= '9') || c == '.' || c == '\b')
            {
                _calculator.ProcessKey(c);
                UpdateDisplay();
                return;
            }

            switch (c)
            {
                case '+':
                    _calculator.ProcessOperation(Operation.Add);
                    break;
                case '-':
                    _calculator.ProcessOperation(Operation.Subtract);
                    break;
                case '*':
                    _calculator.ProcessOperation(Operation.Multiply);
                    break;
                case '/':
                    _calculator.ProcessOperation(Operation.Divide);
                    break;
                case '%':
                    _calculator.ProcessOperation(Operation.Percent);
                    break;
                case '=':
                    _calculator.ProcessOperation(Operation.None);
                    break;
            }
            UpdateDisplay();
        }

        /// <summary>
        /// Handles digit button clicks.
        /// </summary>
        private void DigitBtn_Click(object sender, RoutedEventArgs e)
        {
            var s = ((Button)sender).Content.ToString();
            var ids = s.ToCharArray();
            _calculator.ProcessKey(ids[0]);
            UpdateDisplay();
        }

        /// <summary>
        /// Handles operation button clicks.
        /// </summary>
        private void OperBtn_Click(object sender, RoutedEventArgs e)
        {
            var buttonName = ((Button)sender).Name;

            switch (buttonName)
            {
                case "BPM":
                    _calculator.ProcessOperation(Operation.Negate);
                    break;
                case "BDevide":
                    _calculator.ProcessOperation(Operation.Divide);
                    break;
                case "BMultiply":
                    _calculator.ProcessOperation(Operation.Multiply);
                    break;
                case "BMinus":
                    _calculator.ProcessOperation(Operation.Subtract);
                    break;
                case "BPlus":
                    _calculator.ProcessOperation(Operation.Add);
                    break;
                case "BEqual":
                    _calculator.ProcessOperation(Operation.None);
                    break;
                case "BSqrt":
                    _calculator.ProcessOperation(Operation.Sqrt);
                    break;
                case "BPercent":
                    _calculator.ProcessOperation(Operation.Percent);
                    break;
                case "BOneOver":
                    _calculator.ProcessOperation(Operation.Reciprocal);
                    break;
                case "BC":
                    _calculator.ClearAll();
                    UpdatePaperTrail();
                    break;
                case "BCE":
                    _calculator.ClearEntry();
                    break;
                case "BMemClear":
                    _calculator.MemoryClear();
                    DisplayMemory();
                    break;
                case "BMemSave":
                    _calculator.MemoryStore();
                    DisplayMemory();
                    break;
                case "BMemRecall":
                    _calculator.MemoryRecall();
                    break;
                case "BMemPlus":
                    _calculator.MemoryAdd();
                    DisplayMemory();
                    break;
            }

            UpdateDisplay();
        }

        /// <summary>
        /// Updates the calculator display with the current value.
        /// </summary>
        private void UpdateDisplay()
        {
            DisplayBox.Text = _calculator.Display;
            UpdatePaperTrail();
        }

        /// <summary>
        /// Updates the paper trail display.
        /// </summary>
        private void UpdatePaperTrail()
        {
            PaperBox.Text = _paperTrail.Text;
        }

        /// <summary>
        /// Updates the memory indicator display.
        /// </summary>
        private void DisplayMemory()
        {
            if (_calculator is CalculatorEngine engine)
            {
                BMemBox.Text = engine.GetMemoryDisplayText();
            }
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
        /// Handles the Standard view menu item click.
        /// </summary>
        private void OnMenuStandard(object sender, RoutedEventArgs e)
        {
            StandardMenu.IsChecked = true;
        }

        /// <summary>
        /// Handles the Scientific view menu item click (placeholder for future implementation).
        /// </summary>
        private void OnMenuScientific(object sender, RoutedEventArgs e)
        {
            // Placeholder for scientific mode
        }
    }
}
