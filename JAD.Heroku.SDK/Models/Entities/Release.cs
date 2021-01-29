using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK.Models.Entities
{
    public class Release
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }
}
