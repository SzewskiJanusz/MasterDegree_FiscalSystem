using System.Windows;
using Fiscal_Management_System.viewmodels.device;

namespace Fiscal_Management_System.views.device
{
    /// <summary>
    /// Interaction logic for AssignServicemen.xaml
    /// </summary>
    public partial class AssignServicemen : Window
    {
        public AssignServicemen()
        {
            InitializeComponent();
            this.DataContext = new AssignServicemenViewModel();
        }

        public void closeWindow(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
