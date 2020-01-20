using Fiscal_Management_System.model;
using Fiscal_Management_System.views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using Application = System.Windows.Application;

namespace Fiscal_Management_System
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        private Serviceman LoggedUser;

        public LoginForm()
        {
            InitializeComponent();
            users.ItemsSource = GetUsersFromDB();
            users.SelectedIndex = 0;
        }

        private ObservableCollection<Serviceman> GetUsersFromDB()
        {
            ObservableCollection<Serviceman> allServicemen;

            using (var ctx = new FiscalDbContext())
            {
                allServicemen = new ObservableCollection<Serviceman>(ctx.Set<Serviceman>().ToList());
            }

            return allServicemen;
        }

        private void LoginUser(object sender, RoutedEventArgs e)
        {
            string username = users.Text;
            string salt;
            using (var ctx = new FiscalDbContext())
            {
                salt = ctx.Set<Serviceman>().
                    Where(x => x.Name + " " + x.Surname == username).
                    Select(x => x.Salt).
                    FirstOrDefault();
            }

            var hashedPassword = SHA256Hasher(passwd.Password, salt);

            if (IsPasswordValid(username, hashedPassword))
            {
                using (var ctx = new FiscalDbContext())
                {
                    LoggedUser = ctx.Set<Serviceman>().
                        Where(x => x.Name + " " + x.Surname == username).
                        FirstOrDefault();
                }

                GoToMainWindow(username);
            }
            else
            {
                // WriteLoginFailedMessage();
            }
        }

        [STAThread]
        private void GoToMainWindow(string usr)
        {
            Thread newWindowThread = new Thread(new ThreadStart(RunMainWindow));
            newWindowThread.SetApartmentState(ApartmentState.STA);
            newWindowThread.IsBackground = true;
            newWindowThread.Start();
            this.Hide();
        }

        private void RunMainWindow()
        {
            MainWindow mw = new MainWindow(LoggedUser, this);
            mw.Show();
            System.Windows.Threading.Dispatcher.Run();
        }

        private bool IsPasswordValid(string username, string hashpwd)
        {
            string hashtOfUser;
            using (var ctx = new FiscalDbContext())
            {
                hashtOfUser = ctx.Set<Serviceman>().
                    Where(x => x.Name + " " + x.Surname == username).
                    Select(x => x.Hash).
                    FirstOrDefault();
            }
            return hashtOfUser == hashpwd;
        }

        private string SHA256Hasher(string passwd, string salt)
        {
            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(passwd + salt), 0, Encoding.UTF8.GetByteCount(passwd + salt));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        public int CloseThisWindow(int s)
        {
            this.Close();
            return 1;
        }
    }
}
