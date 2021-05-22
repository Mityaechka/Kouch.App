using System.Text.RegularExpressions;

namespace Kouch.App.Validations
{
    public class IsUrl : ValidationRule
    {
        private readonly Regex reg = new Regex(@"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public IsUrl(string validationMessage) : base(validationMessage)
        {
        }

        public override bool Check(object value)
        {
            return reg.IsMatch((string)value);
        }
    }
}
