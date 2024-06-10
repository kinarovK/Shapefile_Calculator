using Shapefile_geometry_calculator.Model;
using Shapefile_geometry_calculator.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapefile_geometry_calculator_tests
{
    public class ResultConverterTests
    {
        private readonly ResultConverter resultConverter;

        public ResultConverterTests()
        {
            resultConverter = new ResultConverter();
        }
        [Fact]
        public async Task ConvertPolygonResult_EmptyList_ReturnsNull()
        {
            // Arrange
            var input = new List<Result_Model>();

            // Act
            var result = await resultConverter.ConvertPolygonResult(input, AreaMeasure.Sqrkilometer);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ConvertPolygonResult_SingleItem_ReturnsCorrectConversion()
        {
            // Arrange
            var input = new List<Result_Model>
        {
            new Result_Model { Result = 1000000 }
        };

            // Act
            var result = await resultConverter.ConvertPolygonResult(input, AreaMeasure.Sqrkilometer);

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Result);
        }

        [Fact]
        public async Task ConvertPolygonResult_MultipleItems_ReturnsCorrectConversions()
        {
            // Arrange
            var input = new List<Result_Model>
        {
            new Result_Model { Result = 1000000 },
            new Result_Model { Result = 2000000 }
        };

            // Act
            var result = await resultConverter.ConvertPolygonResult(input, AreaMeasure.Sqrkilometer);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].Result);
            Assert.Equal(2, result[1].Result);
        }

        [Fact]
        public async Task ConvertPolygonResult_InvalidMeasureType_DoesNotConvert()
        {
            // Arrange
            var input = new List<Result_Model>
        {
            new Result_Model { Result = 1000000 }
        };

            // Act
            var result = await resultConverter.ConvertPolygonResult(input, (AreaMeasure)999);

            // Assert
            Assert.Single(result);
            Assert.Equal(1000000, result[0].Result);
        }
        [Fact]
        public async Task ConvertPolylineResult_MeterConversion_ReturnsCorrectConversion()
        {
            // Arrange
            var input = new List<Result_Model>
        {
            new Result_Model { Result = 1000 }
        };

            // Act
            var result = await resultConverter.ConvertPolylineResult(input, LenghtMeasure.Meter);

            // Assert
            Assert.Single(result);
            Assert.Equal(1000, result[0].Result);
        }
        [Fact]
        public async Task ConvertPolylineResult_KilometerConversion_ReturnsCorrectConversion()
        {
            // Arrange
            var input = new List<Result_Model>
        {
            new Result_Model { Result = 1000 }
        };

            // Act
            var result = await resultConverter.ConvertPolylineResult(input, LenghtMeasure.Kilometer);

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Result);
        }

        [Fact]
        public async Task ConvertPolylineResult_EmptyList_ReturnsNull()
        {
            // Arrange
            var input = new List<Result_Model>();

            // Act
            var result = await resultConverter.ConvertPolylineResult(input, LenghtMeasure.Meter);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ConvertPolylineResult_MultipleItems_ReturnsCorrectConversions()
        {
            // Arrange
            var input = new List<Result_Model>
        {
            new Result_Model { Result = 1000 },
            new Result_Model { Result = 2000 }
        };

            // Act
            var result = await resultConverter.ConvertPolylineResult(input, LenghtMeasure.Kilometer);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].Result);
            Assert.Equal(2, result[1].Result);
        }

        [Fact]
        public async Task CalculateConvertion_CorrectlyConvertsResults()
        {
            // Arrange
            var input = new List<Result_Model>
        {
            new Result_Model { Result = 1000 },
            new Result_Model { Result = 500 }
        };

            // Act
            var result = await resultConverter.ConvertPolylineResult(input, LenghtMeasure.Kilometer);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].Result);
            Assert.Equal(0.5, result[1].Result);
        }
    }
}
