using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapefile_geometry_calculator.Model.Interfaces
{
    public interface IResultConverter
    {
        Task<List<Result_Model>> ConvertPolygonResult(List<Result_Model> results, Enum measureType);
        Task<List<Result_Model>> ConvertPolylineResult(List<Result_Model> results, Enum measureType);
    }
}
