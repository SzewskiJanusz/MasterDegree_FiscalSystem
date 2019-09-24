using Fiscal_Management_System.viewmodels;
using System.Windows;
using System.Windows.Markup;

namespace Fiscal_Management_System.views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Language = XmlLanguage.GetLanguage("pl-PL");
            this.DataContext = new MainViewModel();
        }
    }
}
