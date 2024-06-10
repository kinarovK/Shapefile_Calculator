using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Shapefile_geometry_calculator.ViewModel.SharedViewModels
{
    internal class EventAggregator
    {
        public static EventAggregator _instance;

        private EventAggregator() { }
       
        public static EventAggregator Instance => _instance = new EventAggregator();


        public event EventHandler<ReportResult_ChangedEventArgs> ReportResultChanged;
        public void PublishReportResultChanged(ReportResult_ChangedEventArgs args)
        {
            ReportResultChanged?.Invoke(this, args);
        }
        
    }
}
