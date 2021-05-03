using Newtonsoft.Json;

namespace Kouch.App.Entities
{
    public class File
    {
        [JsonProperty("image")]
        public string Path { get; set; }
    }
}
