using System;
using System.Collections.Generic;
using System.Text;

namespace CBRN_Project.MVVM.Models.Chemical
{
    using Pops = Dictionary<string, int>;

    class PopsUnit
    {
        #region Fields

        private readonly StringBuilder stringBuilder;

        private readonly string agent;
        private readonly List<string> chTypes;

        #endregion

        #region Constructors

        public PopsUnit(string agent, List<string> chTypes)
        {
            stringBuilder = new StringBuilder();

            this.agent = agent;
            this.chTypes = chTypes;

            if (chTypes.Count > 3)
            {
                throw new Exception("The number of challenges cannot be higher than 3.");
            }
        }

        #endregion

        #region Methods

        public Pops CalcPopsFor1ChTypes(Icon icon, Dictionary<string, double> pbties)
        {
            Pops pops = new Pops();

            foreach (var pbty in pbties)
            {
                pops.Add(pbty.Key, Convert.ToInt32(pbty.Value * icon.Personnel));
            }

            return pops;
        }

        public Pops CalcPopsFor2ChTypes(Icon icon, Dictionary<string, double> pbties)
        {
            Pops pops = new Pops();

            foreach (var pbty0 in pbties)
                if (pbty0.Key.Contains(chTypes[0]))
                    foreach (var pbty1 in pbties)
                        if (pbty1.Key.Contains(chTypes[1]))
                        {
                            stringBuilder.Clear();
                            stringBuilder.Append(pbty0.Key).Append(":").Append(pbty1.Key);

                            pops.Add(stringBuilder.ToString(), Convert.ToInt32(icon.Personnel * pbty0.Value * pbty1.Value));
                        }

            double pbtiesSum0 = 0;
            double pbtiesSum1 = 0;
            foreach (var pbty in pbties) if (pbty.Key.Contains(chTypes[0])) pbtiesSum0 += pbty.Value;
            foreach (var pbty in pbties) if (pbty.Key.Contains(chTypes[1])) pbtiesSum1 += pbty.Value;

            foreach (var pbty in pbties)
                if (pbty.Key.Contains(chTypes[0]))
                    pops.Add(pbty.Key, Convert.ToInt32(icon.Personnel * pbty.Value * (1 - pbtiesSum1)));

            foreach (var pbty in pbties)
                if (pbty.Key.Contains(chTypes[1]))
                    pops.Add(pbty.Key, Convert.ToInt32(icon.Personnel * pbty.Value * (1 - pbtiesSum0)));

            return pops;
        }

        public Pops CalcPopsFor3ChTypes(Icon icon, Dictionary<string, double> pbties)
        {
            throw new NotImplementedException();
        }

        public Pops CalcPops(Icon icon, Dictionary<string, double> pbties)
        {
            switch (chTypes.Count)
            {
                case 1: return CalcPopsFor1ChTypes(icon, pbties);
                case 2: return CalcPopsFor2ChTypes(icon, pbties);
                case 3: return CalcPopsFor3ChTypes(icon, pbties);
            }
            throw new Exception("The number of challenges cannot be higher than 3.");
        }

        #endregion
    }
}
