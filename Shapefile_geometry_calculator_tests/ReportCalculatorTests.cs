using Shapefile_geometry_calculator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapefile_geometry_calculator_tests
{
    public class ReportCalculatorTests
    {
        private readonly ReportCalculator reportCalculator;

        public ReportCalculatorTests()
        {
            reportCalculator = new ReportCalculator();
        }

        [Fact]
        public async Task ComputeReport_SingleItem_ReturnsCorrectStatistics()
        {
            // Arrange
            var input = new List<Result_Model>
        {
            new Result_Model { Result = 10 }
        };

            // Act
            var result = await reportCalculator.ComputeReport(input);

            // Assert
            Assert.Equal(1, result.Count);
            Assert.Equal(10, result.Sum);
            Assert.Equal(10, result.Avg);
            Assert.Equal(10, result.Min);
            Assert.Equal(10, result.Max);
            Assert.Equal(10, result.Median);
        }

        [Fact]
        public async Task ComputeReport_MultipleItems_ReturnsCorrectStatistics()
        {
            // Arrange
            var input = new List<Result_Model>
        {
            new Result_Model { Result = 10 },
            new Result_Model { Result = 20 },
            new Result_Model { Result = 30 }
        };

            // Act
            var result = await reportCalculator.ComputeReport(input);

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal(60, result.Sum);
            Assert.Equal(20, result.Avg);
            Assert.Equal(10, result.Min);
            Assert.Equal(30, result.Max);
            Assert.Equal(20, result.Median);
        }

        [Fact]
        public async Task ComputeReport_MixedValues_ReturnsCorrectStatistics()
        {
            // Arrange
            var input = new List<Result_Model>
        {
            new Result_Model { Result = -10 },
            new Result_Model { Result = 0 },
            new Result_Model { Result = 10 },
            new Result_Model { Result = -5 },
            new Result_Model { Result = 5 }
        };

            // Act
            var result = await reportCalculator.ComputeReport(input);

            // Assert
            Assert.Equal(5, result.Count);
            Assert.Equal(0, result.Sum);
            Assert.Equal(0, result.Avg);
            Assert.Equal(-10, result.Min);
            Assert.Equal(10, result.Max);
            Assert.Equal(0, result.Median);
        }

        [Fact]
        public async Task ComputeReport_AllEqualValues_ReturnsCorrectStatistics()
        {
            // Arrange
            var input = new List<Result_Model>
        {
            new Result_Model { Result = 10 },
            new Result_Model { Result = 10 },
            new Result_Model { Result = 10 }
        };

            // Act
            var result = await reportCalculator.ComputeReport(input);

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal(30, result.Sum);
            Assert.Equal(10, result.Avg);
            Assert.Equal(10, result.Min);
            Assert.Equal(10, result.Max);
            Assert.Equal(10, result.Median);
        }
    }
}
