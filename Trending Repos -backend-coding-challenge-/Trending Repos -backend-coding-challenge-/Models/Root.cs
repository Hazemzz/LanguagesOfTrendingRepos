using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Trending_Repos__backend_coding_challenge_.Models
{
    public class Root
    {
        [JsonPropertyName("items")]
        public List<Items> Items { get; set; }
    }
}