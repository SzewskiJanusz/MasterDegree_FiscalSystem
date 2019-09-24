using System.Windows;
using Fiscal_Management_System.viewmodels.devicemodel;

namespace Fiscal_Management_System.views.devicemodel
{
    /// <summary>
    /// Interaction logic for AddDeviceModel.xaml
    /// </summary>
    public partial class AddDeviceModel : Window
    {
        public AddDeviceModel()
        {
            InitializeComponent();
            this.DataContext = new DeviceModelAddViewModel();
        }

        public void closeWindow(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
