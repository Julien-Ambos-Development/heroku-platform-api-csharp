using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK.Models.EntityCreateOptions
{
    public class PipelineCouplingCreateOptions
    {
        [Required(AllowEmptyStrings = false)]
        [JsonPropertyName("app")]
        public Guid AppId { get; set; }
        [Required(AllowEmptyStrings = false)]
        [JsonPropertyName("pipeline")]
        public Guid PipelineId { get; set; }
        [Required(AllowEmptyStrings = false)]
        [JsonPropertyName("stage")]
        public string Stage { get; set; }
    }
}
