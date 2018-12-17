using CBRN_Project.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRN_Project.Data_Access
{
    public class IconRemovedEventArgs : EventArgs
    {
        public Icon removedIcon { get; private set; }

        public IconRemovedEventArgs(Icon icon)
        {
            removedIcon = icon;
        }
    }
}
