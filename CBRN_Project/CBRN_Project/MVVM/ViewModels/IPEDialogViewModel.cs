using CBRN_Project.MVVM.Models;
using CBRN_Project.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CBRN_Project.MVVM.ViewModels
{
    public class IPEDialogViewModel : ViewModelBase, IDialogRequestClose
    {
        public IPEDialogViewModel()
        {
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        #region Commands

        RelayCommand okCommand;
        RelayCommand cancelCommand;

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

        #endregion

        #region Properties
        
        private float inhalation;

        public float Inhalation
        {
            get
            {
                return inhalation;
            }
            set
            {
                if (value == inhalation)
                    return;

                inhalation = value;

                base.OnPropertyChanged("Inhalation");
            }
        }

        private float pervVap;

        public float PervVap
        {
            get
            {
                return pervVap;
            }
            set
            {
                if (pervVap == value)
                    return;

                pervVap = value;

                OnPropertyChanged("PervVap");
            }
        }

        private float percLiq;

        public float PercLiq
        {
            get
            {
                return percLiq;
            }
            set
            {
                if (percLiq == value)
                    return;

                percLiq = value;
                OnPropertyChanged("PercLiq");
            }
        }

        private float betaRad;

        public float BetaRad
        {
            get
            {
                return betaRad;
            }
            set
            {
                if (betaRad == value)
                    return;

                betaRad = value;
                OnPropertyChanged("BetaRad");
            }
        }

        #endregion

    }
}
