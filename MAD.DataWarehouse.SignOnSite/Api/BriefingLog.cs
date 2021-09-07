using Newtonsoft.Json;
using System;

namespace MAD.DataWarehouse.SignOnSite.Api
{
    public class BriefingLog
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("company")]
        public BLCompany Company { get; set; }

        [JsonProperty("earliest_seen_at")]
        public DateTimeOffset? EarliestSeenAt { get; set; }

        [JsonProperty("earliest_acknowledged_at")]
        public DateTimeOffset? EarliestAcknowledgedAt { get; set; }

        [JsonProperty("day")]
        public DateTimeOffset Day { get; set; }

        public int BriefingId { get; set; }
        public virtual SiteBriefing Briefing { get; set; }

        public partial class BLCompany
        {
            [JsonProperty("name")]
            public string Name { get; set; }
        }
    }
}