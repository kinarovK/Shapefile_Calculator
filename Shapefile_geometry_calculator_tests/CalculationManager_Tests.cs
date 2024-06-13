using Moq;
using Shapefile_geometry_calculator.Model;
using Shapefile_geometry_calculator.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Shapefile_geometry_calculator_tests
{
    public class CalculationManager_Tests
    {
        private readonly Mock<IFolderReader> mockFolderReader;
        private readonly Mock<IShapeGeometryCalculator> mockShapeGeometryCalculator;
        private readonly Mock<IResultConverter> mockResultConverter;
        private readonly CalculationManager calculationManager;


        public CalculationManager_Tests()
        {
            mockFolderReader = new Mock<IFolderReader>();
            mockShapeGeometryCalculator = new Mock<IShapeGeometryCalculator>();
            mockResultConverter = new Mock<IResultConverter>();

            calculationManager = new CalculationManager(
                mockFolderReader.Object,
                mockShapeGeometryCalculator.Object,
                mockResultConverter.Object
            );
        }
        [Fact]
        public async Task DoCalculation_PolygonGeometryType_CallsCorrectMethods_WithHectars()
        {
            // Arrange
            var folder = "someFolder";
            var shapeFiles = new List<string> { "file1.shp", "file2.shp" };
            var polygonResults = new List<Result_Model> { new Result_Model() };
            var convertedResults = new List<Result_Model> { new Result_Model() };

            mockFolderReader.Setup(fr => fr.GetAllShpFromFolder(folder))
                            .ReturnsAsync(shapeFiles);

            mockShapeGeometryCalculator.Setup(sgc => sgc.CalculatePolygonGeomertyAsync(shapeFiles))
                                       .ReturnsAsync(polygonResults);

            mockResultConverter.Setup(rc => rc.ConvertPolygonResultAsync(polygonResults, It.IsAny<Enum>()))
                               .ReturnsAsync(convertedResults);

            // Act
            var result = await calculationManager.DoCalculationAsync(folder, GeometryType.Polygon, AreaMeasure.Hectar);

            // Assert
            Assert.Equal(convertedResults, result);
            mockFolderReader.Verify(fr => fr.GetAllShpFromFolder(folder), Times.Once);
            mockShapeGeometryCalculator.Verify(sgc => sgc.CalculatePolygonGeomertyAsync(shapeFiles), Times.Once);
            mockResultConverter.Verify(rc => rc.ConvertPolygonResultAsync(polygonResults, AreaMeasure.Hectar), Times.Once);
        }
        [Fact]
        public async Task DoCalculation_PolygonGeometryType_CallsCorrectMethods_WithKilometers()
        {
            // Arrange
            var folder = "someFolder";
            var shapeFiles = new List<string> { "file1.shp", "file2.shp" };
            var polygonResults = new List<Result_Model> { new Result_Model() };
            var convertedResults = new List<Result_Model> { new Result_Model() };

            mockFolderReader.Setup(fr => fr.GetAllShpFromFolder(folder))
                            .ReturnsAsync(shapeFiles);

            mockShapeGeometryCalculator.Setup(sgc => sgc.CalculatePolygonGeomertyAsync(shapeFiles))
                                       .ReturnsAsync(polygonResults);

            mockResultConverter.Setup(rc => rc.ConvertPolygonResultAsync(polygonResults, It.IsAny<Enum>()))
                               .ReturnsAsync(convertedResults);

            // Act
            var result = await calculationManager.DoCalculationAsync(folder, GeometryType.Polygon, AreaMeasure.Sqrkilometer);

            // Assert
            Assert.Equal(convertedResults, result);
            mockFolderReader.Verify(fr => fr.GetAllShpFromFolder(folder), Times.Once);
            mockShapeGeometryCalculator.Verify(sgc => sgc.CalculatePolygonGeomertyAsync(shapeFiles), Times.Once);
            mockResultConverter.Verify(rc => rc.ConvertPolygonResultAsync(polygonResults, AreaMeasure.Sqrkilometer), Times.Once);
        }
        [Fact]
        public async Task DoCalculation_PolygonGeometryType_CallsCorrectMethods_WithMeters()
        {
            // Arrange
            var folder = "someFolder";
            var shapeFiles = new List<string> { "file1.shp", "file2.shp" };
            var polygonResults = new List<Result_Model> { new Result_Model() };
            var convertedResults = new List<Result_Model> { new Result_Model() };

            mockFolderReader.Setup(fr => fr.GetAllShpFromFolder(folder))
                            .ReturnsAsync(shapeFiles);

            mockShapeGeometryCalculator.Setup(sgc => sgc.CalculatePolygonGeomertyAsync(shapeFiles))
                                       .ReturnsAsync(polygonResults);

            mockResultConverter.Setup(rc => rc.ConvertPolygonResultAsync(polygonResults, It.IsAny<Enum>()))
                               .ReturnsAsync(convertedResults);

            // Act
            var result = await calculationManager.DoCalculationAsync(folder, GeometryType.Polygon, AreaMeasure.SqrMeter);

            // Assert
            Assert.Equal(convertedResults, result);
            mockFolderReader.Verify(fr => fr.GetAllShpFromFolder(folder), Times.Once);
            mockShapeGeometryCalculator.Verify(sgc => sgc.CalculatePolygonGeomertyAsync(shapeFiles), Times.Once);
            mockResultConverter.Verify(rc => rc.ConvertPolygonResultAsync(polygonResults, AreaMeasure.SqrMeter), Times.Once);
        }

        [Fact]
        public async Task DoCalculation_PolylineGeometryType_CallsCorrectMethods_WithMeters()
        {
            // Arrange
            var folder = "someFolder";
            var shapeFiles = new List<string> { "file1.shp", "file2.shp" };
            var polylineResults = new List<Result_Model> { new Result_Model() };
            var convertedResults = new List<Result_Model> { new Result_Model() };

            mockFolderReader.Setup(fr => fr.GetAllShpFromFolder(folder))
                            .ReturnsAsync(shapeFiles);

            mockShapeGeometryCalculator.Setup(sgc => sgc.CalculatePolylineGeomertyAsync(shapeFiles))
                                       .ReturnsAsync(polylineResults);

            mockResultConverter.Setup(rc => rc.ConvertPolylineResultAsync(polylineResults, It.IsAny<Enum>()))
                               .ReturnsAsync(convertedResults);

            // Act
            var result = await calculationManager.DoCalculationAsync(folder, GeometryType.Polyline, LenghtMeasure.Meter);

            // Assert
            Assert.Equal(convertedResults, result);
            mockFolderReader.Verify(fr => fr.GetAllShpFromFolder(folder), Times.Once);
            mockShapeGeometryCalculator.Verify(sgc => sgc.CalculatePolylineGeomertyAsync(shapeFiles), Times.Once);
            mockResultConverter.Verify(rc => rc.ConvertPolylineResultAsync(polylineResults, LenghtMeasure.Meter), Times.Once);
        }
        [Fact]
        public async Task DoCalculation_PolylineGeometryType_CallsCorrectMethods_WithKilometers()
        {
            // Arrange
            var folder = "someFolder";
            var shapeFiles = new List<string> { "file1.shp", "file2.shp" };
            var polylineResults = new List<Result_Model> { new Result_Model() };
            var convertedResults = new List<Result_Model> { new Result_Model() };

            mockFolderReader.Setup(fr => fr.GetAllShpFromFolder(folder))
                            .ReturnsAsync(shapeFiles);

            mockShapeGeometryCalculator.Setup(sgc => sgc.CalculatePolylineGeomertyAsync(shapeFiles))
                                       .ReturnsAsync(polylineResults);

            mockResultConverter.Setup(rc => rc.ConvertPolylineResultAsync(polylineResults, It.IsAny<Enum>()))
                               .ReturnsAsync(convertedResults);

            // Act
            var result = await calculationManager.DoCalculationAsync(folder, GeometryType.Polyline, LenghtMeasure.Kilometer);

            // Assert
            Assert.Equal(convertedResults, result);
            mockFolderReader.Verify(fr => fr.GetAllShpFromFolder(folder), Times.Once);
            mockShapeGeometryCalculator.Verify(sgc => sgc.CalculatePolylineGeomertyAsync(shapeFiles), Times.Once);
            mockResultConverter.Verify(rc => rc.ConvertPolylineResultAsync(polylineResults, LenghtMeasure.Kilometer), Times.Once);
        }

        [Fact]
        public async Task DoCalculation_FolderReaderThrowsException_HandlesException()
        {
            // Arrange
            var folder = "someFolder";

            mockFolderReader.Setup(fr => fr.GetAllShpFromFolder(folder))
                            .ThrowsAsync(new UnauthorizedAccessException());

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
                calculationManager.DoCalculationAsync(folder, GeometryType.Polygon, LenghtMeasure.Kilometer));
        }
        [Fact]
        public async Task DoCalculation_ShapeGeometryCalculatorThrowsException_HandlesException()
        {
            // Arrange
            var folder = "someFolder";
            var shapeFiles = new List<string> { "file1.shp" };

            mockFolderReader.Setup(fr => fr.GetAllShpFromFolder(folder))
                            .ReturnsAsync(shapeFiles);

            mockShapeGeometryCalculator.Setup(sgc => sgc.CalculatePolygonGeomertyAsync(shapeFiles))
                                       .ThrowsAsync(new Exception("Calculation error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() =>
                calculationManager.DoCalculationAsync(folder, GeometryType.Polygon, LenghtMeasure.Kilometer));
        }
    }
}
