using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using CBRN_Project.Data_Access;

namespace CBRN_Project.MVVM.Models.Chemical
{
    using Cohorts = Dictionary<string, int>;

    class CohortsUnit
    {
        #region Fields

        private readonly StringBuilder stringBuilder;

        private readonly DataService dataService;

        private readonly string agent;
        private readonly List<string> chTypes;

        #endregion

        #region Constructors

        public CohortsUnit(DataService dataService, string agent, List<string> chTypes)
        {
            stringBuilder = new StringBuilder();

            this.dataService = dataService;

            this.agent = agent;
            this.chTypes = chTypes;

            if (chTypes.Count > 3)
            {
                throw new Exception("The number of challenges cannot be higher than 3.");
            }
        }

        #endregion

        #region Methods

        public void Init(Cohorts cohorts)
        {
            DataTable       tpsTable;
            List<DataTable> tpsTables = new List<DataTable>();

            foreach (var chType in chTypes)
            {
                stringBuilder.Clear();
                stringBuilder.Append(agent).Append('_').Append(chType).Append("_TPS");

                tpsTable = dataService.GetTable(stringBuilder.ToString());
                if (tpsTable != null)
                {
                    tpsTables.Add(tpsTable);
                }
                else throw new Exception($"Cannot find the TPS table in the database for the {agent} - {chType} pair.");

                foreach (DataRow row in tpsTable.Rows)
                {
                    stringBuilder.Clear();
                    stringBuilder
                        .Append(agent).Append('_').Append(chType).Append('_').Append(row.Field<string>("InjuryProfileLabel"));

                    cohorts.Add(stringBuilder.ToString(), 0);
                }
            }

            if (chTypes.Count > 1)
            {
                foreach (DataRow row0 in tpsTables[0].Rows)
                    foreach (DataRow row1 in tpsTables[1].Rows)
                    {
                        stringBuilder.Clear();
                        stringBuilder
                            .Append(agent).Append('_').Append(chTypes[0]).Append('_').Append(row0.Field<string>("InjuryProfileLabel"))
                            .Append(":")
                            .Append(agent).Append('_').Append(chTypes[1]).Append('_').Append(row1.Field<string>("InjuryProfileLabel"));

                        cohorts.Add(stringBuilder.ToString(), 0);
                    }
            }

            if (chTypes.Count == 3)
            {
                foreach (DataRow row0 in tpsTables[0].Rows)
                    foreach (DataRow row1 in tpsTables[1].Rows)
                        foreach (DataRow row2 in tpsTables[2].Rows)
                        {
                            stringBuilder.Clear();
                            stringBuilder
                                .Append(agent).Append('_').Append(chTypes[0]).Append('_').Append(row0.Field<string>("InjuryProfileLabel"))
                                .Append(":")
                                .Append(agent).Append('_').Append(chTypes[1]).Append('_').Append(row1.Field<string>("InjuryProfileLabel"))
                                .Append(":")
                                .Append(agent).Append('_').Append(chTypes[2]).Append('_').Append(row2.Field<string>("InjuryProfileLabel"));

                            cohorts.Add(stringBuilder.ToString(), 0);
                        }
            }
        }

        public void CalcCohorts(List<ChemExIcon> exIcons, Cohorts cohorts)
        {
            foreach (var exIcon in exIcons)
            {
                foreach (var pop in exIcon.Pops)
                {
                    cohorts[pop.Key] += pop.Value;
                }
            }
        }

        public void RemoveNulls(Cohorts cohorts)
        {
            List<string> keysToBeRemoved = new List<string>();

            foreach (var cohortKey in cohorts.Keys)
            {
                if (cohorts[cohortKey] == 0)
                {
                    keysToBeRemoved.Add(cohortKey);
                }
            }

            foreach (var keyToBeRemoved in keysToBeRemoved)
            {
                cohorts.Remove(keyToBeRemoved);
            }
        }

        #endregion
    }
}
