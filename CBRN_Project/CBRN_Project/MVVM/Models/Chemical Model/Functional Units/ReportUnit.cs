using System;
using System.Collections.Generic;

namespace CBRN_Project.MVVM.Models.Chemical
{
    using Cohorts   = Dictionary<string, int>;
    using CIPs      = Dictionary<string, List<(double time, int level)>>;
    using Report    = Dictionary<int, DailyReport>;

    class ReportUnit
    {
        #region Fields

        private readonly MethParams methParams;
        private readonly Cohorts cohorts;
        private readonly CIPs CIPs;

        #endregion

        #region Constructors

        public ReportUnit(MethParams methParams, Cohorts cohorts, CIPs CIPs)
        {
            this.methParams = methParams;
            this.cohorts = cohorts;
            this.CIPs = CIPs;
        }

        #endregion

        #region Methods

        private bool IsKIA((double time, int level) timeLvlPair)
        {
            if (timeLvlPair.time <= methParams.T_MTF   &&
                timeLvlPair.time >= methParams.T_death &&
                timeLvlPair.level == 4)
            {
                return true;
            }
            return false;
        }

        private bool IsDOW((double time, int level) timeLvlPair)
        {
            if (timeLvlPair.time >= methParams.T_MTF   &&
                timeLvlPair.time >= methParams.T_death &&
                timeLvlPair.level == 4)
            {
                return true;
            }
            return false;
        }

        private void Report(Report report, ref int crrtDay, ref DailyReport dailyReport, double nextTime = 0)
        {
            if (report.ContainsKey(crrtDay))
            {
                report[crrtDay] += dailyReport;
            }
            else
            {
                report.Add(crrtDay, dailyReport);
            }

            crrtDay = Convert.ToInt32(nextTime) / 1440 + 1;

            dailyReport = new DailyReport();
        }

        private void CalcTotals(Report report)
        {
            DailyReport prevDailyReport = new DailyReport();
            foreach (var day in report.Keys)
            {
                report[day].AddTotalsFrom(prevDailyReport);
                prevDailyReport = report[day];
            }
        }

        private void RunSimWithoutFlagMT(Report report)
        {
            int crrtDay;
            int prevISL;
            int mxmmISL;
            int prevWIA;

            DailyReport dailyReport = new DailyReport();

            foreach (var cohortKey in cohorts.Keys)
            {
                crrtDay = 1;
                prevISL = 0;
                mxmmISL = 0;
                prevWIA = 0;

                for (int i = 0; i < CIPs[cohortKey].Count; ++i)
                {
                    if (IsKIA(CIPs[cohortKey][i]))
                    {
                        dailyReport.WIA = dailyReport.NewWIA = 0;
                        dailyReport.KIA = dailyReport.NewKIA = cohorts[cohortKey];
                        dailyReport.FAT = dailyReport.NewFAT = cohorts[cohortKey];
                        Report(report, ref crrtDay, ref dailyReport);
                        break;
                    }
                    else
                    if (IsDOW(CIPs[cohortKey][i]))
                    {
                        dailyReport.WIA = dailyReport.NewWIA = 0;
                        dailyReport.DOW = dailyReport.NewDOW = cohorts[cohortKey];
                        dailyReport.FAT = dailyReport.NewFAT = cohorts[cohortKey];
                        Report(report, ref crrtDay, ref dailyReport);
                        break;
                    }
                    else
                    if (CIPs[cohortKey][i].level != prevISL)
                        if (prevISL == 0)
                        {
                            dailyReport.NewWIA = cohorts[cohortKey];
                            dailyReport.WIA    = cohorts[cohortKey];
                            mxmmISL = CIPs[cohortKey][i].level;
                        }
                        else
                        if (CIPs[cohortKey][i].level == 0)
                        {
                            if (prevWIA != 0) dailyReport.WIA = -cohorts[cohortKey];
                            else              dailyReport.WIA = dailyReport.NewWIA = 0;
                            switch (prevWIA)
                            {
                                case 1: dailyReport.WIA1 = -cohorts[cohortKey]; break;
                                case 2: dailyReport.WIA2 = -cohorts[cohortKey]; break;
                                case 3: dailyReport.WIA3 = -cohorts[cohortKey]; break;
                            }
                            dailyReport.NewRTD = cohorts[cohortKey];
                            dailyReport.RTD    = cohorts[cohortKey];
                            Report(report, ref crrtDay, ref dailyReport);
                            break;
                        }
                        else
                        if (CIPs[cohortKey][i].level > mxmmISL) mxmmISL = CIPs[cohortKey][i].level;

                    prevISL = CIPs[cohortKey][i].level;

                    if ((i < CIPs[cohortKey].Count - 1 && CIPs[cohortKey][i + 1].time > 1440 * crrtDay) || i == CIPs[cohortKey].Count - 1)
                    {
                        if (mxmmISL != 0)
                        {
                            switch (prevWIA)
                            {
                                case 1: dailyReport.WIA1 = -cohorts[cohortKey]; break;
                                case 2: dailyReport.WIA2 = -cohorts[cohortKey]; break;
                                case 3: dailyReport.WIA3 = -cohorts[cohortKey]; break;
                            }
                        }
                        switch (mxmmISL)
                        {
                            case 1: dailyReport.WIA1 = cohorts[cohortKey]; break;
                            case 2: dailyReport.WIA2 = cohorts[cohortKey]; break;
                            case 3: dailyReport.WIA3 = cohorts[cohortKey]; break;
                        }
                        if (i == CIPs[cohortKey].Count - 1) Report(report, ref crrtDay, ref dailyReport);
                        else                                Report(report, ref crrtDay, ref dailyReport, CIPs[cohortKey][i + 1].time);
                        prevISL = mxmmISL;
                        prevWIA = mxmmISL;
                        mxmmISL = 0;
                    }
                }
            }

            CalcTotals(report);
        }

        private void RunSimWithFlagMT(Report report)
        {
            throw new NotImplementedException();
        }

        public void RunSimulation(Report report)
        {
            if (methParams.FlagMT) RunSimWithFlagMT(report);
            else RunSimWithoutFlagMT(report);
        }

        #endregion
    }
}
