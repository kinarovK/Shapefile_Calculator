using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shapefile_geometry_calculator.AbstractFactory;
using Shapefile_geometry_calculator.Model;
using Shapefile_geometry_calculator.Model.Interfaces;
using Shapefile_geometry_calculator.ViewModel;
using Shapefile_geometry_calculator.ViewModel.SharedViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Shapefile_geometry_calculator
{
    [ExcludeFromCodeCoverage]
    public partial class App : Application
    {
        public static IHost AppHost { get; set; }
        public App()
        {
            HostApplicationBuilder hostBuilder = new HostApplicationBuilder();
            hostBuilder.Configuration.Sources.Clear();
           
            IHost host = hostBuilder.Build();

            IConfiguration config = host.Services.GetService<IConfiguration>();
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<MainWindowViewModel>();
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<Window>();
                    services.AddSingleton<ReportWindowViewModel>();
                    services.AddSingleton<ReportWindow>();
                    services.AddSingleton<IReportData_Service, ReportData_Service>();
                    services.AddScoped<ICalculationManager, CalculationManager>();
                    services.AddScoped<IFolderReader, FolderReader>();
                    services.AddScoped<IShapeGeometryCalculator, ShapeGeometryCalculator>();
                    services.AddScoped<IResultConverter, ResultConverter>();
                    services.AddScoped<IReportCalculator, ReportCalculator>();
                    services.AddScoped<ITableWriter, TableWriter>();
                    services.AddFormFactory<ReportWindow>();
                }).Build();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost.StartAsync();
            var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
            startupForm.Show();
            base.OnStartup(e);
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost.StopAsync();
            base.OnExit(e);
        }
    }
}
