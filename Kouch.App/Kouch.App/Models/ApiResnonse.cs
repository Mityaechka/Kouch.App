using System;
using System.Collections.Generic;
using System.Text;

namespace Kouch.App.Models
{
    public class ApiResnonse
    {
        public string Error { get; set; }
        public bool IsSuccsess { get; set; }
    }
    public class ApiResnonse<T>:ApiResnonse
    {
        public T Result { get; set; }
    }
}
