using System.Windows;
using Fiscal_Management_System.viewmodels.typeofservice;

namespace Fiscal_Management_System.views.typeofservice
{
    /// <summary>
    /// Interaction logic for AddTypeOfService.xaml
    /// </summary>
    public partial class AddTypeOfService : Window
    {
        public AddTypeOfService()
        {
            InitializeComponent();
            this.DataContext = new TypeOfServiceAddViewModel();
        }

        public void closeWindow(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
