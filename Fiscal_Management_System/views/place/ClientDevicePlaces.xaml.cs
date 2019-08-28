using Fiscal_Management_System.model.client;
using Fiscal_Management_System.viewmodels.place;
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

namespace Fiscal_Management_System.views.place
{
    /// <summary>
    /// Interaction logic for ClientDevicePlaces.xaml
    /// </summary>
    public partial class ClientDevicePlaces : UserControl
    {
        public ClientDevicePlaces(Client c, Func<UserControl, int> userControlSwitcher)
        {
            InitializeComponent();
            this.DataContext = new PlaceViewModel(c, userControlSwitcher);
        }
    }
}
