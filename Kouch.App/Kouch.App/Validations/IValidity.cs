using System.Collections.Generic;

namespace Kouch.App.Validations
{
    public interface IValidity
    {
        bool IsValid { get; set; }
        bool IsTouch { get; set; }
        public List<string> Errors { get; set; }
        ValidationCollection ValidationCollection { get; set; }

        bool Validate();
    }
}
