namespace Kouch.App.Validations
{
    public abstract class ValidationRule
    {

        public string ValidationMessage { get; set; }

        protected ValidationRule(string validationMessage)
        {
            ValidationMessage = validationMessage;
        }

        public abstract bool Check(object value);
    }
}
