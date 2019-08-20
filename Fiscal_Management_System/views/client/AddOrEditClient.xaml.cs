using Fiscal_Management_System.model.client;
using Fiscal_Management_System.viewmodels.client;
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

namespace Fiscal_Management_System.views.client
{
    /// <summary>
    /// Interaction logic for AddOrEditClient.xaml
    /// </summary>
    public partial class AddOrEditClient : Window
    {
        public AddOrEditClient()
        {
            InitializeComponent();
            this.DataContext = new ClientAddViewModel();
        }

        public AddOrEditClient(Client c)
        {
            InitializeComponent();
            this.DataContext = new ClientEditViewModel(c);
        }

        public void closeWindow(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
