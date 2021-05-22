namespace Kouch.App.Validations
{
    public class IsNullOrRule : ValidationRule
    {
        public IsNullOrRule(ValidationRule validationRule) : base(validationRule?.ValidationMessage)
        {
            ValidationRule = validationRule;
        }

        public ValidationRule ValidationRule { get; }

        public override bool Check(object value)
        {
            if (value == null || ValidationRule == null || (string.IsNullOrWhiteSpace(value.ToString())))
            {
                return true;
            }
            else
            {
                return ValidationRule.Check(value);
            }
        }
    }
}
