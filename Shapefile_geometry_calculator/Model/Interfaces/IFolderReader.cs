using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapefile_geometry_calculator.Model
{
    public interface IFolderReader
    {
        Task<List<string>> GetAllShpFromFolder(string folderName);
    }
}
