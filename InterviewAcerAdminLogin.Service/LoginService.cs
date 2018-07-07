using InterviewAcerAdminLogin.Common.ResponseClasses;
using InterviewAcerAdminLogin.Repository;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace InterviewAcerAdminLogin.Service
{
    public class LoginService
    {
        private readonly LoginRepository _loginrepository;
        public LoginService()
        {
            _loginrepository = new LoginRepository();
        }

        public async Task<LoginResponse> Login(string userName, string password)
        {
            KeyValuePair<string, string>[] keyValueArray = new KeyValuePair<string, string>[3];
            keyValueArray[0] = new KeyValuePair<string, string>("grant_type", "password");
            keyValueArray[1] = new KeyValuePair<string, string>("username", userName);
            keyValueArray[2] = new KeyValuePair<string, string>("password", password);
            var response =  await _loginrepository.PostFormEncodedContent("token", keyValueArray);
            if (response.StatusCode == HttpStatusCode.OK)
            {
               return await response.Content.ReadAsAsync<LoginResponse>(new[] { new JsonMediaTypeFormatter() });
            }
            else
            {
                return null;
            }
        }
    }
}
