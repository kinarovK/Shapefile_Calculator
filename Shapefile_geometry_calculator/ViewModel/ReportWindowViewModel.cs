using Shapefile_geometry_calculator.Model;
using Shapefile_geometry_calculator.Model.Interfaces;
using Shapefile_geometry_calculator.ViewModel.SharedViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Shapefile_geometry_calculator.ViewModel
{
    [ExcludeFromCodeCoverage]
    public class ReportWindowViewModel : ViewModelBase
    {
        //private readonly Window window;
        private readonly IReportData_Service reportData_Service;
        //private string selectedPath;
        private ReportResult_Model report;
        public ReportWindowViewModel(Window window, IReportData_Service reportData_Service)
        {
            //this.window = window;
            this.reportData_Service = reportData_Service;
            report = reportData_Service.ReportResult;
        }

        public string FolderName
        {
            get => report.FolderName;
            set
            {
                if (report.FolderName != value)
                {
                    report.FolderName = value;
                }
            }
        }
        public double Min
        {
            get => Math.Round(report.Min, 2);
            set
            {
                if (report.Min != value)
                {
                    report.Min = value;
                }
            }
        }
        public double Max
        {
            get => Math.Round(report.Max, 2);
            set
            {
                if (report.Max != value)
                {
                    report.Max = value;
                }
            }
        }
        public double Avg
        {
            get => Math.Round(report.Avg, 2);
            set
            {
                if (report.Avg != value)
                {
                    report.Avg = value;
                }
            }
        }
        public double Sum
        {
            get => Math.Round(report.Sum, 2);
            set
            {
                if (report.Sum != value)
                {
                    report.Sum = value;
                }
            }
        }
        public double Median
        {
            get => Math.Round(report.Median,2);
            set
            {
                if (report.Median != value)
                {
                    report.Median = value;
                }
            }
        }
        public int Count
        {
            get => report.Count;
            set
            {
                if (report.Count != value)
                {
                    report.Count = value;
                }
            }
        }


    }
}
