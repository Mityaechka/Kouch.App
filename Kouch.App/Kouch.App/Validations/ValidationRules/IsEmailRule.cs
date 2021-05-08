using System.ComponentModel.DataAnnotations;

namespace Kouch.App.Validations
{
    public class IsEmailRule : ValidationRule
    {
        public IsEmailRule(string validationMessage) : base(validationMessage)
        {
        }

        public override bool Check(object value)
        {
            EmailAddressAttribute emailAddressAttribute = new EmailAddressAttribute();
            return emailAddressAttribute.IsValid(value);
        }
    }
}
