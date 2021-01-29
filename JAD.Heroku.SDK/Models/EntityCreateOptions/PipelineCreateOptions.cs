using JAD.Heroku.SDK.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JAD.Heroku.SDK.Models.EntityCreateOptions
{
    public class PipelineCreateOptions
    {
        [Required(AllowEmptyStrings = false)]
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [Required]
        [JsonPropertyName("owner")]
        public Owner Owner { get; set; }
    }
}
