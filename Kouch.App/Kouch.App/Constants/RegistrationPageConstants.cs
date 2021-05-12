using Kouch.App.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kouch.App.Constants
{
    public static class RegistrationPageConstants
    {
        public const string EMAIL = "email";
        public const string CODE = "code";
    }
    public static class ServiceTypeDictionary
    {
        public static readonly List<ServiceTypeDisplay> Data = new List<ServiceTypeDisplay>
        {
            new ServiceTypeDisplay{ServiceType = ServiceType.Item,Name = "Вещь" },
            new ServiceTypeDisplay{ServiceType = ServiceType.Service,Name = "Услуга" }
        };
    }
}
