using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAD.DataWarehouse.SignOnSite.Api
{
    public class PaginatedApiResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }

        [JsonProperty("last_page")]
        public int LastPage { get; set; }
    }

    public class PaginatedApiResponse<TData> : PaginatedApiResponse
    {
        [JsonProperty("data")]
        public IEnumerable<TData> Data { get; set; }
    }
}
