using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shapefile_geometry_calculator.ViewModel;
namespace Shapefile_geometry_calculator.Model
{
    public interface ICalculationManager
    {
        Task<List<Result_Model>> DoCalculation(string folder, GeometryType geometryType, Enum measureType);
    }
}
