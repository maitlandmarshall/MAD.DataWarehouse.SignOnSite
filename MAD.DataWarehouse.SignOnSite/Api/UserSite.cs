using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAD.DataWarehouse.SignOnSite.Api
{
    public class UserSite
    {
        public string Address { get; set; }
        public string Classification { get; set; }
        public int Id { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        [JsonProperty("job_ref")]
        public string JobRef { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }
    }
}
