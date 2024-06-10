using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapefile_geometry_calculator.AbstractFactory
{

    public interface IAbstractFactory <T>
    {
        T Create();
    }
}
