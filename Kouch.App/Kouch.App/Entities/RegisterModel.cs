using System;
using System.Collections.Generic;
using System.Text;

namespace Kouch.App.Entities
{
    public class RegisterEmailModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }
    public class RegisterCodeModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
