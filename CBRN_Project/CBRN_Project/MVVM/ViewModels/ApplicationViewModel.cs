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
    public class ApplicationViewModel : WorkspaceViewModel
    {
        public static WorkspaceViewModel currentPageViewModel;
        private IDialogService dialogService;

        public ApplicationViewModel(IDialogService _DialogService)
        {
            currentPageViewModel = new StartViewModel(this, _DialogService);
            dialogService = _DialogService;
        }

        public WorkspaceViewModel CurrentPageViewModel
        {
            get
            {
                return currentPageViewModel;
            }
            set
            {
                if (currentPageViewModel != value)
                {
                    currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }

            }
        }

 
    }
}
