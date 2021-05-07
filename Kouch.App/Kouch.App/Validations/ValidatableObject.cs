using Kouch.App.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kouch.App.Validations
{
    public class ValidatableObject<T> : BaseViewModel, IValidity, IEnumerable<IValidationRule<T>>
    {
        private readonly List<IValidationRule<T>> _validations;
        private readonly BaseViewModel parent;
        private List<string> _errors;
        private T _value;
        private bool _isValid;

        public List<IValidationRule<T>> Validations => _validations;

        public List<string> Errors
        {
            get
            {
                return _errors;
            }
            set
            {
                _errors = value;
                OnPropertyChanged();
            }
        }

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged();
                Validate();
            }
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                OnPropertyChanged();
            }
        }
        public ValidatableObject(BaseViewModel parent) : base(null)
        {
            _isValid = true;
            _errors = new List<string>();
            _validations = new List<IValidationRule<T>>();
            this.parent = parent;
        }


        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = _validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();
            parent.OnPropertyChanged("HasErrors");
            if (Errors.Count != 0)
            {
                parent.OnPropertyChanged("FirstError");
            }
            return this.IsValid;
        }

        public IEnumerator<IValidationRule<T>> GetEnumerator()
        {
            return Validations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Validations.GetEnumerator();
        }
        public void Add(IValidationRule<T> validationRule)
        {
            Validations.Add(validationRule);
        }
    }
    public interface IValidity
    {
        bool IsValid { get; set; }
        public List<string> Errors { get; set; }
    }
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; set; }

        bool Check(T value);
    }
    public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
    {
        public IsNotNullOrEmptyRule(string validationMessage)
        {
            ValidationMessage = validationMessage;
        }

        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;

            return !string.IsNullOrWhiteSpace(str);
        }
    }
}
