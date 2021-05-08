using System;

namespace Kouch.App.Validations
{
    public class ExpressionRule : ValidationRule
    {
        private readonly Func<bool> expression;

        public ExpressionRule(Func<bool> expression,string validationMessage) : base(validationMessage)
        {
            this.expression = expression;
        }


        public override bool Check(object value)
        {
            if (expression == null)
            {
                return false;
            }
            return !expression.Invoke();
        }
    }
}
