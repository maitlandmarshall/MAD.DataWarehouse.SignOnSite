using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAD.DataWarehouse.SignOnSite.Api
{
    public class BriefingLogApiResponse
    {
        [JsonProperty("active_days")]
        public IEnumerable<BriefingLogActiveDay> ActiveDays { get; set; }

        public string Status { get; set; }
    }
}
