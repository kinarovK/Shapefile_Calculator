using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotSpatial.Data;

namespace Shapefile_geometry_calculator.Model
{
    [ExcludeFromCodeCoverage]
    public class ShapeGeometryCalculator : IShapeGeometryCalculator
    {
        public async Task<List<Result_Model>> CalculatePolylineGeomertyAsync(List<string> shapeFiles)
        {
            var result = new List<Result_Model>();
            var tasks = shapeFiles.Select(async shpPath =>
            {
                double sum = 0;
                var shapeName = Path.GetFileNameWithoutExtension(shpPath);
                string geometryType = string.Empty;
                bool isAnotherGeometry = false;
                int count = 0;
                try
                {
                    await Task.Run(() =>
                    {
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        Encoding encoding = Encoding.GetEncoding("UTF-8");
                        using (IFeatureSet featureSet = FeatureSet.Open(shpPath))
                        {
                            foreach (IFeature feature in featureSet.Features)
                            {
                                if (featureSet.FeatureType == DotSpatial.Data.FeatureType.Line)
                                {
                                    sum += feature.Geometry.Length;
                                    geometryType = "Polyline";
                                    count = count + 1;
                                }
                                else
                                {
                                    isAnotherGeometry = true;
                                    break;
                                }
                            }
                        }
                    });
                    if (!isAnotherGeometry)
                    {
                        result.Add(new Result_Model
                        {
                            ShapeName = shapeName,
                            GeometryType = geometryType,
                            Result = sum,
                            Count = count
                        });
                    }
                }
                //Catch the empty shapefiles
                catch
                {}
            });
            await Task.WhenAll(tasks);
            return result;
        }
           
          
        public async Task<List<Result_Model>> CalculatePolygonGeomertyAsync(List<string> shapeFiles)
        {
            var result = new List<Result_Model>();
            var tasks = shapeFiles.Select(async shpPath =>
            {
                var sum = 0.0;
                var shapeName = Path.GetFileNameWithoutExtension(shpPath);
                var geometryType = string.Empty;
                var count = 0;
                var isAnotherGeometry = false;
                try
                {
                    await Task.Run(() =>
                    {
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        Encoding encoding = Encoding.GetEncoding("UTF-8");

                        using (IFeatureSet featureSet = FeatureSet.Open(shpPath))
                        {
                            foreach (IFeature feature in featureSet.Features)
                            {
                                if (featureSet.FeatureType == DotSpatial.Data.FeatureType.Polygon)
                                {
                                    sum += feature.Geometry.Area;
                                    geometryType = "Polygon";
                                    count++;
                                }
                                else
                                {
                                    isAnotherGeometry = true;
                                    break;
                                }
                            }
                        }
                    });

                    if (!isAnotherGeometry)
                    {
                        result.Add(new Result_Model
                        {
                            ShapeName = shapeName,
                            GeometryType = geometryType,
                            Result = sum,
                            Count = count
                        });
                    }
                }
                catch (Exception ex)
                {}
            });
            await Task.WhenAll(tasks);
            return result;
        }
    }
}
