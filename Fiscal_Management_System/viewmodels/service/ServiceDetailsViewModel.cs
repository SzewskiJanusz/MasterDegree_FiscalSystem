using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Fiscal_Management_System.model.service;
using Fiscal_Management_System.views.service;

namespace Fiscal_Management_System.viewmodels.service
{
    public class ServiceDetailsViewModel
    {
        public Service Service{ get; set; }

        private ICommand _goToSetAsDoneButtonCommand;
        public ICommand GoToSetAsDoneButtonCommand
        {
            get
            {
                _goToSetAsDoneButtonCommand = new RelayCommand(o =>
                {
                    Service serv = new Service((Service)o);
                    SetAsDone setasdone_window = new SetAsDone(serv);
                    if ((bool)setasdone_window.ShowDialog())
                    {
                        MessageBox.Show("Dodano urządzenie!");
                    }

                }, o => true);

                return _goToSetAsDoneButtonCommand;
            }
        }

        public ServiceDetailsViewModel(Service s)
        {
            this.Service = s;
        }
    }
}
