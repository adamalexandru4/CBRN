using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using CBRN_Project.Data_Access;
using MathNet.Numerics.Distributions;

namespace CBRN_Project.MVVM.Models.Chemical
{
    using Pbties = Dictionary<string, double>;

    class PbtiesUnit
    {
        #region Fields

        private readonly StringBuilder stringBuilder;

        private readonly DataService dataService;

        private readonly string agent;
        private readonly List<string> chTypes;

        #endregion

        #region Constructors

        public PbtiesUnit(DataService dataService, string agent, List<string> chTypes)
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

        public double CalcPbty(Icon icon, string chType, DataRow row)
        {
            switch (agent)
            {
                case "CG":
                case "CK":
                    {
                        throw new NotImplementedException();
                    }
                default:
                    {
                        return new Normal(0, 1).CumulativeDistribution(
                            row.Field<double>("ProbitSlope") *
                            Math.Log10(
                                icon.EffChallenges.Find(effCh => effCh.ChallengeType == chType).Value /
                                row.Field<double>("ECt50")));
                    }
            }

            throw new Exception($"{agent} is not a valid agent.");
        }

        public Pbties CalcPbties(Icon icon)
        {
            Pbties pbties = new Pbties();

            DataTable tpsTable;
            foreach (var chType in chTypes)
            {
                stringBuilder.Clear();
                stringBuilder.Append(agent).Append('_').Append(chType).Append("_TPS");

                tpsTable = dataService.GetTable(stringBuilder.ToString());
                if (tpsTable == null)
                {
                    throw new Exception($"Cannot find the TPS table in the database for the {agent} - {chType} pair.");
                }

                double prevPbty = 0;
                for (int i = tpsTable.Rows.Count - 1; i >= 0 ; --i)
                {
                    stringBuilder.Clear();
                    stringBuilder
                        .Append(agent)
                        .Append('_')
                        .Append(chType)
                        .Append('_')
                        .Append(tpsTable.Rows[i].Field<string>("InjuryProfileLabel"));

                    pbties.Add(stringBuilder.ToString(), - prevPbty + (prevPbty = CalcPbty(icon, chType, tpsTable.Rows[i])));
                }
            }

            return pbties;
        }

        #endregion
    }
}
