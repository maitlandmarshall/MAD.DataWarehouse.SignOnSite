using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace MAD.DataWarehouse.SignOnSite.Api
{
    public class SignOnSiteApiClient
    {
        private readonly HttpClient httpClient;

        public SignOnSiteApiClient(HttpClient httpClient, AppConfig appConfig)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri("https://app.signonsite.com.au");
            this.httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", appConfig.SignOnSiteKey);
        }

        public async Task<ApiResponse<Site>> GetSites(int limit, int offset)
        {
            var endpoint = "/api/public/sites";
            var query = this.CreateQueryString(new Dictionary<string, object>
            {
                { nameof(limit), limit},
                { nameof(offset), offset}
            });

            var responseJson = await this.httpClient.GetStringAsync($"{endpoint}?{query}");
            var response = JsonConvert.DeserializeObject<ApiResponse<Site>>(responseJson);

            return response;
        }

        public async Task<ApiResponse<SiteAttendance>> GetSiteAttendances(int limit, int offset, int siteId, DateTime? filter_start_time = null, DateTime? filter_end_time = null, string order_direction = null)
        {
            var endpoint = $"/api/public/sites/{siteId}/attendances";
            var query = this.CreateQueryString(new Dictionary<string, object>
            {
                { nameof(limit), limit},
                { nameof(offset), offset},
                { nameof(filter_start_time), filter_start_time},
                { nameof(filter_end_time), filter_end_time},
                { nameof(order_direction), order_direction},
            });

            var responseJson = await this.httpClient.GetStringAsync($"{endpoint}?{query}");
            var response = JsonConvert.DeserializeObject<ApiResponse<SiteAttendance>>(responseJson);

            return response;
        }

        private string CreateQueryString(IDictionary<string, object> queryParams)
        {
            var queryBuilder = HttpUtility.ParseQueryString(string.Empty);

            foreach (var qp in queryParams)
            {
                if (qp.Value is null)
                    continue;

                if (qp.Value is DateTime dt)
                {
                    queryBuilder[qp.Key] = dt.ToString("yyyy-MM-dd");
                }
                else
                {
                    queryBuilder[qp.Key] = qp.Value.ToString();
                }
            }

            return queryBuilder.ToString();
        }
    }
}
