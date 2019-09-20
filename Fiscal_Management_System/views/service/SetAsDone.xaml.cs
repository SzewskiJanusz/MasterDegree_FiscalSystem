using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
    }
}
