using CBRN_Project.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRN_Project.Data_Access
{
    public class IconRepository
    {
        #region Fields

        readonly List<Icon> icons;

        public static int IconId { get; set; } = 1;
        public static int NumberOfIcons { get; set; }

        #endregion

        #region Constructor

        public IconRepository()
        {
            icons = new List<Icon>();
        }

        #endregion

        #region Public Interface

        public event EventHandler<IconAddedEventArgs> IconAdded;

        public void AddIcon(Icon icon)
        {
            if (icon == null)
                throw new ArgumentNullException("icon");

            if(!icons.Contains(icon))
            {
                icons.Add(icon);
                NumberOfIcons = icons.Count;
                IconAdded?.Invoke(this, new IconAddedEventArgs(icon));
                IconId++;
            }
        }

        public event EventHandler<IconRemovedEventArgs> IconRemoved;

        public void RemoveIcon(Icon icon)
        {
            if (icon == null)
                throw new ArgumentNullException("icon");

            if(icons.Exists(param => param.Name == icon.Name))
            {
                icons.Remove(icon);
                IconRemoved?.Invoke(this, new IconRemovedEventArgs(icon));
                NumberOfIcons = icons.Count;
            }
        }

        public bool ContainsIcon(Icon icon)
        {
            if (icon == null)
                throw new ArgumentNullException("icon");

            return icons.Contains(icon);
        }

        public List<Icon> GetIcons()
        {
            return new List<Icon>(icons);
        }
        #endregion
        
    }
}
