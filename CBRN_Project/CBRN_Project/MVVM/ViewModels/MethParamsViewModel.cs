using CBRN_Project.MVVM.Models;
using CBRN_Project.MVVM.Models.Chemical;
using CBRN_Project.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CBRN_Project.MVVM.ViewModels
{
    public class MethParamsViewModel : WorkspaceViewModel
    {
        #region Fields

        public static MethParamsViewModel Instance = null;
        
        #endregion

        #region Constructor

        public MethParamsViewModel()
        {
            if (Instance == null)
                Instance = this;
            
            NewTabVisibility = false;
            InsertMethParamsVisibility = false;

            MakeCasualtyCritList();

            this.DisplayName = "Methodology Parameters";
        }

        #endregion

        #region Next Step Comamnd

        RelayCommand nextStepCommand;

        public ICommand NextStepCommand
        {
            get
            {
                if (nextStepCommand == null)
                    nextStepCommand = new RelayCommand(t => this.NextStep());

                return nextStepCommand;
            }
        }

        void NextStep()
        {
            if (MainWindowViewModel.Instance.IconsList.TotalIcons > 0)
            {
                if (newTabVisibility == true)
                {
                    NewTabVisibility = false;
                    InsertMethParamsVisibility = true;
                }
            }
        }

        #endregion

        #region Save Command

        RelayCommand saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                    saveCommand = new RelayCommand(t => this.Save());

                return saveCommand;
            }
        }
        
        public void Save()
        {
            InsertMethParamsVisibility = false;
            NewTabVisibility = true;

            ChemModel chemModel = new ChemModel(MainWindowViewModel.dataServiceInstance,
                                                MainWindowViewModel.methParamsInstance,
                                                MainWindowViewModel.Instance.iconRepository.GetIcons());

            chemModel.MakeExIcons();
            chemModel.MakeCohorts();
            chemModel.MakeCIPs();

            Dictionary<int, DailyReport> report;
            report = chemModel.MakeReport();
            MainWindowViewModel.Instance.ShowReport(report);
        }
        #endregion

        #region Variables
        
        public double TMTF
        {
            get { return MainWindowViewModel.methParamsInstance.T_MTF; }
            set
            {
                MainWindowViewModel.methParamsInstance.T_MTF = value;
                OnPropertyChanged("TMTF");
            }
        }

        public double TDEATH
        {
            get { return MainWindowViewModel.methParamsInstance.T_death; }
            set
            {
                MainWindowViewModel.methParamsInstance.T_death = value;
                OnPropertyChanged("TDEATH");
            }
        }

        public bool FLAGMT
        {
            get
            {
                return MainWindowViewModel.methParamsInstance.FlagMT;
            }
            set
            {
                MainWindowViewModel.methParamsInstance.FlagMT = value;
                OnPropertyChanged("FLAGMT");
            }
        }

        public void MakeCasualtyCritList()
        {
            CasualtyCritList = new ObservableCollection<string>();
            CasualtyCritList.Add("WIA(1)");
            CasualtyCritList.Add("WIA(2)");
            CasualtyCritList.Add("WIA(3)");
        }

        private ObservableCollection<string> casualtyCritList;

        public ObservableCollection<string> CasualtyCritList
        {
            get { return casualtyCritList; }
            set
            {
                casualtyCritList = value;

                OnPropertyChanged("CasualtyCritList");
            }
        }

        public string CasualtyCritSelected
        {
            get
            {
                switch (MainWindowViewModel.methParamsInstance.CasCrit)
                {
                    case 1:
                        return "WIA(1)";
                    case 2:
                        return "WIA(2)";
                    case 3:
                        return "WIA(3)";
                    default: return "";
                }
            }
            set
            {
                switch(value)
                {
                    case "WIA(1)":
                        MainWindowViewModel.methParamsInstance.CasCrit = 1;
                        break;
                    case "WIA(2)":
                        MainWindowViewModel.methParamsInstance.CasCrit = 2;
                        break;
                    case "WIA(3)":
                        MainWindowViewModel.methParamsInstance.CasCrit = 3;
                        break;
                }

                OnPropertyChanged("CasualtyCritSelected");
            }
        }

        public double DTRT
        {
            get
            {
                return MainWindowViewModel.methParamsInstance.D_trt;
            }
            set
            {
                MainWindowViewModel.methParamsInstance.D_trt = Convert.ToUInt32(value);
                OnPropertyChanged("DTRT");
            }
        }

        private bool newTabVisibility = false;

        public bool NewTabVisibility
        {
            get { return newTabVisibility; }
            set
            {
                newTabVisibility = value;
                OnPropertyChanged("NewTabVisibility");
            }
        }

        private bool insertMethParamsVisibility = false;

        public bool InsertMethParamsVisibility
        {
            get
            {
                return insertMethParamsVisibility;
            }
            set
            {
                insertMethParamsVisibility = value;
                OnPropertyChanged("InsertMethParamsVisibility");
            }
        }

        #endregion
    }
}
