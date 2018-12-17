using System.Windows.Controls;
using CBRN_Project.MVVM.ViewModels;

namespace CBRN_Project.MVVM.Views
{
    public partial class EditView : Page
    {
        public EditView()
        {
            InitializeComponent();
            this.DataContext = new EditViewModel();
        }
    }
}
