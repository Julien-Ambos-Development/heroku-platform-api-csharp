using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JAD.Heroku.SDK.Models;
using JAD.Heroku.SDK.Models.Entities;

namespace JAD.Heroku.SDK.Models.Entities
{
    public class App
    {
        [JsonPropertyName("acm")]
        public bool Acm { get; set; }
        [JsonPropertyName("archived_at")]
        public DateTime? ArchivedAt { get; set; }
        [JsonPropertyName("build_stack")]
        public BuildStack BuildStack { get; set; }
        [JsonPropertyName("buildpack_provided_description")]
        public string BuildpackProvidedDescription { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreateAt { get; set; }
        [JsonPropertyName("git_url")]
        public string GitUrl { get; set; }
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("internal_routing")]
        public bool? InternalRouting { get; set; }
        [JsonPropertyName("maintenance")]
        public bool Maintenance { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("organization")]
        public Organization Organization { get; set; }
        [JsonPropertyName("owner")]
        public Owner Owner { get; set; }
        [JsonPropertyName("region")]
        public Region Region { get; set; }
        [JsonPropertyName("released_at")]
        public DateTime? ReleasedAt { get; set; }
        [JsonPropertyName("repo_size")]
        public int? RepoSize { get; set; }
        [JsonPropertyName("slug_size")]
        public int? SlugSize { get; set; }
        [JsonPropertyName("space")]
        public Space Space { get; set; }
        [JsonPropertyName("stack")]
        public Stack Stack { get; set; }
        [JsonPropertyName("team")]
        public Team Team { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonPropertyName("web_url")]
        public string Uri { get; set; }
    }
}
