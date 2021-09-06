using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAD.DataWarehouse.SignOnSite.Api
{
    public partial class SiteAttendance
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("signon_at")]
        public DateTimeOffset SignonAt { get; set; }

        [JsonProperty("signoff_at")]
        public DateTimeOffset? SignoffAt { get; set; }

        [JsonProperty("company")]
        public SACompany Company { get; set; }

        [JsonProperty("is_visitor")]
        public bool IsVisitor { get; set; }

        [JsonProperty("is_inducted_to_site")]
        public bool IsInductedToSite { get; set; }

        [JsonProperty("is_hidden")]
        public bool IsHidden { get; set; }

        [JsonProperty("user")]
        public SAUser User { get; set; }

        public int SiteId { get; set; }

        public virtual Site Site { get; set; }

        public class SACompany
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }

        public class SAUser
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("first_name")]
            public string FirstName { get; set; }

            [JsonProperty("last_name")]
            public string LastName { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }
        }
    }
}
