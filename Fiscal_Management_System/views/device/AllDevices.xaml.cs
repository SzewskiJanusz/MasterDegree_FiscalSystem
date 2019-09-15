using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.place;
using Fiscal_Management_System.viewmodels.device;
using System.Windows.Controls;

namespace Fiscal_Management_System.views.device
{
    /// <summary>
    /// Interaction logic for AllDevices.xaml
    /// </summary>
    public partial class AllDevices : UserControl
    {
        public AllDevices()
        {
            InitializeComponent();
            this.DataContext = new DeviceViewModel();
        }

        public AllDevices(Client c)
        {
            InitializeComponent();
            this.DataContext = new DeviceViewModel(c);
        }

        public AllDevices(Client c, Place p)
        {
            InitializeComponent();
            this.DataContext = new DeviceViewModel(c, p);
        }
    }
}
