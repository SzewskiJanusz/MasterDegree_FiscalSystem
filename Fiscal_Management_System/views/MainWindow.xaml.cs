using Fiscal_Management_System.model;
using Fiscal_Management_System.viewmodels;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;

namespace Fiscal_Management_System.views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Window loginWindow;

        public MainWindow(Serviceman user, Window w)
        {
            InitializeComponent();
            loginWindow = w;
            this.Language = XmlLanguage.GetLanguage("pl-PL");
            this.Closed += CloseLoginWindow;
            this.DataContext = new MainViewModel(user);
        }

        private void CloseLoginWindow(object sender, System.EventArgs e)
        {
            try
            {
                if (loginWindow.Dispatcher.CheckAccess())
                    loginWindow.Close();
                else
                    loginWindow.Dispatcher.Invoke(DispatcherPriority.Normal, new ThreadStart(loginWindow.Close));
            }
            catch (Exception) { }
        }
    }
}
