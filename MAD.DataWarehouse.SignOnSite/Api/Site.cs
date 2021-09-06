using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAD.DataWarehouse.SignOnSite.Api
{
    public class Site
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("internal_reference")]
        public string InternalReference { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }
    }


}
