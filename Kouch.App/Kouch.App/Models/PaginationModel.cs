using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
