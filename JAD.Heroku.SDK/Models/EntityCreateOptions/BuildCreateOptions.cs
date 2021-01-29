using JAD.Heroku.SDK.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK.Models.EntityCreateOptions
{
    public class BuildCreateOptions
    {
        [Required]
        [JsonPropertyName("source_blob")]
        public SourceBlob SourceBlob { get; set; }
        [JsonPropertyName("buildpacks")]
        public List<BuildPack> BuildPacks { get; set; }
    }
}
