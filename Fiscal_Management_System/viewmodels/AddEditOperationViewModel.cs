using Fiscal_Management_System.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Fiscal_Management_System.viewmodels
{
    public abstract class AddEditOperationViewModel<T> : INotifyPropertyChanged
    {
        /// <summary>
        /// Context of EF
        /// </summary>
        public IDbContext Context;

        /// <summary>
        /// Abstract method. Override with add/edit implementation
        /// </summary>
        public abstract void OperateOnDatabase(T entity);

        public void Operation(T entity)
        {
            if (Context == null)
                Context = new FiscalDbContext();

            OperateOnDatabase(entity);
            Context.SaveChanges();
        }
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

        public AddEditOperationViewModel(FiscalDbContext context)
        {
            Context = context;
        }

        #region INotifyPropertyChanged things
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
