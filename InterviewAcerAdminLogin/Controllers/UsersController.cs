using InterviewAcerAdminLogin.Models;
using InterviewAcerAdminLogin.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InterviewAcerAdminLogin.Controllers
{
    public class UsersController : Controller
    {
        private readonly TokenContainer _tokenContainer;
        private readonly CommonService _commonService;
        List<UserGeneralDetails> userDetails = new List<UserGeneralDetails>();

        public UsersController()
        {
            _tokenContainer = new TokenContainer();
            _commonService = new CommonService();
        }


        // GET: Users
        public async Task<ActionResult> Index()
        {
            await GetUserDetails();
            return View(userDetails);
        }

        private async Task<List<UserGeneralDetails>> GetUserDetails()
        {
            HttpResponseMessage responseMessage = await _commonService.GetAllUsersGeneralInformation(_tokenContainer.ApiToken.ToString());
            if (responseMessage.IsSuccessStatusCode)
            {
                var userDetailsFromResponse = await responseMessage.Content.ReadAsAsync<List<UserGeneralDetails>>(new[] { new JsonMediaTypeFormatter() });
                if (userDetailsFromResponse != null && userDetailsFromResponse.Any())
                {
                    userDetails.AddRange(userDetailsFromResponse);
                }
            }
            return userDetails;
        }


        public async Task<ActionResult> GetAllUserDetails()
        {
            await GetUserDetails();
            return PartialView("_AllUserDetails", userDetails);
        }



        public async Task<ActionResult> GetUserInformation(string userId)
        {
            UserSpecificDetails userDetails = new UserSpecificDetails();
            HttpResponseMessage responseMessage = await _commonService.GetSpecificUserInformation(_tokenContainer.ApiToken.ToString(), userId);
            if (responseMessage.IsSuccessStatusCode)
            {
                userDetails = await responseMessage.Content.ReadAsAsync<UserSpecificDetails>(new[] { new JsonMediaTypeFormatter() });
            }
            ViewBag.baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"].ToString();
            return PartialView("~/Views/Shared/_UserSpecificDetails.cshtml", userDetails);
        }

        public async Task<ActionResult> GetProfilePicture(string imagePath)
        {
            try
            {
                HttpResponseMessage responseMessage = await _commonService.GetProfilePicture(_tokenContainer.ApiToken.ToString(), imagePath);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var byteArrayContent = await responseMessage.Content.ReadAsStreamAsync();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        byteArrayContent.CopyTo(ms);
                        string imgStr = Convert.ToBase64String(ms.ToArray());
                        var imageExtension = Path.GetExtension(imagePath).Substring(Path.GetExtension(imagePath).IndexOf(".") + 1);
                        return Content(string.Format("data:image/" + imageExtension + ";base64,{0}", imgStr));
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}