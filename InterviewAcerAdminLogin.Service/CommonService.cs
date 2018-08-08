using InterviewAcerAdminLogin.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InterviewAcerAdminLogin.Service
{
    public class CommonService
    {
        private readonly CommonRepository _commonRepository;
        public CommonService()
        {
            _commonRepository = new CommonRepository();
        }

        public async Task<HttpResponseMessage> GetAllUsersGeneralInformation(string token)
        {
            return await _commonRepository.GetRequest("api/Account/GetAllUsersGeneralInformation", "", token);
          
        }

        public async Task<HttpResponseMessage> GetSpecificUserInformation(string token, string userId)
        {
            return await _commonRepository.GetRequest("api/Account/GetUserSpecificInformation?userId=",userId, token);
        }


        public async Task<HttpResponseMessage> GetProfilePicture(string token, string imagePath)
        {
            return await _commonRepository.GetRequest("api/Account/GetProfilePicture?imagePath=", imagePath, token);
        }
    }
}
