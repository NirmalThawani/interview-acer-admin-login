using InterviewAcerAdminLogin.Common.RequestClasses;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using System;

namespace InterviewAcerAdminLogin.Repository
{
    public class StageRepository
    {
        private readonly HttpClient httpClient;

        public StageRepository()
        {
            this.httpClient = new HttpClient();
           
        }
        public async Task<HttpResponseMessage> GetRequest(string requestUri, string id, string token)
        {
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"].ToString();
            return await httpClient.GetAsync(baseUrl + requestUri + id);
        }

        public async Task<HttpResponseMessage> SaveGroup(SaveGroupRequest requestObject, string token)
        {
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var uri = new Uri(ConfigurationManager.AppSettings["ApiBaseUrl"].ToString() + "api/AddGroup");
            var response = await httpClient.PostAsJsonAsync(uri, requestObject);
            return response;
        }

        public async Task<HttpResponseMessage> SaveCheckList(List<AddUpdateCheckList> requestObject, string token)
        {
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var uri = new Uri(ConfigurationManager.AppSettings["ApiBaseUrl"].ToString() + "api/AddUpdateCheckList");
            var response = await httpClient.PostAsJsonAsync(uri, requestObject);
            return response;
        }

    }
}
