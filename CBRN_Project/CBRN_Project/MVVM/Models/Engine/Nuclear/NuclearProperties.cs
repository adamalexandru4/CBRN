using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRN_Project.MVVM.Models.Engine.Nuclear
{
    public class NuclearProperties
    {
        //Initial Whole-Body Radiation
        public double NucWholeBody          { get; set; }

        //Blast
        public double NucBlast              { get; set; }

        //Thermal Fluence
        public double NucThermal            { get; set; }

        //APF
        public double APFN                  { get; set; }
        public double APFY                  { get; set; }
        public double APFBlast              { get; set; }
        public double APFThermal            { get; set; }

        //Uniform
        public double UniformThermal        { get; set; }

        //Ranges
        public int RangeBl                  { get; set; }
        public int RangeWB                  { get; set; }
        public int RangeTh                  { get; set; }

        //State for Icons
        public List<int> newDOW ;
        public List<int> newWIA ;
        public List<int> newCONV;
        public List<int> newRTD ;
        public List<int> newKIA ;

        #region Methods
        public NuclearProperties()
        {
            SetLists();
        }

        void SetLists()
        {
            newDOW = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            newWIA = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            newCONV = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            newRTD = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            newKIA = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        }
        #endregion
    }
}
