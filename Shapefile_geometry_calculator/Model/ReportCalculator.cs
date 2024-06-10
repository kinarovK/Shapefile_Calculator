using Shapefile_geometry_calculator.Model.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shapefile_geometry_calculator.Model
{
    public class ReportCalculator : IReportCalculator
    {
        public async Task<ReportResult_Model> ComputeReport(List<Result_Model> input)
        {
            var result = new ReportResult_Model();
            var sortedNumbers = new List<double>();
            result.Min = input.FirstOrDefault().Result;
            foreach (var item in input)
            {
                result.Sum += item.Result;
                if (item.Result > result.Max)
                {
                    result.Max = item.Result;
                }
                if (item.Result < result.Min)
                {
                    result.Min = item.Result;
                }
                result.Count++;
                sortedNumbers.Add(item.Result);
            }

            sortedNumbers.Sort();
            if (result.Count % 2 == 0)
            {
                result.Median = (sortedNumbers[result.Count / 2 - 1] + sortedNumbers[result.Count / 2]) / 2.0;
            }
            else if (result.Count == 1)
            {
                result.Median = sortedNumbers.First();
            }
            else
            {
                result.Median = sortedNumbers[result.Count / 2];
            }
            result.Avg = result.Sum / result.Count;
            return result;
        }
    }
}
