using System;
using System.Windows.Controls;
using Fiscal_Management_System.viewmodels.devicemodel;
using Fiscal_Management_System.viewmodels.revenue;

namespace Fiscal_Management_System.views.devicemodel
{
    /// <summary>
    /// Interaction logic for AllDeviceModels.xaml
    /// </summary>
    public partial class AllDeviceModels : UserControl
    {
        public AllDeviceModels()
        {
            InitializeComponent();
        }

        public AllDeviceModels(Func<UserControl, int> ucSetMethod)
        {
            InitializeComponent();
            this.DataContext = new DeviceModelViewModel(ucSetMethod);
        }
    }
}
