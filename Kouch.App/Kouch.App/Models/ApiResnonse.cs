using System;
using System.Collections.Generic;
using System.Text;

namespace Kouch.App.Models
{
    public class ApiResnonse<T>
    {
        public string Error { get; set; }
        public T Data { get; set; }
        public bool IsSuccsess { get; set; }
    }
}
