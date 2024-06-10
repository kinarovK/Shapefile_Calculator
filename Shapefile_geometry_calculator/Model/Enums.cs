using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapefile_geometry_calculator.Model
{
    public enum LenghtMeasure
    {
        Kilometer,
        Meter
    }
    public enum AreaMeasure
    {
        Sqrkilometer,
        SqrMeter,
        Hectar
    }
    public enum GeometryType
    {
        Polygon,
        Polyline
    }
}
