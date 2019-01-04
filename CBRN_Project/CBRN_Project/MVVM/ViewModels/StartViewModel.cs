using CBRN_Project.Data_Access;
using CBRN_Project.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CBRN_Project.MVVM.ViewModels
{
    public class StartViewModel : WorkspaceViewModel
    {
        private ApplicationViewModel applicationViewModel;
        IDialogService dialogService;

        public StartViewModel(ApplicationViewModel _ApplicationViewModel, IDialogService _DialogService)
        {
            applicationViewModel = _ApplicationViewModel;
            dialogService = _DialogService;
        }

        #region Select Input Scheme Command

        #region Input Scheme 1 Command

        RelayCommand selectInputScheme1Command;

        public ICommand SelectInputScheme1Command
        {
            get
            {
                if (selectInputScheme1Command == null)
                    selectInputScheme1Command = new RelayCommand(t => this.InputScheme1Selected());

                return selectInputScheme1Command;
            }
        }

        void InputScheme1Selected()
        {
            applicationViewModel.CurrentPageViewModel = new MainWindowViewModel(dialogService, 1);
        }

        #endregion

        #region Input Scheme 2 Command

        RelayCommand selectInputScheme2Command;

        public ICommand SelectInputScheme2Command
        {
            get
            {
                if (selectInputScheme2Command == null)
                    selectInputScheme2Command = new RelayCommand(t => this.InputScheme2Selected());

                return selectInputScheme2Command;
            }
        }

        void InputScheme2Selected()
        {
            applicationViewModel.CurrentPageViewModel = new MainWindowViewModel(dialogService, 2);
        }

        #endregion

        #endregion

    }
}
