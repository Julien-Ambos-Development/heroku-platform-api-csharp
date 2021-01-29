using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK.Models.EntityCreateOptions
{
    public class DynoCreateOptions
    {
        [Required(AllowEmptyStrings = false)]
        [JsonPropertyName("command")]
        public string Command { get; set; }
        [JsonPropertyName("attach")]
        public bool Attach { get; set; }
        [JsonPropertyName("env")]
        public Dictionary<string,string> Environment { get; set; }
        [JsonPropertyName("force_no_tty")]
        public bool ForceNoTty { get; set; }
        [JsonPropertyName("size")]
        public string Size { get; set; }
        [JsonPropertyName("time_to_live")]
        public int TimeToLive { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
