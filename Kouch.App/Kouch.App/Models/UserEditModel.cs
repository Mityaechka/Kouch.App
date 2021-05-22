using Newtonsoft.Json;

namespace Kouch.App.Models
{
    public class UserEditModel
    {
        [JsonProperty("first_name", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }
        [JsonProperty("last_name", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }
        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public int? CountryId { get; set; }
        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        public int? CityId { get; set; }
        [JsonProperty("vk_link")]
        public string Vk { get; set; }
        [JsonProperty("instagram_link")]
        public string Instagram { get; set; }
        [JsonProperty("facebook_link")]
        public string Facebook { get; set; }
    }
}
