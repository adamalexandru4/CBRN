using CBRN_Project.Data_Access;
using CBRN_Project.MVVM.ViewModels;
using CBRN_Project.MVVM.Views;
using CBRN_Project.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CBRN_Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow = new MainWindow();

            IDialogService dialogService = new DialogService(MainWindow);
            dialogService.Register<IPEDialogViewModel, DialogWindow>();
            dialogService.Register<VehicleShelterDialogViewModel, VehicleShelterDialogWindow>();

            ApplicationViewModel mainViewModel = new ApplicationViewModel(dialogService);

            EventHandler handler = null;
            handler = delegate
            {
                mainViewModel.RequestClose -= handler;
                MainWindow.Close();
            };

            mainViewModel.RequestClose += handler;

            MainWindow.DataContext = mainViewModel;
            MainWindow.Show();
        }
    }
}
