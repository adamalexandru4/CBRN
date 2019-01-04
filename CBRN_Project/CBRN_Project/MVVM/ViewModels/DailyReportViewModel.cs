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
                        NewRTD = elem.Value.NewRTD
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
