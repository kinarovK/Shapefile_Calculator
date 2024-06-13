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
    internal class FolderReader : IFolderReader
    {

        public async Task<List<string>> GetAllShpFromFolder (string folderName)
        {
            List<string> shpFiles = new List<string>();
            shpFiles = await SearchShpFilesAsync(folderName, shpFiles);

            return shpFiles;
        }
        private async Task<List<string>> SearchShpFilesAsync (string folderName, List<string> SHPs)
        {
            try
            {
                var shapeFiles = await Task.Run(() => Directory.EnumerateFiles(folderName, "*.shp"));

                foreach (string filePath in shapeFiles)
                {
                    SHPs.Add(filePath);
                }
    
                return SHPs;
            }
            catch (UnauthorizedAccessException)
            {
                return null;
            }
        }
    }
}
