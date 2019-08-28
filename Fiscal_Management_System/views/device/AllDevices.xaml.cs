using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.place;
using Fiscal_Management_System.viewmodels.device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
