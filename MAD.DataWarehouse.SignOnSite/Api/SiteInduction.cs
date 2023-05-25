using Newtonsoft.Json;
using System;

namespace MAD.DataWarehouse.SignOnSite.Api
{
    public partial class SiteInduction
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("site_company")]
        public SiteCompany SiteCompany { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("unsubmitted_forms")]
        public UnsubmittedForm[] UnsubmittedForms { get; set; }

        [JsonProperty("completed_inductions")]
        public CompletedInduction[] CompletedInductions { get; set; }
    }

    public partial class CompletedInduction
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("status_set_by")]
        public StatusSetBy StatusSetBy { get; set; }

        [JsonProperty("status_set_at")]
        public DateTimeOffset StatusSetAt { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("number")]
        public long Number { get; set; }

        [JsonProperty("form_name")]
        public string FormName { get; set; }

        [JsonProperty("form_type")]
        public string FormType { get; set; }

        [JsonProperty("form_id")]
        public long? FormId { get; set; }
    }

    public partial class StatusSetBy
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }
    }

    public partial class SiteCompany
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class UnsubmittedForm
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("offsite")]
        public bool Offsite { get; set; }
    }
}
