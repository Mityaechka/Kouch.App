using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Kouch.App.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public string this[string name]
        {
            get
            {
                return _errors?.FirstOrDefault(x => x.Key == name).Value?.FirstOrDefault();
            }
        }

        public INavigation Navigation { get; set; }
        public BaseViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
        }
        public void OnPropertyChanged([CallerMemberName] string propName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public event PropertyChangedEventHandler PropertyChanged;


        readonly IDictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool HasErrors =>
        _errors?.Any(x => x.Value?.Any() == true) == true;
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return _errors.SelectMany(x => x.Value);
            }

            if (_errors.ContainsKey(propertyName)
                && _errors[propertyName].Any())
            {
                return _errors[propertyName];
            }

            return new List<string>();
        }
        protected void Validate(Func<bool> rule, string error,
        [CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrWhiteSpace(propertyName)) return;

            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
            }

            if (rule() == false)
            {
                _errors.Add(propertyName, new List<string> { error });
            }

            OnPropertyChanged(nameof(HasErrors));
            OnPropertyChanged();

            ErrorsChanged?.Invoke(this,
                new DataErrorsChangedEventArgs(propertyName));
        }
    }


}
