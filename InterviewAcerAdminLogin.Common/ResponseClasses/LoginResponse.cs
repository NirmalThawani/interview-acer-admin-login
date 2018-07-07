using Newtonsoft.Json;

namespace InterviewAcerAdminLogin.Common.ResponseClasses
{
    public class LoginResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("Full Name")]
        public string FullName { get; set; }
        [JsonProperty("User Id")]
        public string UserId { get; set; }
        [JsonProperty("IsAdmin")]
        public string IsAdmin { get; set; }
    }
}
