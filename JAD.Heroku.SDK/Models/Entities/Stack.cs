using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK.Models.Entities
{
    public class Stack
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class StackFormat
    {
        public const string Heroku20 = "heroku-20";
        public const string Heroku18 = "heroku-18";
        public const string Heroku16 = "heroku-16";
        public const string Container = "container";
    }
}
