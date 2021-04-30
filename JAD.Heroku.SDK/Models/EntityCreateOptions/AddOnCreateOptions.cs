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
        public string Plan { get; set; }
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

    public class AddOnPostgresqlPlanTypes
    {
        public const string HobbyDev = "heroku-postgresql:hobby-dev";
        public const string HobbyBasic = "heroku-postgresql:hobby-basic";
        public const string Standard0 = "heroku-postgresql:standard-0";
        public const string Standard1 = "heroku-postgresql:standard-1";
        public const string Standard2 = "heroku-postgresql:standard-2";
        public const string Standard3 = "heroku-postgresql:standard-3";
        public const string Standard4 = "heroku-postgresql:standard-4";
        public const string Standard5 = "heroku-postgresql:standard-5";
        public const string Standard6 = "heroku-postgresql:standard-6";
        public const string Standard7 = "heroku-postgresql:standard-7";
        public const string Standard8 = "heroku-postgresql:standard-8";
        public const string Standard9 = "heroku-postgresql:standard-9";
        public const string Shield0 = "heroku-postgresql:shield-0";
        public const string Shield1 = "heroku-postgresql:shield-1";
        public const string Shield2 = "heroku-postgresql:shield-2";
        public const string Shield3 = "heroku-postgresql:shield-3";
        public const string Shield4 = "heroku-postgresql:shield-4";
        public const string Shield5 = "heroku-postgresql:shield-5";
        public const string Shield6 = "heroku-postgresql:shield-6";
        public const string Shield7 = "heroku-postgresql:shield-7";
        public const string Shield8 = "heroku-postgresql:shield-8";
        public const string Shield9 = "heroku-postgresql:shield-9";
        public const string Premium0 = "heroku-postgresql:premium-0";
        public const string Premium1 = "heroku-postgresql:premium-1";
        public const string Premium2 = "heroku-postgresql:premium-2";
        public const string Premium3 = "heroku-postgresql:premium-3";
        public const string Premium4 = "heroku-postgresql:premium-4";
        public const string Premium5 = "heroku-postgresql:premium-5";
        public const string Premium6 = "heroku-postgresql:premium-6";
        public const string Premium7 = "heroku-postgresql:premium-7";
        public const string Premium8 = "heroku-postgresql:premium-8";
        public const string Premium9 = "heroku-postgresql:premium-9";
        public const string Private0 = "heroku-postgresql:private-0";
        public const string Private1 = "heroku-postgresql:private-1";
        public const string Private2 = "heroku-postgresql:private-2";
        public const string Private3 = "heroku-postgresql:private-3";
        public const string Private4 = "heroku-postgresql:private-4";
        public const string Private5 = "heroku-postgresql:private-5";
        public const string Private6 = "heroku-postgresql:private-6";
        public const string Private7 = "heroku-postgresql:private-7";
        public const string Private8 = "heroku-postgresql:private-8";
        public const string Private9 = "heroku-postgresql:private-9";
    }
}
