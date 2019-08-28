using Fiscal_Management_System.model.client;
using Fiscal_Management_System.viewmodels.client;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Fiscal_Management_System.views.client
{
    /// <summary>
    /// Interaction logic for AllClients.xaml
    /// </summary>
    public partial class AllClients : UserControl
    {
        public AllClients(Func<UserControl, int> ucSetMethod)
        {
            InitializeComponent();
            this.DataContext = new ClientViewModel(ucSetMethod);
        }
    }
}
