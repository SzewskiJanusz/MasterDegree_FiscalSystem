using System.Windows;
using Fiscal_Management_System.model.service;
using Fiscal_Management_System.viewmodels.service;

namespace Fiscal_Management_System.views.service
{
    /// <summary>
    /// Interaction logic for SetAsDone.xaml
    /// </summary>
    public partial class SetAsDone : Window
    {
        public SetAsDone()
        {
            InitializeComponent();
        }

        public SetAsDone(Service s)
        {
            InitializeComponent();
            this.DataContext = new SetServiceAsDoneViewModel(s);
        }

        public void closeWindow(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
