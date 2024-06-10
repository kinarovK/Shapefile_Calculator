using Shapefile_geometry_calculator.Model.Interfaces;
using Shapefile_geometry_calculator.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapefile_geometry_calculator.Model
{
    public class ResultConverter : IResultConverter
    {
        public async Task<List<Result_Model>> ConvertPolygonResult(List<Result_Model> results, Enum measureType)

        {
            if (results.Count == 0) return null;
            if (measureType.CompareTo(AreaMeasure.Sqrkilometer) == 0)
            {
                await CalculateConvertion(results, 1000000);
            }
            else if (measureType.CompareTo(AreaMeasure.SqrMeter) == 0)
            {
                
                await CalculateConvertion(results, 1);
            }
            else if (measureType.CompareTo(AreaMeasure.Hectar)== 0)
            {
                await CalculateConvertion(results, 10000);
            }
            return results;
        }
        public async Task<List<Result_Model>> ConvertPolylineResult(List<Result_Model> results, Enum measureType)
        {
            if (results.Count == 0) 
                return null;

            if (measureType.CompareTo(LenghtMeasure.Meter) == 0)
            {
                await CalculateConvertion(results, 1);
            }
            else if (measureType.CompareTo(LenghtMeasure.Kilometer) == 0)
            {
                await CalculateConvertion(results, 1000);
            }
            return results;
        }

        private async Task<List<Result_Model>> CalculateConvertion(List<Result_Model> results, int num)
        {
            return await Task.Run(() =>
            {
                foreach (var item in results)
                {
                    item.Result = Math.Round(item.Result / num, 2);
                }
                return results;
            });
        }
    }
}
