using Kouch.App.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kouch.App.Validations
{
    public class ValidatableObject<T> : BaseViewModel, IValidity, IEnumerable<ValidationRule>
    {
        public Action onChangeValue;
        private readonly List<ValidationRule> _validations;
        private readonly BaseViewModel parent;
        private List<string> _errors;
        private T _value;
        private bool _isValid;
        public ValidationCollection ValidationCollection { get; set; }
        public List<ValidationRule> Validations => _validations;

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
                OnPropertyChanged(nameof(FirstError));
            }
        }
        public string FirstError => Errors?.FirstOrDefault();
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (!IsTouch&&value!=null)
                {
                    IsTouch = true;
                }
                _value = value;
                if (onChangeValue != null)
                {
                    onChangeValue();
                }
                OnPropertyChanged();
                if (ValidationCollection == null)
                {
                    Validate();
                }
                else
                {
                    ValidationCollection.UpdateAll();
                }
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
                OnPropertyChanged(nameof(ShowError));
            }
        }
        private bool isTouch;
        public bool IsTouch
        {
            get => isTouch; set
            {
                if (isTouch != value)
                {
                    isTouch = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ShowError));
                }
            }
        }
        public bool ShowError => IsTouch && !IsValid;
        public ValidatableObject(BaseViewModel parent) : base(null)
        {
            _isValid = true;
            _errors = new List<string>();
            _validations = new List<ValidationRule>();
            this.parent = parent;
        }


        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = _validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();


            ValidationCollection?.Notify();

            return this.IsValid;
        }

        public IEnumerator<ValidationRule> GetEnumerator()
        {
            return Validations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Validations.GetEnumerator();
        }
        public void Add(ValidationRule validationRule)
        {
            Validations.Add(validationRule);
        }
    }
    public class ValidationCollection : IEnumerable<IValidity>
    {
        public ValidationCollection(string name, BaseViewModel parent)
        {
            Name = name;
            Parent = parent;
        }
        protected readonly List<IValidity> validatableObjects = new List<IValidity>();



        public bool HasErrors => validatableObjects.Any(x => !x.IsValid);
        public bool IsValid => !validatableObjects.Any(x => !x.IsValid);
        public string FirstError => validatableObjects.SelectMany(x => x.Errors).FirstOrDefault();

        public string Name { get; }
        public BaseViewModel Parent { get; }

        public void Notify()
        {
            Parent?.OnPropertyChanged($"{Name}");
        }
        public void UpdateAll()
        {
            validatableObjects.ForEach(x => x?.Validate());
            
        }
        public void Add(IValidity validity)
        {
            validity.ValidationCollection = this;
            validatableObjects.Add(validity);
        }
        public IEnumerator<IValidity> GetEnumerator()
        {
            return validatableObjects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return validatableObjects.GetEnumerator();

        }
    }
}
