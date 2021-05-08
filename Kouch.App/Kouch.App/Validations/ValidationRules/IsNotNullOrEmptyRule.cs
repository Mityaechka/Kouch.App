namespace Kouch.App.Validations
{
    public class IsNotNullOrEmptyRule : ValidationRule
    {
        public IsNotNullOrEmptyRule(string validationMessage):base(validationMessage)
        {
        }


        public override bool Check(object value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;

            return !string.IsNullOrWhiteSpace(str);
        }
    }
    public class LengthRule : ValidationRule
    {
        private readonly int length;

        public LengthRule(int length, string validationMessage) : base(validationMessage)
        {
            this.length = length;
        }

        public override bool Check(object value)
        {
            if (value == null)
            {
                return false;
            }
           if(typeof(string)!= value.GetType())
            {
                return false;
            }
            return ((string)value).Length == length;
        }
    }
}
