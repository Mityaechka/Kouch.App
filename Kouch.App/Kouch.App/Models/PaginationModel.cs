using Newtonsoft.Json;
using System.Collections.Generic;

namespace Kouch.App.Models
{
    public class PaginationModel<T>
    {
        public int Count { get; set; }
        [JsonProperty("is_end")]
        public bool IsEnd { get; set; }
        [JsonProperty("results")]

        public List<T> Data { get; set; }
    }
}
