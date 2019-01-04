using CBRN_Project.Data_Access;
using CBRN_Project.MVVM.Models;
using CBRN_Project.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CBRN_Project.MVVM.ViewModels
{

    public class ChallengeTypeClass : ViewModelBase
    {
        public bool isChecked { get; set; }
        public string value { get; set; }

        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }

        public string ChallengeType
        {
            get { return value; }
            set
            {
                this.value = value;
                OnPropertyChanged("ChallengeType");
            }
        }
    }


    public class MethParamsDisplay
    {
        public enum ChallengeType { Chemical, Biological, Radiological, Nuclear }

        public List<string> ChallengeTypeList;
        public string ChType { get; set; }
        public List<string> Agents { get; set; }
        public List<string> ChTypes { get; set; }
        public List<double> Values { get; set; }

        public MethParamsDisplay()
        {
            //ChallengeTypeList = GetChallengeTypeList();
            ChType = ChallengeType.Chemical.ToString();
            Agents = MethParams.GetChemAgents();
        }

        public static List<ChallengeTypeClass> GetChTypesList()
        {
            List<ChallengeTypeClass> list = new List<ChallengeTypeClass>();
            list.Add(new ChallengeTypeClass { ChallengeType = "Inhaled", IsChecked = false });
            list.Add(new ChallengeTypeClass { ChallengeType = "Percutaneous Vapour", IsChecked = false });
            list.Add(new ChallengeTypeClass { ChallengeType = "PercLiq", IsChecked = false });
            
            return list;
        }

    }

    public class CreateIconInput2ViewModel : ViewModelBase
    {
        #region Fields

        readonly Icon icon;
        readonly IconRepository iconRepository;
        readonly MethParamsDisplay methParamsDisplay;

        RelayCommand saveCommand;

        #endregion

        #region Constructor

        public CreateIconInput2ViewModel(Icon newIcon, IconRepository newIconRepository)
        {
            icon = newIcon ?? throw new ArgumentNullException("icon");
            iconRepository = newIconRepository ?? throw new ArgumentNullException("iconrepository");

            methParamsDisplay = new MethParamsDisplay();
            methParamsDisplay.ChTypes = new List<string>();
            methParamsDisplay.Values = new List<double>();

            ChallengeTypes = new ObservableCollection<ChallengeTypeClass>(MethParamsDisplay.GetChTypesList());
            AgentsList = new ObservableCollection<string>(methParamsDisplay.Agents);
            this.DisplayName = "New icon";
        }
        
        #endregion

        #region Icon Properties

        public double Personnel
        {
            get { return icon.Personnel; }
            set
            {
                if (value == icon.Personnel)
                    return;

                icon.Personnel = value;

                base.OnPropertyChanged("Personnel");
            }
        }

        #endregion

        #region Challenge

        private ObservableCollection<string> agentsList;

        public ObservableCollection<string> AgentsList
        {
            get { return agentsList; }
            set
            {
                agentsList = value;
                OnPropertyChanged("AgentsList");
            }
        }

        private string agentSelected;

        public string AgentSelected
        {
            get { return agentSelected; }
            set
            {
                agentSelected = value;
                OnPropertyChanged("AgentSelected");
            }
        }

        private ObservableCollection<ChallengeTypeClass> challengeTypes;

        public ObservableCollection<ChallengeTypeClass> ChallengeTypes
        {
            get { return challengeTypes; }
            set
            {
                challengeTypes = value;
                OnPropertyChanged("ChallengeTypes");
            }
        }

        private double effectiveValue;

        public double EffectiveValue
        {
            get { return effectiveValue; }
            set
            {
                effectiveValue = value;
                OnPropertyChanged("EffectiveValue");
            }
        }


        #endregion

        #region Add Command

        RelayCommand addCommand;

        public ICommand AddCommand
        {
            get
            {
                if(addCommand == null)
                {
                    addCommand = new RelayCommand(t => this.AddEffective());
                }

                return addCommand;
            }
        }

        void AddEffective()
        {
            foreach (var elem in ChallengeTypes)
            {
                if (elem.IsChecked)
                {
                    elem.IsChecked = false;
                    icon.EffChallenges.Add(new EffChallenge(AgentSelected, elem.value, EffectiveValue));
                    MainWindowViewModel.methParamsInstance.ChTypes.Add(elem.value);
                }
            }

            MainWindowViewModel.methParamsInstance.Agent = AgentSelected;
            MainWindowViewModel.methParamsInstance.CalcEffCh = false;

            // RESTORE NULL VALUES
            AgentSelected = null;
            EffectiveValue = 0;
        }

        #endregion

        #region Save Command

        public ICommand SaveCommand
        {
            get
            {
                if(saveCommand == null)
                {
                    saveCommand = new RelayCommand(param => this.Save(),
                                                    param => this.CanSave);
                }

                return saveCommand;
            }
        }

        public void Save()
        {
            if (this.IsNewIcon)
                iconRepository.AddIcon(icon);

            if (MainWindowViewModel.Instance.IconsList.IconsList.Count > 0)
                MainWindowViewModel.Instance.MethParamsWorkspace.NewTabVisibility = true;
            MainWindowViewModel.Instance.Workspace = MainWindowViewModel.Instance.MethParamsWorkspace;

            base.OnPropertyChanged("DisplayName");
        }

        bool CanSave
        {
            get
            {
                return true;
            }
        }

        bool IsNewIcon
        {
            get { return !iconRepository.ContainsIcon(icon); }
        }

        #endregion
    }
}
