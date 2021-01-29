using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK.Models.Entities
{
    public class Source
    {
        [JsonPropertyName("source_blob")]
        public SourceBlobSource SourceBlob { get; set; }
    }

    public class SourceBlobSource{
        [JsonPropertyName("get_url")]
        public string GetUrl { get; set; }
        [JsonPropertyName("put_url")]
        public string PutUrl { get; set; }
    }
}
