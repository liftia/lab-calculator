// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using CalculatorDemo.Engine;
using Xunit;

namespace LabCalculator.Tests
{
    /// <summary>
    /// Unit tests for PaperTrail history formatting.
    /// Tests verify that calculation history is correctly formatted and accumulated.
    /// </summary>
    public class PaperTrailTests
    {
        /// <summary>
        /// TEST 1: Verifies that adding arguments and result formats correctly.
        /// Expected format: "arguments = result\n"
        /// </summary>
        [Fact]
        public void AddResult_FormatsCorrectly()
        {
            // Arrange
            var paper = new PaperTrail();

            // Act
            paper.AddArguments("5 + 3");
            paper.AddResult("8");

            // Assert
            Assert.Equal("5 + 3 = 8\n", paper.TrailText);
        }

        /// <summary>
        /// TEST 2: Verifies that Clear resets the trail text to empty.
        /// </summary>
        [Fact]
        public void Clear_ResetsTrailText()
        {
            // Arrange
            var paper = new PaperTrail();
            paper.AddArguments("5 + 3");
            paper.AddResult("8");

            // Act
            paper.Clear();

            // Assert
            Assert.Equal(string.Empty, paper.TrailText);
        }

        /// <summary>
        /// TEST 3: Verifies that multiple operations accumulate correctly.
        /// Each operation should appear on its own line in the trail.
        /// </summary>
        [Fact]
        public void MultipleOperations_AccumulatesCorrectly()
        {
            // Arrange
            var paper = new PaperTrail();

            // Act
            paper.AddArguments("5 + 3");
            paper.AddResult("8");

            paper.AddArguments("8 * 2");
            paper.AddResult("16");

            // Assert
            Assert.Equal("5 + 3 = 8\n8 * 2 = 16\n", paper.TrailText);
        }

        /// <summary>
        /// Verifies that TrailText is empty initially.
        /// </summary>
        [Fact]
        public void TrailText_InitiallyEmpty()
        {
            // Arrange
            var paper = new PaperTrail();

            // Assert
            Assert.Equal(string.Empty, paper.TrailText);
        }

        /// <summary>
        /// Verifies that calling AddResult without AddArguments still works.
        /// </summary>
        [Fact]
        public void AddResult_WithoutArguments_FormatsWithEmptyArguments()
        {
            // Arrange
            var paper = new PaperTrail();

            // Act
            paper.AddResult("Error");

            // Assert
            Assert.Equal(" = Error\n", paper.TrailText);
        }
    }
}
