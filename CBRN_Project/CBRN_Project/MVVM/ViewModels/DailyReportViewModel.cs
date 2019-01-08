using CBRN_Project.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRN_Project.MVVM.ViewModels
{
    public class DataGridDisplay : ViewModelBase
    {
        public int day { get; set; } = 1;
        public double NewKIA { get; set; } = 0;
        public double NewDOW { get; set; } = 0;
        public double NewFAT { get; set; } = 0;
        public double NewWIA { get; set; } = 0;
        public double NewRTD { get; set; } = 0;

        public double KIA { get; set; } = 0;
        public double DOW { get; set; } = 0;
        public double FAT { get; set; } = 0;
        public double WIA1 { get; set; } = 0;
        public double WIA2 { get; set; } = 0;
        public double WIA3 { get; set; } = 0;
        public double WIA { get; set; } = 0;
        public double RTD { get; set; } = 0;

        public int Day
        {
            get { return day; }
            set
            {
                day = value;
                OnPropertyChanged("Day");
            }
        }
    }

    public class DailyReportViewModel : ViewModelBase
    {
        Dictionary<int, DailyReport> report;

        public DailyReportViewModel(Dictionary<int, DailyReport> report)
        {
            this.report = report;
            GenerateListToDisplay(report);
        }

        public void GenerateListToDisplay(Dictionary<int, DailyReport> report)
        {
            MyDictionary = new ObservableCollection<DataGridDisplay>();
            foreach (var elem in report)
            {
                if (elem.Key != 0)
                {
                    MyDictionary.Add(new DataGridDisplay
                    {
                        Day = elem.Key,
                        NewKIA = elem.Value.NewKIA,
                        NewDOW = elem.Value.NewDOW,
                        NewFAT = elem.Value.NewFAT,
                        NewWIA = elem.Value.NewWIA,
                        NewRTD = elem.Value.NewRTD,
                        KIA = elem.Value.KIA,
                        DOW = elem.Value.DOW,
                        FAT = elem.Value.FAT,
                        WIA1 = elem.Value.WIA1,
                        WIA2 = elem.Value.WIA2,
                        WIA3 = elem.Value.WIA3,
                        WIA = elem.Value.WIA,
                        RTD = elem.Value.RTD
                    });
                }
            }
        }

        ObservableCollection<DataGridDisplay> myDictionary;

        public ObservableCollection<DataGridDisplay> MyDictionary
        {
            get
            {
                if (myDictionary == null)
                    myDictionary = new ObservableCollection<DataGridDisplay>();
                return myDictionary;
            }
            set
            {
                myDictionary = value;
                OnPropertyChanged("MyDictionary");
            }
        }

    }
}
