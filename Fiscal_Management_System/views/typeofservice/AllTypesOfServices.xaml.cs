using System;
using System.Windows.Controls;
using Fiscal_Management_System.viewmodels.devicemodel;
using Fiscal_Management_System.viewmodels.typeofservice;

namespace Fiscal_Management_System.views.typeofservice
{
    /// <summary>
    /// Interaction logic for AllTypesOfServices.xaml
    /// </summary>
    public partial class AllTypesOfServices : UserControl
    {
        public AllTypesOfServices()
        {
            InitializeComponent();
        }

        public AllTypesOfServices(Func<UserControl, int> ucSetMethod)
        {
            InitializeComponent();
            this.DataContext = new TypesOfServicesViewModel(ucSetMethod);
        }
    }
}
