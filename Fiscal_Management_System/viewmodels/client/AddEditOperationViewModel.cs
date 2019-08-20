using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Fiscal_Management_System.viewmodels.client
{
    public abstract class AddEditOperationViewModel<T> : INotifyPropertyChanged
    {
        /// <summary>
        /// Abstract method. Override with add/edit implementation
        /// </summary>
        public abstract void Operation(T entity);

        /// <summary>
        /// Command of confirmation button
        /// </summary>
        private ICommand _confirmButtonCommand;
        public ICommand ConfirmButtonCommand
        {
            get
            {
                _confirmButtonCommand = new RelayCommand(o =>
                {
                    Operation((T)o);
                }, o => true);

                return _confirmButtonCommand;
            }
        }

        /// <summary>
        /// Client for addition/edition
        /// </summary>
        private T _entity;
        public T Entity
        {
            get { return _entity; }
            set { _entity = value; OnPropertyChanged("Entity"); }
        }

        /// <summary>
        /// Since add and edit operate on one view, button text and window title are set dynamically
        /// </summary>
        public string ButtonText { get; set; }
        /// <summary>
        /// Since add and edit operate on one view, button text and window title are set dynamically
        /// </summary>
        public string WindowTitle { get; set; }

        public AddEditOperationViewModel()
        { }

        #region INotifyPropertyChanged things
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
