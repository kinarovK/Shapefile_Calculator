using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapefile_geometry_calculator.Model.Interfaces
{
    public interface ITableWriter
    {
         Task ExportToTableAsync(List<Result_Model> results, string folderName, Enum selectedMeasure);
    }
}
