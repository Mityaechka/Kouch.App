using System.Text.RegularExpressions;

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
    public class IsNullOrRule : ValidationRule
    {
        public IsNullOrRule(ValidationRule validationRule) : base(validationRule?.ValidationMessage)
        {
            ValidationRule = validationRule;
        }

        public ValidationRule ValidationRule { get; }

        public override bool Check(object value)
        {
            if (value == null || ValidationRule == null ||(string.IsNullOrWhiteSpace(value.ToString())))
            {
                return true;
            }
            else
            {
                return ValidationRule.Check(value);
            }
        }
    }
    public class IsUrl : ValidationRule
    {
        Regex reg = new Regex(@"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    
        public IsUrl(string validationMessage) : base(validationMessage)
        {
        }

        public override bool Check(object value)
        {
            return reg.IsMatch((string)value);
        }
    }
}
