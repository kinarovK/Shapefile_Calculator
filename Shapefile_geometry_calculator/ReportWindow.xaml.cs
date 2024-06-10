using Shapefile_geometry_calculator.ViewModel;
using Shapefile_geometry_calculator.ViewModel.SharedViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Shapefile_geometry_calculator
{
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        public ReportWindow(IReportData_Service reportData_Service)
        {
            InitializeComponent();
            DataContext = new ReportWindowViewModel(this, reportData_Service);
        }
    }
}
