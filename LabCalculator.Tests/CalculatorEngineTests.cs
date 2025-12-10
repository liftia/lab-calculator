// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CalculatorDemo.Engine;
using Xunit;

namespace LabCalculator.Tests
{
    /// <summary>
    /// Unit tests for CalculatorEngine arithmetic operations.
    /// Tests are independent of WPF and UI concerns.
    /// </summary>
    public class CalculatorEngineTests
    {
        private readonly ICalculator _calculator = new CalculatorEngine();

        #region Basic Arithmetic Tests (Required: 5 tests for +, -, *, /, %)

        /// <summary>
        /// TEST 1: Verifies that addition of two positive numbers returns the correct sum.
        /// </summary>
        [Fact]
        public void Add_TwoPositiveNumbers_ReturnsSum()
        {
            // Arrange
            double a = 5;
            double b = 3;

            // Act
            var result = _calculator.Add(a, b);

            // Assert
            Assert.Equal(8, result);
        }

        /// <summary>
        /// TEST 2: Verifies that subtraction returns the correct difference.
        /// </summary>
        [Fact]
        public void Subtract_TwoNumbers_ReturnsDifference()
        {
            // Arrange
            double a = 10;
            double b = 4;

            // Act
            var result = _calculator.Subtract(a, b);

            // Assert
            Assert.Equal(6, result);
        }

        /// <summary>
        /// TEST 3: Verifies that multiplication returns the correct product.
        /// </summary>
        [Fact]
        public void Multiply_TwoNumbers_ReturnsProduct()
        {
            // Arrange
            double a = 6;
            double b = 7;

            // Act
            var result = _calculator.Multiply(a, b);

            // Assert
            Assert.Equal(42, result);
        }

        /// <summary>
        /// TEST 4: Verifies that division by non-zero returns the correct quotient.
        /// </summary>
        [Fact]
        public void Divide_ByNonZero_ReturnsQuotient()
        {
            // Arrange
            double a = 20;
            double b = 4;

            // Act
            var result = _calculator.Divide(a, b);

            // Assert
            Assert.Equal(5, result);
        }

        /// <summary>
        /// TEST 5: Verifies that percent calculation returns the correct value.
        /// Example: 50% of 200 = 100
        /// </summary>
        [Fact]
        public void Percent_ReturnsCorrectValue()
        {
            // Arrange
            double baseValue = 200;
            double percentage = 50;

            // Act
            var result = _calculator.Percent(baseValue, percentage);

            // Assert
            Assert.Equal(100, result);
        }

        #endregion

        #region Additional Tests (Edge Cases and Other Operations)

        /// <summary>
        /// TEST 6: Verifies that division by zero returns positive infinity.
        /// </summary>
        [Fact]
        public void Divide_ByZero_ReturnsInfinity()
        {
            // Arrange
            double a = 10;
            double b = 0;

            // Act
            var result = _calculator.Divide(a, b);

            // Assert
            Assert.True(double.IsPositiveInfinity(result));
        }

        /// <summary>
        /// TEST 7: Verifies that square root of a positive number returns the correct root.
        /// </summary>
        [Fact]
        public void SquareRoot_PositiveNumber_ReturnsRoot()
        {
            // Arrange
            double value = 16;

            // Act
            var result = _calculator.SquareRoot(value);

            // Assert
            Assert.Equal(4, result);
        }

        /// <summary>
        /// TEST 8: Verifies that negation changes the sign of a positive number.
        /// </summary>
        [Fact]
        public void Negate_PositiveNumber_ReturnsNegative()
        {
            // Arrange
            double value = 5;

            // Act
            var result = _calculator.Negate(value);

            // Assert
            Assert.Equal(-5, result);
        }

        /// <summary>
        /// TEST 9: Verifies that reciprocal (1/x) returns the correct value.
        /// </summary>
        [Fact]
        public void Reciprocal_NonZero_ReturnsReciprocal()
        {
            // Arrange
            double value = 4;

            // Act
            var result = _calculator.Reciprocal(value);

            // Assert
            Assert.Equal(0.25, result);
        }

        /// <summary>
        /// TEST 10: Verifies that IsValidResult correctly identifies valid and invalid numbers.
        /// Tests NaN, Infinity (positive and negative), and valid numbers.
        /// </summary>
        [Theory]
        [InlineData(double.NaN, false)]
        [InlineData(double.PositiveInfinity, false)]
        [InlineData(double.NegativeInfinity, false)]
        [InlineData(42.0, true)]
        [InlineData(0.0, true)]
        [InlineData(-15.5, true)]
        public void IsValidResult_ReturnsExpected(double value, bool expected)
        {
            // Act
            var result = _calculator.IsValidResult(value);

            // Assert
            Assert.Equal(expected, result);
        }

        #endregion

        #region Additional Edge Case Tests

        /// <summary>
        /// Verifies that adding negative numbers works correctly.
        /// </summary>
        [Fact]
        public void Add_NegativeNumbers_ReturnsCorrectSum()
        {
            // Arrange
            double a = -5;
            double b = -3;

            // Act
            var result = _calculator.Add(a, b);

            // Assert
            Assert.Equal(-8, result);
        }

        /// <summary>
        /// Verifies that negating a negative number returns positive.
        /// </summary>
        [Fact]
        public void Negate_NegativeNumber_ReturnsPositive()
        {
            // Arrange
            double value = -5;

            // Act
            var result = _calculator.Negate(value);

            // Assert
            Assert.Equal(5, result);
        }

        /// <summary>
        /// Verifies that square root of negative number returns NaN.
        /// </summary>
        [Fact]
        public void SquareRoot_NegativeNumber_ReturnsNaN()
        {
            // Arrange
            double value = -16;

            // Act
            var result = _calculator.SquareRoot(value);

            // Assert
            Assert.True(double.IsNaN(result));
        }

        #endregion
    }
}
