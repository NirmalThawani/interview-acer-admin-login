using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InterviewAcerAdminLogin.Repository
{
    public class CommonRepository
    {
        private readonly HttpClient httpClient;

        public CommonRepository()
        {
            httpClient = new HttpClient();

        }
        public async Task<HttpResponseMessage> GetRequest(string requestUri, string id, string token)
        {
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"].ToString();
            return await httpClient.GetAsync(baseUrl + requestUri + id);
        }
    }
}
