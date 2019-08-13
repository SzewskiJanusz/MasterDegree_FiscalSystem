using Fiscal_Management_System.viewmodels.client;
using System.Windows.Controls;

namespace Fiscal_Management_System.views.client
{
    /// <summary>
    /// Interaction logic for AllClients.xaml
    /// </summary>
    public partial class AllClients : UserControl
    {
        public AllClients()
        {
            InitializeComponent();
            this.DataContext = new ClientViewModel();
        }
    }
}
