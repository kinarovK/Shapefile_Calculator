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

            mockShapeGeometryCalculator.Setup(sgc => sgc.CalculatePolygonGeomerty(shapeFiles))
                                       .ReturnsAsync(polygonResults);

            mockResultConverter.Setup(rc => rc.ConvertPolygonResult(polygonResults, It.IsAny<Enum>()))
                               .ReturnsAsync(convertedResults);

            // Act
            var result = await calculationManager.DoCalculation(folder, GeometryType.Polygon, AreaMeasure.Hectar);

            // Assert
            Assert.Equal(convertedResults, result);
            mockFolderReader.Verify(fr => fr.GetAllShpFromFolder(folder), Times.Once);
            mockShapeGeometryCalculator.Verify(sgc => sgc.CalculatePolygonGeomerty(shapeFiles), Times.Once);
            mockResultConverter.Verify(rc => rc.ConvertPolygonResult(polygonResults, AreaMeasure.Hectar), Times.Once);
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

            mockShapeGeometryCalculator.Setup(sgc => sgc.CalculatePolygonGeomerty(shapeFiles))
                                       .ReturnsAsync(polygonResults);

            mockResultConverter.Setup(rc => rc.ConvertPolygonResult(polygonResults, It.IsAny<Enum>()))
                               .ReturnsAsync(convertedResults);

            // Act
            var result = await calculationManager.DoCalculation(folder, GeometryType.Polygon, AreaMeasure.Sqrkilometer);

            // Assert
            Assert.Equal(convertedResults, result);
            mockFolderReader.Verify(fr => fr.GetAllShpFromFolder(folder), Times.Once);
            mockShapeGeometryCalculator.Verify(sgc => sgc.CalculatePolygonGeomerty(shapeFiles), Times.Once);
            mockResultConverter.Verify(rc => rc.ConvertPolygonResult(polygonResults, AreaMeasure.Sqrkilometer), Times.Once);
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

            mockShapeGeometryCalculator.Setup(sgc => sgc.CalculatePolygonGeomerty(shapeFiles))
                                       .ReturnsAsync(polygonResults);

            mockResultConverter.Setup(rc => rc.ConvertPolygonResult(polygonResults, It.IsAny<Enum>()))
                               .ReturnsAsync(convertedResults);

            // Act
            var result = await calculationManager.DoCalculation(folder, GeometryType.Polygon, AreaMeasure.SqrMeter);

            // Assert
            Assert.Equal(convertedResults, result);
            mockFolderReader.Verify(fr => fr.GetAllShpFromFolder(folder), Times.Once);
            mockShapeGeometryCalculator.Verify(sgc => sgc.CalculatePolygonGeomerty(shapeFiles), Times.Once);
            mockResultConverter.Verify(rc => rc.ConvertPolygonResult(polygonResults, AreaMeasure.SqrMeter), Times.Once);
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

            mockShapeGeometryCalculator.Setup(sgc => sgc.CalculatePolylineGeomerty(shapeFiles))
                                       .ReturnsAsync(polylineResults);

            mockResultConverter.Setup(rc => rc.ConvertPolylineResult(polylineResults, It.IsAny<Enum>()))
                               .ReturnsAsync(convertedResults);

            // Act
            var result = await calculationManager.DoCalculation(folder, GeometryType.Polyline, LenghtMeasure.Meter);

            // Assert
            Assert.Equal(convertedResults, result);
            mockFolderReader.Verify(fr => fr.GetAllShpFromFolder(folder), Times.Once);
            mockShapeGeometryCalculator.Verify(sgc => sgc.CalculatePolylineGeomerty(shapeFiles), Times.Once);
            mockResultConverter.Verify(rc => rc.ConvertPolylineResult(polylineResults, LenghtMeasure.Meter), Times.Once);
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

            mockShapeGeometryCalculator.Setup(sgc => sgc.CalculatePolylineGeomerty(shapeFiles))
                                       .ReturnsAsync(polylineResults);

            mockResultConverter.Setup(rc => rc.ConvertPolylineResult(polylineResults, It.IsAny<Enum>()))
                               .ReturnsAsync(convertedResults);

            // Act
            var result = await calculationManager.DoCalculation(folder, GeometryType.Polyline, LenghtMeasure.Kilometer);

            // Assert
            Assert.Equal(convertedResults, result);
            mockFolderReader.Verify(fr => fr.GetAllShpFromFolder(folder), Times.Once);
            mockShapeGeometryCalculator.Verify(sgc => sgc.CalculatePolylineGeomerty(shapeFiles), Times.Once);
            mockResultConverter.Verify(rc => rc.ConvertPolylineResult(polylineResults, LenghtMeasure.Kilometer), Times.Once);
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
                calculationManager.DoCalculation(folder, GeometryType.Polygon, LenghtMeasure.Kilometer));
        }
        [Fact]
        public async Task DoCalculation_ShapeGeometryCalculatorThrowsException_HandlesException()
        {
            // Arrange
            var folder = "someFolder";
            var shapeFiles = new List<string> { "file1.shp" };

            mockFolderReader.Setup(fr => fr.GetAllShpFromFolder(folder))
                            .ReturnsAsync(shapeFiles);

            mockShapeGeometryCalculator.Setup(sgc => sgc.CalculatePolygonGeomerty(shapeFiles))
                                       .ThrowsAsync(new Exception("Calculation error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() =>
                calculationManager.DoCalculation(folder, GeometryType.Polygon, LenghtMeasure.Kilometer));
        }
    }
}
