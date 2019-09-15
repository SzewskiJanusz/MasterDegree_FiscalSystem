using System;
using System.Windows.Controls;
using Fiscal_Management_System.viewmodels.service;

namespace Fiscal_Management_System.views.service
{
    /// <summary>
    /// Interaction logic for AllServices.xaml
    /// </summary>
    public partial class AllServices : UserControl
    {
        public AllServices(Func<UserControl, int> ucSetMethod)
        {
            InitializeComponent();
            this.DataContext = new ServicesViewModel(ucSetMethod);
        }
    }
}
