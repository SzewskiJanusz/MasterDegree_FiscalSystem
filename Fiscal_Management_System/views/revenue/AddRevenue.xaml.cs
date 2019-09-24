using System.Windows;
using Fiscal_Management_System.viewmodels.revenue;

namespace Fiscal_Management_System.views.revenue
{
    /// <summary>
    /// Interaction logic for AddRevenue.xaml
    /// </summary>
    public partial class AddRevenue : Window
    {
        public AddRevenue()
        {
            InitializeComponent();
            this.DataContext = new RevenueAddViewModel();
        }

        public void closeWindow(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
