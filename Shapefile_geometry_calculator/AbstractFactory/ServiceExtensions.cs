using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapefile_geometry_calculator.AbstractFactory
{
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        public static void AddFormFactory<TForm>(this IServiceCollection services) where TForm : class
        {
            services.AddTransient<TForm>();
            services.AddSingleton<Func<TForm>>(x=> () => x.GetService<TForm>());
            services.AddSingleton<IAbstractFactory<TForm>, AbstractFactory<TForm>>();
        }
    }
}
