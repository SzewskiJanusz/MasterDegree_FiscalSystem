using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Fiscal_Management_System.model
{
    public class EntitySearcher<T> : INotifyPropertyChanged
    {

        public EntitySearcher(){}

        public EntitySearcher(ObservableCollection<T> col)
        {
            Collection = col;
            FilteredCollection = col;
        }

        public ObservableCollection<T> Collection
        {
            get;
            set;
        }

        private ObservableCollection<T> _filteredCollection;
        public ObservableCollection<T> FilteredCollection
        {
            get { return _filteredCollection; }
            set { _filteredCollection = value; OnPropertyChanged("FilteredCollection"); }
        }

        private string _searchText;
        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                FilteredCollection = GetFilteredData(Collection, _searchText);
            }

        }

        private ObservableCollection<T> GetFilteredData(ObservableCollection<T> collection, string searchText)
        {
            ObservableCollection<T> data = new ObservableCollection<T>();
            foreach (T c in collection)
            {
                object[] properties = c.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(pi => pi.PropertyType == typeof(string))
                .Select(pi => pi.GetValue(c, null))
                .ToArray();

                foreach (object prop in properties)
                {
                    if (prop != null)
                    {
                        if (prop.ToString().Contains(searchText))
                        {
                            data.Add(c);
                        }
                    }
                }
            }
            return data;
        }

        #region INotifyProperty Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
