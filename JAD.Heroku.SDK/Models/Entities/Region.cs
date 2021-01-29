using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK.Models.Entities
{
    public class Region
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class RegionFormat
    {
        public const string Europe = "eu";
        public const string UnitedStates = "us";
        public const string Dublin = "dublin";
        public const string Frankfurt = "frankfurt";
        public const string Orgeon = "oregon";
        public const string Sydney = "sydney";
        public const string Tokyo = "tokyo";
        public const string Virginia = "virginia";
    }
}
