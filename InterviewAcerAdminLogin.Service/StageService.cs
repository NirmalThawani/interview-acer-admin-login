using InterviewAcerAdminLogin.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using InterviewAcerAdminLogin.Common.ResponseClasses;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using InterviewAcerAdminLogin.Common.RequestClasses;

namespace InterviewAcerAdminLogin.Service
{
    public class StageService
    {
        private readonly StageRepository _stagerepository;
        public StageService()
        {
            _stagerepository = new StageRepository();
        }

        public async Task<List<StageResponse>> GetStages(int interviewTypeId, string token)
        {
           var response = await _stagerepository.GetRequest("api/GetStageDetails", "?interviewTypeId=" + interviewTypeId, token);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadAsAsync<List<StageResponse>>(new[] { new JsonMediaTypeFormatter() });
            }
            else
            {
                return null;
            }
        }

        public async Task<List<GroupDetails>> GetGroups(int stageId, string token)
        {
            var response = await _stagerepository.GetRequest("api/GetGroups", "?stageId=" + stageId, token);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadAsAsync<List<GroupDetails>>(new[] { new JsonMediaTypeFormatter() });
            }
            else
            {
                return null;
            }
        }

        public async Task<HttpResponseMessage> AddGroup(SaveGroupRequest saveGroupRequest, string token)
        {
           return await _stagerepository.SaveGroup(saveGroupRequest, token);
        }

        public async Task<HttpResponseMessage> AddUpdateCheckList(List<AddUpdateCheckList> requestObject, string token)
        {
            return await _stagerepository.SaveCheckList(requestObject, token);
        }

        public async Task<List<CheckLisDetails>> GetCheckList(int groupId, string token)
        {
            var response = await _stagerepository.GetRequest("api/GetCheckList", "?groupId=" + groupId, token);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadAsAsync<List<CheckLisDetails>>(new[] { new JsonMediaTypeFormatter() });
            }
            else
            {
                return null;
            }
        }
    }
}
