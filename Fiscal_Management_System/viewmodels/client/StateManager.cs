using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Fiscal_Management_System.viewmodels.client
{
    public class StateManager : ComboBoxManager<string>
    {
        public StateManager()
        {
            AllData = new List<string>()
            {
                "dolnośląskie", "kujawsko-pomorskie", "lubelskie", "lubuskie", "łódzkie", "małopolskie", "mazowieckie", "opolskie",
                "podkarpackie", "podlaskie", "pomorskie", "śląskie", "świętokrzyskie", "warmińsko-mazurskie", "wielkopolskie", "zachodniopomorskie"
            };
        }
    }
}
