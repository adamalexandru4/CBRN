using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using CBRN_Project.Data_Access;

namespace CBRN_Project.MVVM.Models.Chemical
{
    using CIPs = Dictionary<string, Dictionary<double, double>>;
    
    class CIPsUnit
    {
        #region Fields

        private readonly StringBuilder stringBuilder;

        private readonly DataService dataService;

        private readonly string agent;
        private readonly List<string> chTypes;

        #endregion

        #region Constructors

        public CIPsUnit(DataService dataService, string agent, List<string> chTypes)
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

        public void Init(CIPs CIPs)
        {
            DataTable ipTable;
            List<DataTable> ipTables = new List<DataTable>();

            Dictionary<double, double> timeLvlPair;
            foreach (var chType in chTypes)
            {
                stringBuilder.Clear();
                stringBuilder.Append(agent).Append(' ').Append(chType).Append(" IP");

                ipTable = dataService.GetTable(stringBuilder.ToString());
                if (ipTable != null)
                {
                    ipTables.Add(ipTable);
                }
                else
                {
                    throw new Exception($"Cannot find the IP table in the database for the {agent} - {chType} pair.");
                }
                
                for (int i = 1; i < ipTable.Columns.Count; ++i)
                {
                    stringBuilder.Clear();
                    stringBuilder
                        .Append(agent).Append(':').Append(chType).Append(':').Append(ipTable.Columns[i].ColumnName);

                    timeLvlPair = new Dictionary<double, double>();
                    foreach (DataRow row in ipTable.Rows)
                    {
                        timeLvlPair.Add((double)row[ipTable.Columns[0].ColumnName], (double)row[ipTable.Columns[i].ColumnName]);
                    }

                    CIPs.Add(stringBuilder.ToString(), timeLvlPair);
                }
            }

            if (chTypes.Count > 1)
            {
                for (int i = 1; i < ipTables[0].Columns.Count; ++i)
                    for (int j = 1; j < ipTables[1].Columns.Count; ++j)
                    {
                        stringBuilder.Clear();
                        stringBuilder
                            .Append(agent).Append(':').Append(chTypes[0]).Append(':').Append(ipTables[0].Columns[i].ColumnName)
                            .Append("::")
                            .Append(agent).Append(':').Append(chTypes[1]).Append(':').Append(ipTables[1].Columns[j].ColumnName);

                        timeLvlPair = new Dictionary<double, double>();
                        foreach (DataRow row in ipTables[0].Rows)
                        {
                            timeLvlPair.Add((double)row[ipTables[0].Columns[0].ColumnName], (double)row[ipTables[0].Columns[i].ColumnName]);
                        }
                        foreach (DataRow row in ipTables[1].Rows)
                        {
                            if (timeLvlPair.ContainsKey((double)row[ipTables[1].Columns[0].ColumnName]) &&
                                timeLvlPair[(double)row[ipTables[1].Columns[0].ColumnName]] < (double)row[ipTables[1].Columns[j].ColumnName])
                            {
                                timeLvlPair[(double)row[ipTables[1].Columns[0].ColumnName]] = (double)row[ipTables[1].Columns[j].ColumnName];
                            }
                            else
                            {
                                timeLvlPair.Add((double)row[ipTables[1].Columns[0].ColumnName], (double)row[ipTables[1].Columns[j].ColumnName]);
                            }
                        }

                        CIPs.Add(stringBuilder.ToString(), timeLvlPair);
                    }
            }

            if (chTypes.Count == 3)
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
