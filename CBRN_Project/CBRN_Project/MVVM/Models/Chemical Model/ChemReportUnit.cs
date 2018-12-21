using System;
using System.Collections.Generic;

namespace CBRN_Project.MVVM.Models.Chemical
{
    using Cohorts   = Dictionary<string, double>;
    using CIPs      = Dictionary<string, Dictionary<double, uint>>;
    using Report    = Dictionary<uint, OutputData>;

    class ChemReportUnit
    {
        #region Fields

        private uint prevISL;
        private uint crrtISL;
        private uint crrtDay;

        private readonly MethParams methParams;
        private readonly Cohorts cohorts;
        private readonly CIPs CIPs;

        private Report report;

        #endregion

        #region Constructors

        public ChemReportUnit(MethParams methParams, Cohorts cohorts, CIPs CIPs)
        {
            prevISL = 0;
            crrtISL = 0;
            crrtDay = 1;

            this.methParams = methParams;
            this.cohorts = cohorts;
            this.CIPs = CIPs;

            report = new Report();
        }
        
        #endregion

        #region Methods

        private bool IsKIA(KeyValuePair<double, uint> timeLvlPair)
        {
            if (timeLvlPair.Key < methParams.T_MTF   &&
                timeLvlPair.Key > methParams.T_death &&
                timeLvlPair.Value == 4)
            {
                return true;
            }
            return false;
        }

        private bool IsDOW(KeyValuePair<double, uint> timeLvlPair)
        {
            if (timeLvlPair.Key > methParams.T_MTF   &&
                timeLvlPair.Key > methParams.T_death &&
                timeLvlPair.Value == 4)
            {
                return true;
            }
            return false;
        }

        private bool Has_ISL_changed(KeyValuePair<double, uint> timeLvlPair)
        {
            if (timeLvlPair.Value == prevISL)
            {
                return false;
            }
            return true;
        }

        private bool Is_ISL_zero(KeyValuePair<double, uint> timeLvlPair)
        {
            if (timeLvlPair.Value == 0)
            {
                return true;
            }
            return false;
        }

        private void Report(OutputData outputData)
        {
            if (report.ContainsKey(crrtDay))
            {
                report[crrtDay] += outputData;
                ++crrtDay;
            }
            else
            {
                report.Add(crrtDay, outputData);
                ++crrtDay;
            }
        }

        private void RunSimWithoutFlagMT()
        {
            OutputData outputData = new OutputData();

            Dictionary<double, uint> timeLvlPairs;
            foreach (var cohortKey in cohorts.Keys)
            {
                timeLvlPairs = CIPs[cohortKey];
                foreach (var timeLvlPair in timeLvlPairs)
                {
                    if (timeLvlPair.Key > crrtDay * 1440) Report(outputData);

                    if (IsKIA(timeLvlPair))
                    {
                        outputData.NewKIA = cohorts[cohortKey];
                        outputData.NewFAT = cohorts[cohortKey];
                        outputData.KIA   += cohorts[cohortKey];
                        outputData.FAT   += cohorts[cohortKey];
                        Report(outputData);
                        return;
                    }
                    else if (IsDOW(timeLvlPair))
                    {
                        outputData.NewDOW = cohorts[cohortKey];
                        outputData.NewFAT = cohorts[cohortKey];
                        outputData.DOW   += cohorts[cohortKey];
                        outputData.FAT   += cohorts[cohortKey];
                        Report(outputData);
                        return;
                    }
                    else if (Has_ISL_changed(timeLvlPair))
                    {
                        if (Is_ISL_zero(timeLvlPair))
                        {
                            outputData.NewRTD = cohorts[cohortKey];
                            outputData.RTD   += cohorts[cohortKey];
                            Report(outputData);
                            return;
                        }
                        else
                        {
                            if (timeLvlPair.Value > crrtISL) prevISL = crrtISL = timeLvlPair.Value;
                            else                             prevISL = timeLvlPair.Value;
                        }
                    }
                }
            }
        }

        private void RunSimWithFlagMT()
        {
            throw new NotImplementedException();
        }

        public Report RunSimulation()
        {
            if (methParams.FlagMT) RunSimWithFlagMT();
            else                   RunSimWithoutFlagMT();

            return report;
        }

        #endregion
    }
}
