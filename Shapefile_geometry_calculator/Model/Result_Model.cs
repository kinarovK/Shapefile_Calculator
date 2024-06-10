using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shapefile_geometry_calculator.Model
{
    [ExcludeFromCodeCoverage]
    public class Result_Model
    {
        public string ShapeName { get; set; }
        public string GeometryType { get; set; }
        public double Result { get; set; }
        public int Count { get; set; }
        public override string ToString() => $"{ShapeName}";
    }
}
