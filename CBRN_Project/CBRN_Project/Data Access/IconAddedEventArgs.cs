using CBRN_Project.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRN_Project.Data_Access
{
    /// <summary>
    /// Event arguments used by IconRepository's IconAdded event.
    /// </summary>
    public class IconAddedEventArgs : EventArgs
    {
        public Icon NewIcon { get; private set; }

        public IconAddedEventArgs(Icon newIcon)
        {
            this.NewIcon = newIcon;
        }
    }
}
