using System.Text.Json.Serialization;

namespace Trending_Repos__backend_coding_challenge_.Models
{
    public class Items
    {
        [JsonPropertyName("language")]
        public string Language { get; set; }
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("full_name")]
        public string Full_Name { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
