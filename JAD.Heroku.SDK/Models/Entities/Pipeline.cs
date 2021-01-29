using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK.Models.Entities
{
    public class Pipeline
    {
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("id")] 
        public Guid Id { get; set; }
        [JsonPropertyName("name")] 
        public string Name { get; set; }
        [JsonPropertyName("owner")] 
        public Owner Owner { get; set; }
        [JsonPropertyName("updated_at")] 
        public DateTime UpdatedAt { get; set; }
    }

    public static class PipelineStage
    {
        public const string Production = "production";
        public const string Staging = "staging";
        public const string Test = "test";
        public const string Review = "review";
    }
}
