using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK.Models.Entities
{
    public class Account
    {
        [JsonPropertyName("acknowledged_msa")]
        public bool AcknowledgedMsa { get; set; }
        [JsonPropertyName("acknowledged_msa_at")]
        public DateTime? AcknowledgedMsaAt { get; set; }
        [JsonPropertyName("allow_tracking")]
        public bool AllowTracking { get; set; }
        [JsonPropertyName("beta")]
        public bool Beta { get; set; }
        [JsonPropertyName("country_of_residence")]
        public string CountryOfResidence { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("default_organization")]
        public DefaultOrganisation DefaultOrganisation { get; set; }
        [JsonPropertyName("default_team")]
        public DefaultTeam DefaultTeam { get; set; }
        [JsonPropertyName("delinquent_at")]
        public DateTime? DelinquentAt { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("federated")]
        public bool Federated { get; set; }
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("identity_provider")]
        public IdentityProvider IdentityProvider { get; set; }
        [JsonPropertyName("italian_customer_terms")]
        public string ItalianCustomerTerms { get; set; }
        [JsonPropertyName("italian_partner_terms")]
        public string ItalianPartnerTerms { get; set; }
        [JsonPropertyName("last_login")]
        public DateTime? LastLogin { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("sms_number")]
        public string SmsNumber { get; set; }
        [JsonPropertyName("suspended_at")]
        public DateTime? SuspendedAt { get; set; }
        [JsonPropertyName("two_factor_authentication")]
        public bool TwoFactorAuthentication { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonPropertyName("verified")]
        public bool Verified { get; set; }
    }

    public class DefaultOrganisation
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
    public class DefaultTeam
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
    public class IdentityProvider
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("organization")]
        public Organization Organization { get; set; }
        [JsonPropertyName("owner")]
        public Owner Owner { get; set; }
        [JsonPropertyName("team")]
        public Team Team { get; set; }
    }
}
