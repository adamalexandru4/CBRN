using System.Collections.Generic;
using CBRN_Project.Data_Access;

namespace CBRN_Project.MVVM.Models.Chemical
{
    class ChemModel
    {
        #region Fields

        private readonly DataService dataService;

        private readonly MethParams methParams;

        private readonly List<Icon> icons;
        private readonly List<ChemExIcon> exIcons;

        private readonly Dictionary<string, int> cohorts;
        private readonly Dictionary<string, List<(double, int)>> CIPs;

        #endregion

        #region Constructors

        public ChemModel(DataService dataService, MethParams methParams, IEnumerable<Icon> icons)
        {
            this.dataService = dataService;

            this.methParams = methParams;

            this.icons = new List<Icon>(icons);
            exIcons    = new List<ChemExIcon>();

            cohorts = new Dictionary<string, int>();
            CIPs    = new Dictionary<string, List<(double, int)>>();
        }

        #endregion

        #region Methods

        private void CalcEffCh()
        {
            throw new System.NotImplementedException();
        }

        public void MakeExIcons()
        {
            if (methParams.CalcEffCh) CalcEffCh();

            PbtiesUnit  pbtiesUnit  = new PbtiesUnit(dataService, methParams.Agent, methParams.ChTypes);
            PopsUnit    popsUnit    = new PopsUnit(methParams.Agent, methParams.ChTypes);

            ChemExIcon exIcon;
            foreach (var icon in icons)
            {
                exIcon = new ChemExIcon()
                {
                    Icon    = icon,
                    Pbties  = pbtiesUnit.CalcPbties(icon)
                };
                exIcon.Pops = popsUnit.CalcPops(icon, exIcon.Pbties);
                exIcons.Add(exIcon);
            }
        }

        public void MakeCohorts()
        {
            CohortsUnit cohortsUnit = new CohortsUnit(dataService, methParams.Agent, methParams.ChTypes);

            cohortsUnit.Init(cohorts);
            cohortsUnit.CalcCohorts(exIcons, cohorts);
            cohortsUnit.RemoveNulls(cohorts);
        }

        public void MakeCIPs()
        {
            new CIPsUnit(dataService, methParams.Agent, methParams.ChTypes).Init(cohorts, CIPs);
        }

        public Dictionary<int, DailyReport> MakeReport()
        {
            Dictionary<int, DailyReport> report = new Dictionary<int, DailyReport>();

            new ReportUnit(methParams, cohorts, CIPs).RunSimulation(report);

            return report;
        }

        #endregion
    }
}
