using JAD.Heroku.SDK.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK.Models.Entities
{
    public class Build
    {
        [JsonPropertyName("app")]
        public App App { get; set; }
        [JsonPropertyName("buildpacks")]
        public List<BuildPack> Buildpacks { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("output_stream_url")]
        public string OutputStreamUrl { get; set; }
        [JsonPropertyName("release")]
        public Release Release { get; set; }
        [JsonPropertyName("slug")]
        public Slug Slug { get; set; }
        [JsonPropertyName("source_blob")]
        public SourceBlob SourceBlob { get; set; }
        [JsonPropertyName("stack")]
        public string Stack { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonPropertyName("user")]
        public User User { get; set; }
    }

    public class BuildPack
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class SourceBlob
    {
        [JsonPropertyName("checksum")]
        public string Checksum { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("version")]
        public string Version { get; set; }
    }
    public class User
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }
    
    public class BuildStatus
    {
        public const string Pending = "pending";
        public const string Failed = "failed";
        public const string Succeeded = "succeeded";
    }
}
