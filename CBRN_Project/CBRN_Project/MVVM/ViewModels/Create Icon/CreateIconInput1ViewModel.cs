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
    public class CreateIconInput1ViewModel : ViewModelBase
    {
        #region Fields

        readonly Icon icon;
        readonly IconRepository iconRepository;

        IDialogService dialogService;

        RelayCommand saveCommand;
        RelayCommand changeToTextBoxBreathingRate;
        RelayCommand editIpeCommand;
        RelayCommand vehicleShelterCommand;
        RelayCommand addChallengeCommand;

        #endregion

        #region Constructor

        public CreateIconInput1ViewModel(Icon newIcon, IconRepository newIconRepository, 
                                        IDialogService dialogService)
        {
            icon = newIcon ?? throw new ArgumentNullException("icon");
            iconRepository = newIconRepository ?? throw new ArgumentNullException("iconrepository");
            this.dialogService = dialogService;

            this.DisplayName = "New icon";

            ChallengeTypes = new ObservableCollection<ChallengeTypeClass>(MethParamsDisplay.GetChTypesList());

            CreateBreathingRateValues();
            CreateIpeClasses();
            CreateUniformList();
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

        public double BodySurfaceArea
        {
            get
            {
                return icon.BodySurfaceArea;
            }
            set
            {
                if (value == icon.BodySurfaceArea)
                    return;

                icon.BodySurfaceArea = value;

                base.OnPropertyChanged("BodySurfaceArea");
            }
        }

        #region Challenge

        private double stepValue;

        public double StepValue
        {
            get { return stepValue; }
            set
            {
                stepValue = value;
                OnPropertyChanged("StepValue");
            }
        }

        private double challengeValue;

        public double ChallengeValue
        {
            get
            {
                return challengeValue;
            }
            set
            {
                challengeValue = value;
                OnPropertyChanged("ChallengeValue");
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

        private ChallengeTypeClass challengeSelected;

        public ChallengeTypeClass ChallengeSelected
        {
            get { return challengeSelected; }
            set
            {
                challengeSelected = value;
                OnPropertyChanged("ChallengeSelected");
            }
        }

        #endregion

        #region Breathing Rate

        private ObservableCollection<string> breathingRateList;

        public ObservableCollection<string> BreathingRateList
        {
            get { return breathingRateList; }
            set
            {
                breathingRateList = value;

                OnPropertyChanged("BreathingRateList");
            }
        }

        private string breathingRateSelected;

        public string BreathingRateSelected
        {
            get { return breathingRateSelected; }
            set
            {
                breathingRateSelected = value;
                icon.BreathingRate.BreathingRateActivityLevel = breathingRateSelected;

                OnPropertyChanged("BreathingRateSelected");
            }
        }

        private bool breathingRateTextBoxVisiblity = false;
        
        public bool BreathingRateTextBoxVisibility
        {
            get { return breathingRateTextBoxVisiblity; }
            set
            {
                breathingRateTextBoxVisiblity = value;
                OnPropertyChanged("BreathingRateTextBoxVisibility");
            }
        }

        private bool breathingRateComboBoxVisiblity = true;

        public bool BreathingRateComboBoxVisibility
        {
            get { return breathingRateComboBoxVisiblity; }
            set
            {
                breathingRateComboBoxVisiblity = value;
                OnPropertyChanged("BreathingRateComboBoxVisibility");
            }
        }

        public double BreathingRateChemAg
        {
            get
            {
                return icon.BreathingRate.ChemAg_Ih;
            }
            set
            {
                if (value == icon.BreathingRate.ChemAg_Ih)
                    return;

                icon.BreathingRate.ChemAg_Ih = value;

                base.OnPropertyChanged("BreathingRateChemAg");
            }
        }

        public double BreathingRateBioAg
        {
            get
            {
                return icon.BreathingRate.BioAg_RadPar_Ih;
            }
            set
            {
                if (value == icon.BreathingRate.BioAg_RadPar_Ih)
                    return;

                icon.BreathingRate.BioAg_RadPar_Ih = value;
                base.OnPropertyChanged("BreathingRateBioAg");
            }
        }

        void CreateBreathingRateValues()
        {
            icon.BreathingRate = new BreathingRate();

            var list = BreathingRate.GetActivityLevelList();
            BreathingRateList = new ObservableCollection<string>();
            foreach(var elem in list)
            {
                BreathingRateList.Add(elem);
            }

            BreathingRateSelected = icon.BreathingRate.BreathingRateActivityLevel;
        }

        #region Breathing Rate Command

        public ICommand ChangeToTextBoxBreathingRate
        {
            get
            {
                if(changeToTextBoxBreathingRate == null)
                {
                    changeToTextBoxBreathingRate = new RelayCommand(param => this.ChangeVisiblitiesBreathingRate());
                }
                return changeToTextBoxBreathingRate;
            }
        }

        public void ChangeVisiblitiesBreathingRate()
        {
            BreathingRateComboBoxVisibility = false;
            BreathingRateTextBoxVisibility = true;
        }

        #endregion

        #endregion

        #region IPE 

        private ObservableCollection<string> ipeList;

        public ObservableCollection<string> IpeList
        {
            get { return ipeList; }
            set
            {
                ipeList = value;

                OnPropertyChanged("IpeList");
            }
        }

        private string ipeSelected;

        public string IpeSelected
        {
            get { return ipeSelected; }
            set
            {
                ipeSelected = value;
                icon.IPE.IpeClass = ipeSelected;

                OnPropertyChanged("IpeSelected");
            }
        }

        void CreateIpeClasses()
        {
            icon.IPE = new ProtFactors();

            IpeList = new ObservableCollection<string>();
            var list = ProtFactors.GetProtectionFactorsList();
            foreach(var elem in list)
            {
                IpeList.Add(elem);
            }

            IpeSelected = icon.IPE.IpeClass;
        }

        public ICommand EditIpeCommand
        {
            get
            {
                if (editIpeCommand == null)
                {
                    editIpeCommand = new RelayCommand(param => this.EditIpe());
                }

                return editIpeCommand;
            }
        }

        void EditIpe()
        {
            var viewModel = new IPEDialogViewModel();
            
            bool? result = dialogService.ShowDialog(viewModel);
            if(result.HasValue)
            {
                if(result.Value)
                {
                    icon.IPE.Inhalation = (viewModel.Inhalation == 0) ? 1 : viewModel.Inhalation;
                    icon.IPE.PervVap    = (viewModel.PervVap == 0) ? 1 : viewModel.PervVap;
                    icon.IPE.PercLiq    = (viewModel.PercLiq == 0) ? 1 : viewModel.PercLiq;
                    icon.IPE.BetaRad    = (viewModel.BetaRad == 0) ? 1 : viewModel.BetaRad;
                    
                    IpeList.Add("Default");
                    IpeSelected = IpeList.First(t => t == "Default");
                }
                else
                {
                    // If cancel is pressed
                }
            }

        
        }

        #endregion

        #region Vehicle Shelter

        public ICommand VehicleShelterCommand
        {
            get
            {
                if (vehicleShelterCommand == null)
                {
                    vehicleShelterCommand = new RelayCommand(param => this.EditVehicleShelter());
                }
                return vehicleShelterCommand;
            }
        }

        void EditVehicleShelter()
        {
            var viewModel = new VehicleShelterDialogViewModel();

            bool? result = dialogService.ShowDialog(viewModel);
            if (result.HasValue)
            {
                if (result.Value)
                {
                    if(viewModel.VehicleShelterTypeSelected == "With ColPro")
                    {
                        icon.Vehicle_Shelter.InhalationPercutaneousVapourProtection.SetWithColPro(viewModel.VentilationClassSelected);
                    }
                    if(viewModel.VehicleShelterTypeSelected == "Without ColPro")
                    {
                        icon.Vehicle_Shelter.InhalationPercutaneousVapourProtection.SetWithoutColPro(viewModel.AER, viewModel.Duration, viewModel.Occupancy);
                    }
                    else
                    {
                        icon.Vehicle_Shelter.InhalationPercutaneousVapourProtection.SetDefault();
                    }
                }
                else
                {
                    // If cancel is pressed
                }
            }
        }

        public double NeutronRadiation
        {
            get
            {
                return icon.Vehicle_Shelter.RadiationProtection.NeutronRadiation;
            }
            set
            {
                icon.Vehicle_Shelter.RadiationProtection.NeutronRadiation = value;
                OnPropertyChanged("NeutronRadiation");
            }
        }

        public double GammaRadiation
        {
            get
            {
                return icon.Vehicle_Shelter.RadiationProtection.GammaRadiation;
            }
            set
            {
                icon.Vehicle_Shelter.RadiationProtection.GammaRadiation = value;
                OnPropertyChanged("GammaRadiation");
            }
        }

        public double BlastShielding
        {
            get
            {
                return icon.Vehicle_Shelter.BlastProtection.ProtectionFactor;
            }
            set
            {
                icon.Vehicle_Shelter.BlastProtection.ProtectionFactor = value;
                OnPropertyChanged("BlastShielding");
            }
        }

        public double Prophylaxis
        {
            get
            {
                return icon.Prophylaxis.ProtectionFactor;
            }
            set
            {
                icon.Prophylaxis.ProtectionFactor = value;
                OnPropertyChanged("Prophylaxis");
            }
        }

        private ObservableCollection<string> uniformList;

        public ObservableCollection<string> UniformList
        {
            get
            {
                return uniformList;
            }
            set
            {
                if (uniformList == value)
                    return;
                uniformList = value;
                OnPropertyChanged("UniformList");
            }
        }

        private string uniformListSelected;

        public string UniformListSelected
        {
            get { return uniformListSelected; }
            set
            {
                uniformListSelected = value;
                icon.Uniform.UniformType = uniformListSelected;
                OnPropertyChanged("UniformListSelected");
            }
        }

        void CreateUniformList()
        {
            UniformList = new ObservableCollection<string>();

            UniformList.Add("Bare Skin");
            UniformList.Add("BDU + T-shirt");
            UniformList.Add("BDU + T-shirt + Airspace");
            UniformList.Add("BDO");
            UniformList.Add("BDO + Airspace");
            UniformList.Add("BDO + Airspace + T-shirt");
            UniformList.Add("BDO + BDU + T-shirt + Airspace");
        }
        #endregion

        #endregion

        #region Add Challenge

        public ICommand AddChallengeCommand
        {
            get
            {
                if(addChallengeCommand == null)
                {
                    addChallengeCommand = new RelayCommand(t => this.AddChallenge());
                }

                return addChallengeCommand;
            }
        }

        void AddChallenge()
        {
            var alreadyInsertedChallenge = icon.Challenges.Find(t => t.ChallengeType == ChallengeSelected.ChallengeType);

            if (alreadyInsertedChallenge == null)
            {
                var challenge = new Challenge
                {
                    Agent = MainWindowViewModel.Instance.agent,
                    ChallengeType = ChallengeSelected.ChallengeType,
                    Step = StepValue,
                    Values = new List<double>()
                };
                challenge.Values.Add(ChallengeValue);
                icon.Challenges.Add(challenge);

                if(MainWindowViewModel.methParamsInstance.ChTypes.Find(t => t == challengeSelected.ChallengeType) == null)
                    MainWindowViewModel.methParamsInstance.ChTypes.Add(challengeSelected.ChallengeType);
            }
            else
            {
                alreadyInsertedChallenge.Values.Add(ChallengeValue);
            }

            ChallengeValue = 0;
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

            MainWindowViewModel.methParamsInstance.CalcEffCh = true;

            if (MainWindowViewModel.Instance.IconsList.IconsList.Count > 0)
                MainWindowViewModel.Instance.MethParamsWorkspace.NewTabVisibility = true;

            MainWindowViewModel.Instance.Workspace = MainWindowViewModel.Instance.MethParamsWorkspace;
            MainWindowViewModel.methParamsInstance.Agent = MainWindowViewModel.Instance.agent;

            base.OnPropertyChanged("DisplayName");
        }

        bool CanSave
        {
            get
            {
                if (Personnel != 0 &&
                   !String.IsNullOrEmpty(IpeSelected) &&
                   StepValue != 0 &&
                   icon.Challenges.Count > 0 &&
                   NeutronRadiation != 0 && GammaRadiation != 0)
                    return true;
                else
                    return false;
            }
        }

        bool IsNewIcon
        {
            get { return !iconRepository.ContainsIcon(icon); }
        }

        #endregion

    }
}
