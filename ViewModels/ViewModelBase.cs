using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SalonFryzjerski.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly Dictionary<string, string> _errors = new();

        // Opcjonalna obs³uga b³êdów dla walidacji w³aœciwoœci
        public string this[string columnName]
        {
            get
            {
                if (_errors.ContainsKey(columnName))
                {
                    return _errors[columnName];
                }
                return null;
            }
        }

        public string Error => _errors.Any() ? string.Join("\n", _errors.Values) : null;

        protected void SetError(string propertyName, string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                _errors.Remove(propertyName);
            }
            else
            {
                _errors[propertyName] = errorMessage;
            }
            OnPropertyChanged(nameof(Error));
            OnPropertyChanged(propertyName);
        }

        public void ClearErrors()
        {
            _errors.Clear();
            OnPropertyChanged(nameof(Error));
        }
    }
}
