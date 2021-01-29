using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK.Models.EntityCreateOptions
{
    public class AddOnAttachmentCreateOptions
    {
        [Required]
        [JsonPropertyName("addon")]
        public string Addon { get; set; }
        [Required]
        [JsonPropertyName("app")]
        public string App { get; set; }

        [JsonPropertyName("confirm")]
        public string Confirm { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("namespace")]
        public string Namespace { get; set; }
    }
}
