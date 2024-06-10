using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapefile_geometry_calculator.Model
{
    [ExcludeFromCodeCoverage]
    public struct ReportResult_Model
    {
       
        public string FolderName { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public double Avg { get; set; }
        public double Sum { get; set; }
        public double Median { get; set; }
        public int Count { get; set; }
    }
}
