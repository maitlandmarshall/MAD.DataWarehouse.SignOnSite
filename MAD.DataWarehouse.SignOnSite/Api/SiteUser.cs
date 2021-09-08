using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAD.DataWarehouse.SignOnSite.Api
{
    public class SiteUser
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("induction")]
        public SUInduction Induction { get; set; }

        [JsonProperty("site_company")]
        public SUSiteCompany SiteCompany { get; set; }

        public int SiteId { get; set; }
        public virtual Site Site { get; set; }

        public partial class SUInduction
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("number")]
            public int Number { get; set; }

            [JsonProperty("state")]
            public InductionState State { get; set; }

            public partial class InductionState
            {
                [JsonProperty("as_string")]
                public string AsString { get; set; }

                [JsonProperty("set_at")]
                public DateTime SetAt { get; set; }

                [JsonProperty("set_by")]
                public StateSetBy SetBy { get; set; }

                public partial class StateSetBy
                {
                    [JsonProperty("id")]
                    public int Id { get; set; }

                    [JsonProperty("first_name")]
                    public string FirstName { get; set; }

                    [JsonProperty("last_name")]
                    public string LastName { get; set; }
                }
            }
        }

        public partial class SUSiteCompany
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }
    }
}
