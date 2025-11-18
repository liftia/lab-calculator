// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Xunit;

namespace CalculatorDemo.Tests
{
    /// <summary>
    /// Unit tests for the CalculatorEngine class.
    /// Tests cover basic arithmetic operations and calculator functionality.
    /// </summary>
    public class CalculatorEngineTests
    {
        /// <summary>
        /// Creates a new CalculatorEngine instance for testing.
        /// </summary>
        private static CalculatorEngine CreateCalculator()
        {
            var paperTrail = new PaperTrail();
            return new CalculatorEngine(paperTrail);
        }

        #region Basic Operations Tests

        /// <summary>
        /// Tests that addition operation returns correct result.
        /// </summary>
        [Fact]
        public void Calculate_Addition_ReturnsCorrectResult()
        {
            // Arrange
            var calculator = CreateCalculator();

            // Act
            double result = calculator.Calculate(Operation.Add, 5, 3);

            // Assert
            Assert.Equal(8, result);
        }

        /// <summary>
        /// Tests that subtraction operation returns correct result.
        /// </summary>
        [Fact]
        public void Calculate_Subtraction_ReturnsCorrectResult()
        {
            // Arrange
            var calculator = CreateCalculator();

            // Act
            double result = calculator.Calculate(Operation.Subtract, 10, 4);

            // Assert
            Assert.Equal(6, result);
        }

        /// <summary>
        /// Tests that multiplication operation returns correct result.
        /// </summary>
        [Fact]
        public void Calculate_Multiplication_ReturnsCorrectResult()
        {
            // Arrange
            var calculator = CreateCalculator();

            // Act
            double result = calculator.Calculate(Operation.Multiply, 7, 6);

            // Assert
            Assert.Equal(42, result);
        }

        /// <summary>
        /// Tests that division operation returns correct result.
        /// </summary>
        [Fact]
        public void Calculate_Division_ReturnsCorrectResult()
        {
            // Arrange
            var calculator = CreateCalculator();

            // Act
            double result = calculator.Calculate(Operation.Divide, 20, 4);

            // Assert
            Assert.Equal(5, result);
        }

        /// <summary>
        /// Tests that percentage operation returns correct result.
        /// Percentage is calculated as (operand1 * operand2) / 100.
        /// </summary>
        [Fact]
        public void Calculate_Percentage_ReturnsCorrectResult()
        {
            // Arrange
            var calculator = CreateCalculator();

            // Act
            double result = calculator.Calculate(Operation.Percent, 200, 15);

            // Assert
            Assert.Equal(30, result); // 200 * 15 / 100 = 30
        }

        #endregion

        #region Advanced Operations Tests

        /// <summary>
        /// Tests that square root operation returns correct result.
        /// </summary>
        [Fact]
        public void Calculate_SquareRoot_ReturnsCorrectResult()
        {
            // Arrange
            var calculator = CreateCalculator();

            // Act
            double result = calculator.Calculate(Operation.Sqrt, 16, 0);

            // Assert
            Assert.Equal(4, result);
        }

        /// <summary>
        /// Tests that reciprocal operation (1/x) returns correct result.
        /// </summary>
        [Fact]
        public void Calculate_Reciprocal_ReturnsCorrectResult()
        {
            // Arrange
            var calculator = CreateCalculator();

            // Act
            double result = calculator.Calculate(Operation.Reciprocal, 4, 0);

            // Assert
            Assert.Equal(0.25, result);
        }

        /// <summary>
        /// Tests that negate operation returns correct result.
        /// </summary>
        [Fact]
        public void Calculate_Negate_ReturnsCorrectResult()
        {
            // Arrange
            var calculator = CreateCalculator();

            // Act
            double result = calculator.Calculate(Operation.Negate, 5, 0);

            // Assert
            Assert.Equal(-5, result);
        }

        #endregion

        #region Edge Cases Tests

        /// <summary>
        /// Tests that division by zero throws an InvalidOperationException.
        /// </summary>
        [Fact]
        public void Calculate_DivisionByZero_ThrowsException()
        {
            // Arrange
            var calculator = CreateCalculator();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                calculator.Calculate(Operation.Divide, 10, 0));
        }

        /// <summary>
        /// Tests that adding negative numbers works correctly.
        /// </summary>
        [Fact]
        public void Calculate_AdditionWithNegativeNumbers_ReturnsCorrectResult()
        {
            // Arrange
            var calculator = CreateCalculator();

            // Act
            double result = calculator.Calculate(Operation.Add, -5, 3);

            // Assert
            Assert.Equal(-2, result);
        }

        /// <summary>
        /// Tests that multiplication with decimal numbers works correctly.
        /// </summary>
        [Fact]
        public void Calculate_MultiplicationWithDecimals_ReturnsCorrectResult()
        {
            // Arrange
            var calculator = CreateCalculator();

            // Act
            double result = calculator.Calculate(Operation.Multiply, 2.5, 4);

            // Assert
            Assert.Equal(10, result);
        }

        #endregion

        #region Display and Input Tests

        /// <summary>
        /// Tests that ProcessKey correctly updates the display with digits.
        /// </summary>
        [Fact]
        public void ProcessKey_Digits_UpdatesDisplay()
        {
            // Arrange
            var calculator = CreateCalculator();

            // Act
            calculator.ProcessKey('1');
            calculator.ProcessKey('2');
            calculator.ProcessKey('3');

            // Assert
            Assert.Equal("123", calculator.Display);
        }

        /// <summary>
        /// Tests that ProcessKey correctly handles decimal point.
        /// </summary>
        [Fact]
        public void ProcessKey_DecimalPoint_UpdatesDisplayCorrectly()
        {
            // Arrange
            var calculator = CreateCalculator();

            // Act
            calculator.ProcessKey('3');
            calculator.ProcessKey('.');
            calculator.ProcessKey('1');
            calculator.ProcessKey('4');

            // Assert
            Assert.Equal("3.14", calculator.Display);
        }

        /// <summary>
        /// Tests that multiple decimal points are ignored.
        /// </summary>
        [Fact]
        public void ProcessKey_MultipleDecimalPoints_IgnoresSecond()
        {
            // Arrange
            var calculator = CreateCalculator();

            // Act
            calculator.ProcessKey('3');
            calculator.ProcessKey('.');
            calculator.ProcessKey('1');
            calculator.ProcessKey('.');
            calculator.ProcessKey('4');

            // Assert
            Assert.Equal("3.14", calculator.Display);
        }

        #endregion

        #region Memory Operations Tests

        /// <summary>
        /// Tests that memory store and recall work correctly.
        /// </summary>
        [Fact]
        public void Memory_StoreAndRecall_ReturnsStoredValue()
        {
            // Arrange
            var calculator = CreateCalculator();
            calculator.ProcessKey('4');
            calculator.ProcessKey('2');

            // Act
            calculator.MemoryStore();
            calculator.ClearAll();
            calculator.MemoryRecall();

            // Assert
            Assert.Equal("42", calculator.Display);
        }

        /// <summary>
        /// Tests that memory add correctly adds to existing memory value.
        /// </summary>
        [Fact]
        public void Memory_Add_AddsToExistingValue()
        {
            // Arrange
            var calculator = CreateCalculator();
            calculator.ProcessKey('1');
            calculator.ProcessKey('0');
            calculator.MemoryStore();

            calculator.ClearAll();
            calculator.ProcessKey('5');
            calculator.MemoryAdd();

            // Act
            calculator.MemoryRecall();

            // Assert
            Assert.Equal("15", calculator.Display);
        }

        /// <summary>
        /// Tests that memory clear resets memory to zero.
        /// </summary>
        [Fact]
        public void Memory_Clear_ResetsToZero()
        {
            // Arrange
            var calculator = CreateCalculator();
            calculator.ProcessKey('4');
            calculator.ProcessKey('2');
            calculator.MemoryStore();

            // Act
            calculator.MemoryClear();

            // Assert
            Assert.Equal(0, calculator.Memory);
        }

        #endregion

        #region Clear Operations Tests

        /// <summary>
        /// Tests that ClearAll resets the calculator to initial state.
        /// </summary>
        [Fact]
        public void ClearAll_ResetsCalculator()
        {
            // Arrange
            var calculator = CreateCalculator();
            calculator.ProcessKey('5');
            calculator.ProcessKey('5');

            // Act
            calculator.ClearAll();

            // Assert
            Assert.Equal("0", calculator.Display);
        }

        #endregion

        #region Paper Trail Tests

        /// <summary>
        /// Tests that paper trail records calculation history.
        /// </summary>
        [Fact]
        public void PaperTrail_RecordsCalculationHistory()
        {
            // Arrange
            var paperTrail = new PaperTrail();
            var calculator = new CalculatorEngine(paperTrail);

            // Act - Perform 5 + 3 = 8
            calculator.ProcessKey('5');
            calculator.ProcessOperation(Operation.Add);
            calculator.ProcessKey('3');
            calculator.ProcessOperation(Operation.None); // Equals

            // Assert
            Assert.Contains("5 + 3 = 8", paperTrail.Text);
        }

        #endregion
    }
}
