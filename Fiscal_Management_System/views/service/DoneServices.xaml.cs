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
using Fiscal_Management_System.viewmodels.service;

namespace Fiscal_Management_System.views.service
{
    /// <summary>
    /// Interaction logic for DoneServices.xaml
    /// </summary>
    public partial class DoneServices : UserControl
    {
        public DoneServices(Func<UserControl, int> ucswitcher)
        {
            InitializeComponent();
            this.DataContext = new DoneServicesViewModel(ucswitcher);
        }
    }
}
