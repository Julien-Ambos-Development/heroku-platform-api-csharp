using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK.Models.Entities
{
    public class PipelineCoupling
    {
        [JsonPropertyName("app")]
        public App App { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("pipeline")]
        public Pipeline Pipeline { get; set; }
        [JsonPropertyName("stage")]
        public string Stage { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
