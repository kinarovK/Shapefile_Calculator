using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapefile_geometry_calculator.Model
{
    public interface IShapeGeometryCalculator
    {
         Task<List<Result_Model>> CalculatePolylineGeomertyAsync(List<string> shapeFiles);
         Task<List<Result_Model>> CalculatePolygonGeomertyAsync(List<string> shapeFiles);
    }
}
