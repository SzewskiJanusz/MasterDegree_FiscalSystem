using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Fiscal_Management_System.viewmodels
{
    public class ComboBoxManager<T> : INotifyPropertyChanged
    {
        private List<T> _allData;
        public List<T> AllData { get { return _allData; } set { _allData = value; } }

        public ComboBoxManager(){ }

        #region INotifyPropertyChanged things
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
