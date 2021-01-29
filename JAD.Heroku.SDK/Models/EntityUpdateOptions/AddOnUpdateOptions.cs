using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK.Models.EntityUpdateOptions
{
    public class AddOnUpdateOptions
    {
        [Required]
        [JsonPropertyName("plan")]
        public string plan { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
