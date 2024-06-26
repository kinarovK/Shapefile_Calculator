﻿using Shapefile_geometry_calculator.Model.Interfaces;
using Shapefile_geometry_calculator.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapefile_geometry_calculator.Model
{
    public class CalculationManager : ICalculationManager
    {
        private readonly IFolderReader folderReader;
        private readonly IShapeGeometryCalculator shapeGeometryCalculator;
        private readonly IResultConverter resultConverter;
        public CalculationManager(IFolderReader folderReader, IShapeGeometryCalculator shapeGeometryCalculator, IResultConverter resultConverter) 
        {
            this.folderReader = folderReader;
            this.shapeGeometryCalculator = shapeGeometryCalculator;
            this.resultConverter = resultConverter;
        }
        public async Task<List<Result_Model>> DoCalculationAsync(string folder, GeometryType geometryType, Enum measureType) 
        {
            var result = new List<Result_Model>();
            var shapeFiles =await folderReader.GetAllShpFromFolder(folder);
            if (geometryType == GeometryType.Polygon)
            {
                result = await shapeGeometryCalculator.CalculatePolygonGeomertyAsync(shapeFiles);
                result =  await resultConverter.ConvertPolygonResultAsync(result, measureType);
            }
            else
            {
                result = await shapeGeometryCalculator.CalculatePolylineGeomertyAsync(shapeFiles);
                result = await resultConverter.ConvertPolylineResultAsync(result, measureType);
            }
            return result;
        }
    }
}
