﻿using System;
using System.Windows.Controls;
using Fiscal_Management_System.model;
using Fiscal_Management_System.viewmodels.service;

namespace Fiscal_Management_System.views.service
{
    /// <summary>
    /// Interaction logic for PlannedServices.xaml
    /// </summary>
    public partial class PlannedServices : UserControl
    {
        public PlannedServices(Func<UserControl, int> ucSetMethod, Serviceman serviceman)
        {
            InitializeComponent();
            this.DataContext = new PlannedServicesViewModel(ucSetMethod, serviceman);
        }
    }
}
