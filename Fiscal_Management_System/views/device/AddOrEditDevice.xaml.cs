using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.device;
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
    /// Interaction logic for AddOrEditDevice.xaml
    /// </summary>
    public partial class AddOrEditDevice : Window
    {
        public AddOrEditDevice(Device c)
        {
            InitializeComponent();
            this.DataContext = new DeviceEditViewModel(c);
        }

        public AddOrEditDevice(Client c)
        {
            InitializeComponent();
            this.DataContext = new DeviceAddViewModel(c);
        }

        public AddOrEditDevice(Client c, Place p)
        {
            InitializeComponent();
            this.DataContext = new DeviceAddViewModel(c, p);
        }

        public AddOrEditDevice(Device d, Client c, Place p)
        {
            InitializeComponent();
            this.DataContext = new DeviceEditViewModel(d, c, p);
        }

        public void closeWindow(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
