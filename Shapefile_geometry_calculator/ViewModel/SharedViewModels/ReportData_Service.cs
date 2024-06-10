using Shapefile_geometry_calculator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapefile_geometry_calculator.ViewModel.SharedViewModels
{
    public class ReportData_Service : IReportData_Service
    {
        public ReportResult_Model ReportResult { get; set; }
    }
}
