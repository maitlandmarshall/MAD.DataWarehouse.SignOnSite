using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAD.DataWarehouse.SignOnSite.Api
{
    public partial class SiteUserApiResponse
    {
        [JsonProperty("users")]
        public List<SiteUser> Users { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
