using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK.Models.Entities
{
    public class AddOn
    {
        [JsonPropertyName("actions")]
        public List<Actions> Actions { get; set; }
        [JsonPropertyName("addon_service")]
        public AddOnService AddOnService { get; set; }
        [JsonPropertyName("app")]
        public App App { get; set; }
        [JsonPropertyName("billed_price")]
        public BilledPrice BilledPrice { get; set; }
        [JsonPropertyName("billing_entity")]
        public BillingEntity BillingEntity { get; set; }
        [JsonPropertyName("config_vars")]
        public List<string> ConfigVars { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("plan")]
        public Plan Plan { get; set; }
        [JsonPropertyName("provider_id")]
        public string ProviderId { get; set; }
        [JsonPropertyName("state")]
        public string State { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonPropertyName("web_url")]
        public Uri? WebUrl { get; set; }
    }

    public class Actions
    {
        [JsonPropertyName("action")]
        public string Action { get; set; }
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("label")]
        public string Label { get; set; }
        [JsonPropertyName("requires_owner")]
        public bool? RequiresOwner { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class AddOnService
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class BilledPrice
    {
        [JsonPropertyName("cents")]
        public int Cents { get; set; }
        [JsonPropertyName("contract")]
        public bool? Contract { get; set; }
        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }

    public class BillingEntity
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class Plan
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
