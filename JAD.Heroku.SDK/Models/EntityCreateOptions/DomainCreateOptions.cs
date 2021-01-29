using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK.Models.EntityCreateOptions
{
    public class DomainCreateOptions
    {
        [Required(AllowEmptyStrings = false)]
        [JsonPropertyName("hostname")]
        public string Hostname { get; set; }
        [Required(AllowEmptyStrings = false)]
        [JsonPropertyName("sni_endpoint")]
        public string SniEndpoint { get; set; }
    }
}
