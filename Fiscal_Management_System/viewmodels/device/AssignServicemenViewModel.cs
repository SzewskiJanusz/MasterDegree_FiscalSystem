using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.device;
using Fiscal_Management_System.model.place;
using Fiscal_Management_System.views.device;

namespace Fiscal_Management_System.viewmodels.device
{
    public class AssignServicemenViewModel : INotifyPropertyChanged
    {
        public IDbContext Context;

        private ObservableCollection<string> _allServicemen;
        public ObservableCollection<string> AllServicemen
        {
            get => _allServicemen;
            set { _allServicemen = value; OnPropertyChanged("AllServicemen");  }
        }

        private ObservableCollection<string> _chosenServicemen;
        public ObservableCollection<string> ChosenServicemen
        {
            get => _chosenServicemen;
            set { _chosenServicemen = value; OnPropertyChanged("ChosenServicemen"); }
        }

        private ICommand _moveRightButtonCommand;
        public ICommand MoveRightButtonCommand
        {
            get
            {
                _moveRightButtonCommand = new RelayCommand(o =>
                {
                    string a = (string) o;
                    if (string.IsNullOrEmpty(a)) return;
                    ChosenServicemen.Add(a);
                    AllServicemen.Remove(a);
                }, o => true);

                return _moveRightButtonCommand;
            }
        }

        private ICommand _moveLeftButtonCommand;
        public ICommand MoveLeftButtonCommand
        {
            get
            {
                _moveLeftButtonCommand = new RelayCommand(o =>
                {
                    string a = (string)o;
                    if (string.IsNullOrEmpty(a)) return;
                    AllServicemen.Add(a);
                    ChosenServicemen.Remove(a);
                }, o => true);

                return _moveLeftButtonCommand;
            }
        }


        public AssignServicemenViewModel()
        {
            ChosenServicemen = new ObservableCollection<string>();
            AllServicemen = GetAllServicemen();
        }

        public AssignServicemenViewModel(IDbContext context)
        {
            ChosenServicemen = new ObservableCollection<string>();
            AllServicemen = new ObservableCollection<string>();
            Context = context;
        }

        private ObservableCollection<string> GetAllServicemen()
        {
            ObservableCollection<string> servicemen;
            using (var ctx = (Context == null ? new FiscalDbContext() : Context))
            {
                servicemen = new ObservableCollection<string>(ctx.Set<Serviceman>().Select(x=>x.Name + " " + x.Surname).ToList());
            }
            return servicemen;
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
