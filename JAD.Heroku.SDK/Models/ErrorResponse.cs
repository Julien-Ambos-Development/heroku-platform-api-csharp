using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK.Models
{
    public class ErrorResponse
    {
        [JsonPropertyName("id")]
        public string ErrorId { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
