using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK.Models.Entities
{
    public class Dyno
    {
        [JsonPropertyName("app")]
        public App App { get; set; }
        [JsonPropertyName("attach_url")]
        public string AttachUrl { get; set; }
        [JsonPropertyName("command")]
        public string Command { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("release")]
        public Release Release { get; set; }
        [JsonPropertyName("size")]
        public string Size { get; set; }
        [JsonPropertyName("state")]
        public string State { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime UpdateAt { get; set; }
    }
}
