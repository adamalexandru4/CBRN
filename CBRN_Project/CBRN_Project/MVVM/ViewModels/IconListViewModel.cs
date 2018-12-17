using CBRN_Project.Data_Access;
using CBRN_Project.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CBRN_Project.MVVM.ViewModels
{
    public class IconListViewModel : ViewModelBase
    {
        #region Fields

        readonly IconRepository iconRepository;

        public double TotalIcons {
            get
            {
                return IconsList.Count;
            }
        }

        public Icon SelectedIcon { get; set; }

        public ObservableCollection<Icon> IconsList { get; private set; }

        #endregion 

        public IconListViewModel(IconRepository newIconRepository)
        {
            IconsList = new ObservableCollection<Icon>();

            this.iconRepository = newIconRepository;
            base.DisplayName = "List";

            // Subscribe for notifications of when a new icon is saved.
            iconRepository.IconAdded += this.OnIconAddedToRepository;

            //iconRepository.IconRemoved += this.OnIconRemovedFromRepository;
        }

        void OnIconAddedToRepository(object sender, IconAddedEventArgs e)
        {
            var icon = new Icon(IconRepository.IconId);
            IconsList.Add(icon);
            
            this.OnPropertyChanged("TotalIcons");
        }

        public void RemoveIcon()
        {
            if (SelectedIcon != null)
            {
                iconRepository.RemoveIcon(SelectedIcon);
                IconsList.Remove(SelectedIcon);

                this.OnPropertyChanged("TotalIcons");
            }
        }

        /*public void OnIconRemovedFromRepository(object sender, IconRemovedEventArgs e)
        {
            if (SelectedIcon != null)
            {
                IconsList.Remove(SelectedIcon);
                
                this.OnPropertyChanged("TotalIcons");
            }
        }*/
    }
}
