using Newtonsoft.Json;
using System;
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
            var queryBuilder = HttpUtility.ParseQueryString(string.Empty);

            queryBuilder[nameof(limit)] = limit.ToString();
            queryBuilder[nameof(offset)] = offset.ToString();

            var responseJson = await this.httpClient.GetStringAsync($"{endpoint}?{queryBuilder}");
            var response = JsonConvert.DeserializeObject<ApiResponse<Site>>(responseJson);

            return response;
        }
    }
}
