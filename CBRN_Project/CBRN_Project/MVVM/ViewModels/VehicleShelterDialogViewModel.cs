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
    public class VehicleShelterDialogViewModel : ViewModelBase, IDialogRequestClose
    {
        public VehicleShelterDialogViewModel()
        {
            VehicleShelterType = new ObservableCollection<string>();
            VehicleShelterType.Add("None");
            VehicleShelterType.Add("With ColPro");
            VehicleShelterType.Add("Without ColPro");

            VehicleShelterTypeSelected = VehicleShelterType.First(param => param == "None");

            VentilationClass = new ObservableCollection<string>();
            VentilationClass.Add("Vehicle w/ColPro");
            VentilationClass.Add("Shelter w/ColPro");
            
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        #region Commands

        RelayCommand okCommand;
        RelayCommand cancelCommand;
        RelayCommand nextCommand;

        public ICommand OkCommand
        {
            get
            {
                if (okCommand == null)
                {
                    okCommand = new RelayCommand(param => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
                }

                return okCommand;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new RelayCommand(param => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
                }

                return cancelCommand;
            }
        }

        public ICommand NextCommand
        {
            get
            {
                if(nextCommand == null)
                {
                    nextCommand = new RelayCommand(param => this.SetValues());
                }
                return nextCommand;
            }
        }

        void SetValues()
        {
            if (vehicleShelterTypeSelected == "With ColPro")
            {
                StartupChoose = false;
                WithColProVisibility = true;

            }
            else if(vehicleShelterTypeSelected == "Without ColPro")
            {
                StartupChoose = false;
                WithoutColProVisibility = true;
            }
            else
            {
                CloseRequested.Invoke(this, new DialogCloseRequestedEventArgs(true));
            }

        }

        #endregion

        #region Properties

        private bool withoutColProVisibility = false;

        public bool WithoutColProVisibility
        {
            get
            {
                return withoutColProVisibility;
            }
            set
            {
                withoutColProVisibility = value;
                OnPropertyChanged("WithoutColProVisibility");
            }
        }

        private bool withColProVisibility = false;

        public bool WithColProVisibility
        {
            get
            {
                return withColProVisibility;
            }
            set
            {
                withColProVisibility = value;
                OnPropertyChanged("WithColProVisibility");
            }
        }

        private bool startupChoose = true;

        public bool StartupChoose
        {
            get
            {
                return startupChoose;
            }
            set
            {
                startupChoose = value;
                OnPropertyChanged("StartupChoose");
            }
        }

        private ObservableCollection<string> vehicleShelterType;

        public ObservableCollection<string> VehicleShelterType
        {
            get
            {
                return vehicleShelterType;
            }
            set
            {
                if (vehicleShelterType == null)
                    vehicleShelterType = new ObservableCollection<string>();

                vehicleShelterType = value;
                OnPropertyChanged("VehicleShelterType");
            }
        }

        private string vehicleShelterTypeSelected;

        public string VehicleShelterTypeSelected
        {
            get
            {
                return vehicleShelterTypeSelected;
            }
            set
            {
                vehicleShelterTypeSelected = value;
                OnPropertyChanged("VehicleShelterTypeSelected");
            }
        }

        #region Without ColPro

        private double aer;

        public double AER
        {
            get
            {
                return aer;
            }
            set
            {
                if (value == aer)
                    return;

                aer = value;

                base.OnPropertyChanged("AER");
            }
        }

        private double duration;

        public double Duration
        {
            get
            {
                return duration;
            }
            set
            {
                if (value == duration)
                    return;

                duration = value;

                base.OnPropertyChanged("Duration");
            }
        }

        private double occupancy;

        public double Occupancy
        {
            get
            {
                return occupancy;
            }
            set
            {
                if (value == occupancy)
                    return;

                occupancy = value;

                base.OnPropertyChanged("Occupancy");
            }
        }

        #endregion

        #region With ColPro

        private ObservableCollection<string> ventilationClass;

        public ObservableCollection<string> VentilationClass
        {
            get
            {
                return ventilationClass;
            }
            set
            {
                if (ventilationClass == null)
                    ventilationClass = new ObservableCollection<string>();

                ventilationClass = value;
                OnPropertyChanged("VentilationClass");
            }
        }

        private string ventilationClassSelected;

        public string VentilationClassSelected
        {
            get
            {
                return ventilationClassSelected;
            }
            set
            {
                ventilationClassSelected = value;
                OnPropertyChanged("VentilationClassSelected");
            }
        }

        #endregion

        #endregion
    }
}
