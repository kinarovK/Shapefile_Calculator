using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.WindowsAPICodePack.Dialogs;
using Shapefile_geometry_calculator.AbstractFactory;
using Shapefile_geometry_calculator.Model;
using Shapefile_geometry_calculator.Model.Interfaces;
using Shapefile_geometry_calculator.ViewModel.SharedViewModels;

namespace Shapefile_geometry_calculator.ViewModel
{
    [ExcludeFromCodeCoverage]
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<Enum> comboBoxItems;
        private ObservableCollection<GeometryType> geometryTypes;
        public ObservableCollection<GeometryType> GeometryTypes
        {
            get { return geometryTypes; }
            private set
            {
                geometryTypes = value;
                OnPropertyChanged(nameof(GeometryTypes));
            }
        }
        private ReportWindow reportWindowInstance;
        private readonly IAbstractFactory<ReportWindow> reportWindow;
        private readonly ITableWriter tableWriter;
        private bool isReportWindowOpened = false;
        //Ctor
        public MainWindowViewModel(ICalculationManager calculationManager, IReportCalculator reportCalculator, IAbstractFactory<ReportWindow> reportWindow, ITableWriter tableWriter)
        {

            OpenFolderBrowserCommand = new RelayCommand(OpenFolderBrowser);
            RunCalculationCommand = new RelayCommand(RunCalculation);
            SummarizeResultsCommand = new RelayCommand(SummarizeResults);
            ExportToTableCommand = new RelayCommand(ExportResultToTable);
            OnPropertyChanged(nameof(PathToDisplay));
            this.calculationManager = calculationManager;
            this.reportCalculator = reportCalculator;
            this.reportWindow = reportWindow;
            this.tableWriter = tableWriter;
            ResultToDisplay = new ObservableCollection<Result_Model>();
            OnPropertyChanged(nameof(ResultToDisplay));
            GeometryTypes = new ObservableCollection<GeometryType>
            {
            GeometryType.Polygon,
            GeometryType.Polyline
            };
            selectedGeometryType = GeometryType.Polyline;
            ComboBoxItems = new ObservableCollection<Enum>();
            UpdateComboBoxItems();
            selectedComboBoxItem = ComboBoxItems.First();
        }

        private GeometryType selectedGeometryType;
        public GeometryType SelectedGeometryType
        {
            get { return selectedGeometryType; }
            set
            {
                if (selectedGeometryType != value)
                {
                    selectedGeometryType = value;
                    OnPropertyChanged(nameof(SelectedGeometryType));
                    UpdateComboBoxItems();
                }
            }
        }
        public ICommand OpenFolderBrowserCommand { get; }
        public ICommand RunCalculationCommand { get; }
        public ICommand SummarizeResultsCommand { get; }

        public ICommand ExportToTableCommand { get; }
        private bool isCalculating;
        private string path = string.Empty;
        private readonly ICalculationManager calculationManager;
        private readonly IReportCalculator reportCalculator;
        private List<Result_Model> results;
        private ReportResult_Model reportResult;
        public ObservableCollection<Result_Model> ResultToDisplay { get; set; }//observable?

        public string PathToDisplay
        {
            get { return path; }
            set
            {
                path = value;
                OnPropertyChanged(nameof(PathToDisplay));
            }
        }
        public ObservableCollection<Enum> ComboBoxItems
        {
            get { return comboBoxItems; }
            private set
            {
                    comboBoxItems = value;
                    OnPropertyChanged(nameof(ComboBoxItems));
            }
        }
        private void UpdateComboBoxItems()
        {
            var items = new List<Enum>();
            if (selectedGeometryType == GeometryType.Polygon)
            {
                items = Enum.GetValues(typeof(AreaMeasure)).Cast<Enum>().ToList();
            }
            else if (selectedGeometryType == GeometryType.Polyline)
            {
                items = Enum.GetValues(typeof(LenghtMeasure)).Cast<Enum>().ToList();
            }
            if (comboBoxItems != null)
            {
                comboBoxItems.Clear();
            }
            foreach (var item in items)
            {
                comboBoxItems.Add(item);
            }
            SelectedComboBoxItem = ComboBoxItems.First();
        }

        private Enum selectedComboBoxItem;
        public Enum SelectedComboBoxItem
        {
            get { return selectedComboBoxItem; }
            set
            {
                if (selectedComboBoxItem != value)
                {
                    selectedComboBoxItem = value;
                    OnPropertyChanged(nameof(SelectedComboBoxItem));
                }
            }
        }
        public List<LenghtMeasure> LenghtMeasures { get; }
        public List<AreaMeasure> AreaMeasures { get; }
        public async void OpenFolderBrowser(object obj)
        {
            try
            {
                FolderBrowserDialog fdb = new FolderBrowserDialog();
                if (fdb.ShowDialog() == DialogResult.OK)
                {
                    PathToDisplay = fdb.SelectedPath;
                }
            }
            catch 
            {
                System.Windows.MessageBox.Show("Something wrong, try again", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
        }
        public async void RunCalculation(object obj)
        {
            ResultToDisplay.Clear();
            if (isCalculating)
            {
                System.Windows.MessageBox.Show("Calculation already in progress. Please wait.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (path == string.Empty)
            {
                System.Windows.MessageBox.Show("Select a folder", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                results = await calculationManager.DoCalculation(path, selectedGeometryType, selectedComboBoxItem);
                if (results == null)
                {
                    System.Windows.MessageBox.Show("In selected folder shp-s not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                foreach (var item in results)
                {
                    ResultToDisplay.Add(item);
                }
            }
        }

        public async void SummarizeResults(object obj)
        {
            if (results == null)
            {
                System.Windows.MessageBox.Show("Results is empty for report", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            reportResult = await reportCalculator.ComputeReport(results);
            reportResult.FolderName = PathToDisplay;
            if (!isReportWindowOpened )
            {
                var reportService = App.AppHost.Services.GetRequiredService<IReportData_Service>();
                reportService.ReportResult = reportResult;
                reportWindowInstance = reportWindow.Create();
                reportWindowInstance.Closed += (s, e) => isReportWindowOpened = false;
                reportWindowInstance.Show();
                isReportWindowOpened = true;
            }
            else
            {
                reportWindowInstance.Activate();
            }
        }
        public async void ExportResultToTable(object obj)
        {
            var pathToExport = string.Empty;
            try
            {
                FolderBrowserDialog fdb = new FolderBrowserDialog();
                if (fdb.ShowDialog() == DialogResult.OK)
                {
                    pathToExport = fdb.SelectedPath;
                    await tableWriter.ExportToTable(results, pathToExport, SelectedComboBoxItem);

                    System.Windows.MessageBox.Show("Export completed!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Something wrong, try again", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
        }
  
    }
}
