using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK.Models.Entities
{
    public class Domain
    {
        [JsonPropertyName("acm_status")]
        public string AcmStatus { get; set; }
        [JsonPropertyName("acm_status_reason")]
        public string AcmStatusReason { get; set; }
        [JsonPropertyName("app")]
        public App App { get; set; }
        [JsonPropertyName("cname")]
        public string Cname { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("hostname")]
        public Uri Hostname { get; set; }
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("kind")]
        public string Kind { get; set; }
        [JsonPropertyName("sni_endpoint")]
        public SniEndpoint SniEndpoint { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    public class SniEndpoint
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
