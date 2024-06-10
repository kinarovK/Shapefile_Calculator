using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapefile_geometry_calculator.Model.Interfaces
{
    public interface IReportCalculator
    {
         Task<ReportResult_Model> ComputeReport(List<Result_Model> input);
    }
}
