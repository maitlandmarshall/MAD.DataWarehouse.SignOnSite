using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAD.DataWarehouse.SignOnSite.Api
{
    public class SiteBriefing
    {
        public int Id { get; set; }

        [JsonProperty("acknowledgement_count")]
        public int AcknowledgementCount { get; set; }

        public string Content { get; set; }

        [JsonProperty("content_text")]
        public string ContentText { get; set; }

        [JsonProperty("current_status")]
        public string CurrentStatus { get; set; }

        [JsonProperty("end_date")]
        public DateTime? EndDate { get; set; }

        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }

        public int Number { get; set; }

        [JsonProperty("seen_count")]
        public int SeenCount { get; set; }

        [JsonProperty("should_show_set_by_user")]
        public bool ShouldShowSetByUser { get; set; }

        public string Type { get; set; }

        public int SiteId { get; set; }
        public virtual Site Site { get; set; }
    }
}
