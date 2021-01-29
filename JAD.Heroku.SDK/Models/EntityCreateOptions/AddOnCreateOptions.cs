using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK.Models.EntityCreateOptions
{
    public class AddOnCreateOptions
    {
        [Required]
        [JsonPropertyName("plan")]
        public string plan { get; set; }
        [JsonPropertyName("attachment")]
        public AddOnCreateAttachment Attachment { get; set; }
        [JsonPropertyName("config")]
        public Dictionary<string, string> Config { get; set; } = new Dictionary<string, string>();
        [JsonPropertyName("confirm")]
        public string Confirm { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
    
    public class AddOnCreateAttachment
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
