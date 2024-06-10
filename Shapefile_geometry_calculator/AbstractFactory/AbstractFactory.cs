using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapefile_geometry_calculator.AbstractFactory
{
    [ExcludeFromCodeCoverage]
    public class AbstractFactory<T> : IAbstractFactory<T>
    {
        private readonly Func<T> _factory;
        public AbstractFactory(Func<T> factory)
        {
            this._factory = factory;
        }

        public T Create()
        {
            return _factory();
        }
    }
}
