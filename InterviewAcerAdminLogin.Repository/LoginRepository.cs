using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace InterviewAcerAdminLogin.Repository
{
    public class LoginRepository
    {
        private readonly HttpClient httpClient;

        public LoginRepository()
        {
            this.httpClient = new HttpClient();
        }
        public async Task<HttpResponseMessage> PostFormEncodedContent(string requestUri, params KeyValuePair<string, string>[] values)
        {
            var baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"].ToString();
            using (var content = new FormUrlEncodedContent(values))
            {
                var response = await httpClient.PostAsync(baseUrl+requestUri, content);
                return response;
            }
        }
    }
}
