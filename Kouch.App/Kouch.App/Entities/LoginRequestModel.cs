using System;
using System.Collections.Generic;
using System.Text;

namespace Kouch.App.Entities
{
    public class LoginRequestModel
    {
        public string Email{ get; set; }
        public string Password { get; set; }
    }
    public class LoginResponseModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public TokenModel Tokens { get; set; }

    }
    public class TokenModel
    {
        public string Access { get; set; }
        public string Refresh { get; set; }
    }
}
