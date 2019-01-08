using CBRN_Project.Data_Access;
using CBRN_Project.MVVM.Models;
using CBRN_Project.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CBRN_Project.MVVM.ViewModels
{
    public class MainWindowViewModel : WorkspaceViewModel
    {
        #region Fields

        public static MainWindowViewModel           Instance = null;
        public readonly IconRepository              iconRepository;
        public static DataService                   dataServiceInstance = null;
        public static MethParams                    methParamsInstance = null;

        IconListViewModel                           iconsList;
        //ObservableCollection<WorkspaceViewModel>  workspaces;

        ViewModelBase                               workspace;
        MethParamsViewModel                         methParamsWorkspace;

        public int                                  inputScheme;
        IDialogService                              dialogService;

        public string                               typeOfChallenge;
        public string                               agent;

        #endregion

        #region Constructor

        public MainWindowViewModel(IDialogService dialogService, int inputScheme, string typeOfChallenge = null, string agent = null)
        {
            if (Instance == null)
                Instance = this;

            if (methParamsInstance == null)
                methParamsInstance = new MethParams();

            if (dataServiceInstance == null)
                dataServiceInstance = new DataService();

            MethParamsWorkspace = new MethParamsViewModel();
            Workspace           = MethParamsWorkspace;

            this.typeOfChallenge = typeOfChallenge;
            this.agent = agent;

            this.inputScheme    = inputScheme;
            this.dialogService  = dialogService;
            base.DisplayName    = "MainWindow";
            iconRepository      = new IconRepository();
        }

        #endregion

        #region Icon List

        public IconListViewModel IconsList
        {
            get
            {
                if(iconsList == null)
                {
                    iconsList = new IconListViewModel(iconRepository);
                }
                return iconsList;
            }
        }

        #endregion

        #region Workspaces

        public MethParamsViewModel MethParamsWorkspace
        {
            set
            {
                methParamsWorkspace = value;
                OnPropertyChanged("MethParamsWorkspace");
            }
            get
            {
                if(methParamsWorkspace == null)
                {
                    methParamsWorkspace = MethParamsViewModel.Instance;
                    OnPropertyChanged("MethParamsWorkspace");
                }

                return methParamsWorkspace;
            }
        }

       /* public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if(workspaces == null)
                {
                    workspaces = new ObservableCollection<WorkspaceViewModel>();
                    workspaces.CollectionChanged += this.OnWorkspacesChanged;
                }

                return workspaces;
            }
        }*/

        public ViewModelBase Workspace
        {
            set
            {
                workspace = value;
                OnPropertyChanged("Workspace");
            }
            get
            {
                if(workspace == null)
                {
                    workspace = new WorkspaceViewModel();
                    OnPropertyChanged("Workspace");
                }
                return workspace;
            }
        }
        /*
        void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.OnWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.OnWorkspaceRequestClose;

        }

        void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            workspace.Dispose();
            this.Workspaces.Remove(workspace);

            if (iconsList.TotalIcons > 0 && workspaces.Count == 0)
                NewTabVisibility = true;
        }*/

        #endregion

        #region Commands

        #region Create Icon Command

        RelayCommand createIconCommand;

        public ICommand CreateIconCommand
        {
            get
            {
                if(createIconCommand == null)
                {
                    createIconCommand = new RelayCommand(param => this.CreateNewIcon());
                }

                return createIconCommand;
            }
        }

        void CreateNewIcon()
        {
            MethParamsWorkspace.NewTabVisibility = false;
            MethParamsWorkspace.InsertMethParamsVisibility = false;

            if (Workspace == methParamsWorkspace)
            {

                Icon newIcon = Icon.CreateNewIcon(IconRepository.IconId);
                if (inputScheme == 1)
                {

                    CreateIconInput1ViewModel newWorkspace = new CreateIconInput1ViewModel(newIcon, iconRepository, dialogService);
                    /* this.Workspaces.Add(workspace);
                    this.SetActiveWorkspace(workspace);*/
                    Workspace = newWorkspace;
                    OnPropertyChanged("Workspace");
                }
                else
                {
                    CreateIconInput2ViewModel newWorkspace = new CreateIconInput2ViewModel(newIcon, iconRepository, agent);
                    /*this.Workspaces.Add(workspace);
                    this.SetActiveWorkspace(workspace);*/
                    Workspace = newWorkspace;
                    OnPropertyChanged("Workspace");
                }
            }
        }
        /*
        void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }

        public void CloseWorkspace()
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            WorkspaceViewModel currentViewModel = collectionView.CurrentItem as WorkspaceViewModel;

            if (collectionView != null)
            {
                collectionView.MoveCurrentToPrevious();
            }
            
            currentViewModel.RequestClose += this.OnWorkspaceRequestClose;
            Workspaces.Remove(currentViewModel);

            if (iconsList.TotalIcons > 0 && workspaces.Count == 0)
                NewTabVisibility = true;

        }*/
        #endregion

        #region Remove Icon Command

        RelayCommand removeIconCommand;

        public ICommand RemoveIconCommand
        {
            get
            {
                if (removeIconCommand == null)
                    removeIconCommand = new RelayCommand(t => this.RemoveIcon());

                return removeIconCommand;
            }
        }

        void RemoveIcon()
        {
            iconsList.RemoveIcon();
            
            if (iconsList.IconsList.Count == 0)
                this.methParamsWorkspace.NewTabVisibility = false;

            if (Workspace is DailyReportViewModel)
                Workspace = MethParamsWorkspace;


        }

        #endregion

        #endregion

        #region Show Report

        public void ShowReport(Dictionary<int, DailyReport> report)
        {
            Workspace = new DailyReportViewModel(report);
            OnPropertyChanged("Workspace");
        }

        #endregion
    }
}
