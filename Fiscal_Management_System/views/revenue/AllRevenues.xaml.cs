using System;
using System.Windows;
using System.Windows.Controls;
using Fiscal_Management_System.viewmodels.revenue;

namespace Fiscal_Management_System.views.revenue
{
    /// <summary>
    /// Interaction logic for AllRevenues.xaml
    /// </summary>
    public partial class AllRevenues : UserControl
    {
        public AllRevenues()
        {
            InitializeComponent();
        }

        public AllRevenues(Func<UserControl, int> ucSetMethod)
        {
            InitializeComponent();
            this.DataContext = new RevenueViewModel(ucSetMethod);
        }
    }
}
