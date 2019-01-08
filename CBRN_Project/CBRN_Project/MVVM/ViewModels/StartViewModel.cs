using CBRN_Project.Data_Access;
using CBRN_Project.MVVM.Models;
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
    public class StartViewModel : WorkspaceViewModel
    {
        private ApplicationViewModel applicationViewModel;
        IDialogService dialogService;

        public StartViewModel(ApplicationViewModel _ApplicationViewModel, IDialogService _DialogService)
        {
            applicationViewModel = _ApplicationViewModel;
            dialogService = _DialogService;

            TypesOfChallenges = new ObservableCollection<string>();
            TypesOfChallenges.Add("Chemical");
            TypesOfChallenges.Add("Biological");
            TypesOfChallenges.Add("Radiological");
            TypesOfChallenges.Add("Nuclear");


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
            applicationViewModel.CurrentPageViewModel = new MainWindowViewModel(dialogService, 2, typeOfChallengeSelected, agentSelected);
        }

        #endregion

        #region Binding Visibilities

        private bool inputSchemeVisibility = false;

        public bool InputSchemeVisibility
        {
            get
            {
                return inputSchemeVisibility;
            }
            set
            {
                if (inputSchemeVisibility == value)
                    return;

                inputSchemeVisibility = value;
                OnPropertyChanged("InputSchemeVisibility");
            }
        }

        private bool firstSelectionVisibility = true;

        public bool FirstSelectionVisibility
        {
            get
            {
                return firstSelectionVisibility;
            }
            set
            {
                if (value != firstSelectionVisibility)
                {
                    firstSelectionVisibility = value;
                }
                OnPropertyChanged("FirstSelectionVisibility");
            }
        }

        private bool agentsTypesVisibility = false;

        public bool AgentsTypesVisibility
        {
            get
            {
                return agentsTypesVisibility;
            }
            set
            {
                agentsTypesVisibility = true;
                OnPropertyChanged("AgentsTypesVisibility");
            }
        }

        private bool nextButtonVisibility = false;

        public bool NextButtonVisibility
        {
            get
            {
                return nextButtonVisibility;
            }
            set
            {
                nextButtonVisibility = value;
                OnPropertyChanged("NextButtonVisibility");
            }
        }

        #endregion

        #region Binding ComboBoxes

        private ObservableCollection<string> typesOfChallenge;

        public ObservableCollection<string> TypesOfChallenges
        {
            get
            {
                return typesOfChallenge;
            }
            set
            {
                typesOfChallenge = value;
                OnPropertyChanged("TypesOfChallenges");
            }
        }

        private string typeOfChallengeSelected;

        public string TypeOfChallengeSelected
        {
            get
            {
                return typeOfChallengeSelected;
            }
            set
            {
                typeOfChallengeSelected = value;

                if (typeOfChallengeSelected == "Chemical")
                {

                    AgentsTypesVisibility = true;
                    AgentsTypesForChallenge = new ObservableCollection<string>(MethParams.GetChemAgents());
                }
                else
                {
                    AgentsTypesForChallenge.Clear();
                }

                OnPropertyChanged("TypeOfChallengeSelected");
            }
        }

        private ObservableCollection<string> agentsTypesForChallenge;

        public ObservableCollection<string> AgentsTypesForChallenge
        {
            get
            {
                return agentsTypesForChallenge;
            }

            set
            {
                agentsTypesForChallenge = value;
                OnPropertyChanged("AgentsTypesForChallenge");
            }
        }

        private string agentSelected;

        public string AgentSelected
        {
            get
            {
                return agentSelected;
            }
            set
            {
                agentSelected = value;
                if (value == "VX")
                {
                    NextButtonVisibility = true;
                }
                OnPropertyChanged("AgentSelected");
            }
        }

        #endregion

        #region Commands

        RelayCommand nextCommand;

        public ICommand NextCommand
        {
            get
            {
                if (nextCommand == null)
                {
                    nextCommand = new RelayCommand(param => this.Next());
                }

                return nextCommand;
            }
        }

        void Next()
        {
            FirstSelectionVisibility = false;
            InputSchemeVisibility = true;
        }

        #endregion

        #endregion

    }
}
