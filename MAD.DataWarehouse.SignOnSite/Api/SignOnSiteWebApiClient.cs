﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MAD.DataWarehouse.SignOnSite.Api
{
    public class SignOnSiteWebApiClient
    {
        private readonly HttpClient httpClient;

        public SignOnSiteWebApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task Login(string email, string password)
        {
            var formContent = new MultipartFormDataContent();
            formContent.Add(new StringContent(email), nameof(email));
            formContent.Add(new StringContent(password), nameof(password));

            await this.httpClient.PostAsync("/login", formContent);
        }

        public async Task<ApiResponse<SiteBriefing>> GetSiteBriefings(int siteId, int? limit = null, int? offset = null)
        {
            var endpoint = $"/web/api/v2/sites/{siteId}/briefings";
            var query = new Dictionary<string, object>
            {
                {nameof(limit), limit },
                {nameof(offset), offset }
            }.CreateQueryString();

            var responseJson = await this.httpClient.GetStringAsync($"{endpoint}?{query}");
            var response = JsonConvert.DeserializeObject<ApiResponse<SiteBriefing>>(responseJson);

            return response;
        }
    }
}
