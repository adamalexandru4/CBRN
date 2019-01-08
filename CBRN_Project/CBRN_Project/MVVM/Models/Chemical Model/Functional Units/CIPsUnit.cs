using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using CBRN_Project.Data_Access;

namespace CBRN_Project.MVVM.Models.Chemical
{
    using CIPs = Dictionary<string, List<(double, int)>>;
    
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

        private void MakeCIPsFor1ChTypes(List<string> keys, CIPs CIPs)
        {
            stringBuilder.Clear();
            stringBuilder.Append(keys[0].Substring(0, keys[0].LastIndexOf('_'))).Append("_IP");

            DataTable ipTable = dataService.GetTable(stringBuilder.ToString());


            string effect = keys[0].Substring(keys[0].LastIndexOf('_') + 1);

            List<(double, int)> timeLvlPairs = new List<(double, int)>();
            foreach (DataRow row in ipTable.Rows)
            {
                timeLvlPairs.Add((
                    Convert.ToDouble(row[0]),
                    Convert.ToInt32 (row[effect])
                    ));
            }


            CIPs.Add(keys[0], timeLvlPairs);
        }

        private void MakeCIPsFor2ChTypes(List<string> keys, CIPs CIPs)
        {
            DataTable       ipTable;
            List<DataTable> ipTables = new List<DataTable>();

            foreach (var key in keys)
            {
                stringBuilder.Clear();
                stringBuilder.Append(key.Substring(0, key.LastIndexOf('_'))).Append("_IP");

                ipTable = dataService.GetTable(stringBuilder.ToString());
                ipTables.Add(ipTable);
            }


            string effect0 = keys[0].Substring(keys[0].LastIndexOf('_') + 1);
            string effect1 = keys[1].Substring(keys[1].LastIndexOf('_') + 1);

            List<(double, int)> timeLvlPairs = new List<(double, int)>();
            int i0 = 0;
            int i1 = 0;
            while (i0 < ipTables[0].Rows.Count && i1 < ipTables[1].Rows.Count)
            {
                if (Convert.ToDouble(ipTables[0].Rows[i0][0]) <
                    Convert.ToDouble(ipTables[1].Rows[i1][0]))
                {
                    timeLvlPairs.Add((
                        Convert.ToDouble(ipTables[0].Rows[i0][0]),
                        Convert.ToInt32 (ipTables[0].Rows[i0][effect0])
                        ));
                    ++i0;
                }
                else
                if (Convert.ToDouble(ipTables[0].Rows[i0][0]) >
                    Convert.ToDouble(ipTables[1].Rows[i1][0]))
                {
                    timeLvlPairs.Add((
                        Convert.ToDouble(ipTables[1].Rows[i1][0]),
                        Convert.ToInt32 (ipTables[1].Rows[i1][effect1])
                        ));
                    ++i1;
                }
                else
                {
                    if (Convert.ToInt32(ipTables[0].Rows[i0][effect0]) <
                        Convert.ToInt32(ipTables[1].Rows[i1][effect1]))
                    {
                        timeLvlPairs.Add((
                            Convert.ToDouble(ipTables[1].Rows[i1][0]),
                            Convert.ToInt32 (ipTables[1].Rows[i1][effect1])
                            ));
                    }
                    else
                    {
                        timeLvlPairs.Add((
                            Convert.ToDouble(ipTables[0].Rows[i0][0]),
                            Convert.ToInt32 (ipTables[0].Rows[i0][effect0])
                            ));
                    }
                    ++i0;
                    ++i1;
                }
            }
            while (i0 < ipTables[0].Rows.Count)
            {
                timeLvlPairs.Add((
                        Convert.ToDouble(ipTables[0].Rows[i0][0]),
                        Convert.ToInt32 (ipTables[0].Rows[i0][effect0])
                        ));
                ++i0;
            }
            while (i1 < ipTables[1].Rows.Count)
            {
                timeLvlPairs.Add((
                        Convert.ToDouble(ipTables[1].Rows[i1][0]),
                        Convert.ToInt32 (ipTables[1].Rows[i1][effect1])
                        ));
                ++i1;
            }


            CIPs.Add(keys[0] + ":" + keys[1], timeLvlPairs);
        }

        private void MakeCIPsFor3ChTypes(List<string> keys, CIPs CIPs)
        {
            throw new NotImplementedException();
        }

        public void Init(Dictionary<string, int> cohorts, CIPs CIPs)
        {
            foreach (var cohortKey in cohorts.Keys)
            {
                List<string> keys = new List<string>(cohortKey.Split(new char[] { ':' }));

                switch (keys.Count)
                {
                    case 1: MakeCIPsFor1ChTypes(keys, CIPs); break;
                    case 2: MakeCIPsFor2ChTypes(keys, CIPs); break;
                    case 3: MakeCIPsFor3ChTypes(keys, CIPs); break;
                }
            }
        }

        #endregion
    }
}
