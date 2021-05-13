using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kouch.App.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Category> Children { get; set; }
    }
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        //public Profile Profile { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        public string Avatar { get; set; }
        [JsonProperty("country")]
        public Country Country { get; set; }
        [JsonProperty("city")]
        public City City { get; set; }
        [JsonProperty("vk_link")]
        public string Vk { get; set; }
        [JsonProperty("facebook_link")]
        public string Facebook { get; set; }
        [JsonProperty("instagram_link")]
        public string Instagram { get; set; }

    }

    //public class Profile
    //{
    //    [JsonProperty("first_name")]
    //    public string FirstName { get; set; }
    //    [JsonProperty("last_name")]
    //    public string LastName { get; set; }
    //    public string Avatar { get; set; }
    //}
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
