using Shapefile_geometry_calculator.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapefile_geometry_calculator.Model
{
    [ExcludeFromCodeCoverage]
    internal class TableWriter : ITableWriter
    {
        private string _filePath;
        public async Task ExportToTableAsync(List<Result_Model> results, string folderName, Enum selectedMeasure)
        {
            var newFileName = Path.GetDirectoryName(folderName);
            var fileName =DateTime.Now.ToString("yyyy-MM-dd_H-mm-s");
            _filePath = Path.Combine(folderName + "\\" + fileName + ".csv");
            using (var sw =new StreamWriter(_filePath))
            { 
                await sw.WriteLineAsync($"Selected measurment: {selectedMeasure}");
                await sw.WriteLineAsync("Shape name;Geometry type;Result;Count");
                foreach (var item in results)
                {
                    await sw.WriteLineAsync($"{item.ShapeName};{item.GeometryType};{item.Result};{item.Count}");
                }
            }
        }
    }
}
