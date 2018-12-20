using System.Collections.Generic;
using CBRN_Project.Data_Access;

namespace CBRN_Project.MVVM.Models.Chemical
{
    class ChemModel
    {
        #region Fields

        private readonly DataService dataService;

        private readonly string agent;
        private readonly List<string> chTypes;

        private readonly List<Icon> icons;
        private readonly List<ChemExIcon> exIcons;

        private readonly Dictionary<string, double> cohorts;
        private readonly Dictionary<string, Dictionary<double, double>> CIPs;

        #endregion

        #region Constructors

        public ChemModel(DataService dataService, IEnumerable<Icon> icons)
        {
            this.dataService = dataService;

            this.icons = new List<Icon>(icons);
            exIcons    = new List<ChemExIcon>();

            cohorts = new Dictionary<string, double>();
            CIPs    = new Dictionary<string, Dictionary<double, double>>();
        }

        #endregion

        #region Methods

        public void MakeExIcons()
        {
            PbtiesUnit pbtiesUnit = new PbtiesUnit(dataService, agent, chTypes);
            PopsUnit popsUnit = new PopsUnit(agent, chTypes);

            ChemExIcon exIcon;
            foreach (var icon in icons)
            {
                exIcon = new ChemExIcon()
                {
                    Icon = icon,
                    Pbties = pbtiesUnit.CalcPbties(icon)
                };
                exIcon.Pops = popsUnit.CalcPops(icon, exIcon.Pbties);
                exIcons.Add(exIcon);
            }
        }

        public void MakeCohorts()
        {
            CohortsUnit cohortsUnit = new CohortsUnit(dataService, agent, chTypes);

            cohortsUnit.Init(cohorts);
            cohortsUnit.CalcCohorts(exIcons, cohorts);
        }

        public void MakeCIPs()
        {
            CIPsUnit CIPsUnit = new CIPsUnit(dataService, agent, chTypes);

            CIPsUnit.Init(CIPs);
        }

        #endregion
    }
}
