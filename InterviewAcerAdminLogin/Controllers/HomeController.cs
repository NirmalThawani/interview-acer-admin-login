using InterviewAcerAdminLogin.Common.RequestClasses;
using InterviewAcerAdminLogin.Common.ResponseClasses;
using InterviewAcerAdminLogin.CustomAtribute;
using InterviewAcerAdminLogin.Models;
using InterviewAcerAdminLogin.Service;
using InterviewAcerAdminLogin.ViewModel;
using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InterviewAcerAdminLogin.Controllers
{
    public class HomeController : Controller
    {
        private readonly TokenContainer _tokenContainer;
        private readonly StageService _stageService;

        public HomeController()
        {
            _tokenContainer = new TokenContainer();
            _stageService = new StageService();
        }
        [Authentication]
        public ActionResult Index()
        {
            List<Stages> stageList = new List<Stages>();
            stageList.Add(new Stages() { StageId = 1, isActive = true, StageName = "Stage 1" });

            stageList.Add(new Stages() { StageId = 2, isActive = false, StageName = "Stage 2" });

            stageList.Add(new Stages() { StageId = 3, isActive = false, StageName = "Stage 3" });
            return View(stageList);
        }


        public async Task<PartialViewResult> GetStages(int interviewType)
        {
            List<Stages> stageList = new List<Stages>();
            var stages = await _stageService.GetStages(interviewType, _tokenContainer.ApiToken.ToString());
            var groups = await _stageService.GetGroups(4, _tokenContainer.ApiToken.ToString());
            if (stages != null && stages.Any())
            {
                stages = stages.OrderBy(x => x.Sequence).ToList();
                foreach (var stage in stages)
                {
                    stageList.Add(new Stages() { StageId = stage.StageId, StageName = stage.Name });
                }
                stageList[0].isActive = true;
            }
            return PartialView("~/Views/Shared/_InterviewStages.cshtml", stageList);
        }

        public async Task<PartialViewResult> GetGroups(int stageId)
        {
            List<GroupDetails> groups;
            groups = await _stageService.GetGroups(stageId, _tokenContainer.ApiToken.ToString());
            if (groups != null && groups.Any())
            {
                groups = groups.OrderBy(x => x.Sequence).ToList();
            }
            else
            {
                groups = new List<GroupDetails>();
            }
            return PartialView("~/Views/Shared/_StageGroups.cshtml", groups);
        }

        public async Task<bool> AddGroup(SaveGroupRequest saveGroup)
        {
            var response = await _stageService.AddGroup(saveGroup, _tokenContainer.ApiToken.ToString());
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public void UploadExcelsheet()
        //{
        //    if (Request.Files.Count > 0)
        //    {
        //        var file = Request.Files[0];
        //        //List<ProductModel> _lstProductMaster = new List<ProductModel>();
        //        string filePath = string.Empty;
        //        if (file.ContentType == "application/vnd.ms-excel" || file.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        //        {
        //            try
        //            {
        //                string path = Server.MapPath("~/CheckListExcelDocuments/");
        //                if (!Directory.Exists(path))
        //                {
        //                    Directory.CreateDirectory(path);
        //                }
        //                filePath = path + Path.GetFileName("ProductUploadSheet-" + DateTime.Now.ToString("dd-MMM-yyyy-HH-mm-ss-ff") + Path.GetExtension(file.FileName));
        //                string extension = Path.GetExtension("ProductUploadSheet-" + DateTime.Now.ToString("dd-MMM-yyyy-HH-mm-ss-ff") + Path.GetExtension(file.FileName));
        //                file.SaveAs(filePath);
        //                var connectionString = "";
        //                switch (extension)
        //                {
        //                    case ".xls": //Excel 97-03.
        //                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", filePath);
        //                        break;
        //                    case ".xlsx": //Excel 07 and above.
        //                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", filePath);
        //                        break;
        //                }

        //                var excelFile = new ExcelQueryFactory(filePath);
        //                var artistAlbums = from a in excelFile.Worksheet("Sheet1") select a;

        //                foreach (var a in artistAlbums)
        //                {

        //                }

        //                //using (OleDbConnection connExcel = new OleDbConnection(connectionString))
        //                //{
        //                //    using (OleDbCommand cmdExcel = new OleDbCommand())
        //                //    {
        //                //        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
        //                //        {
        //                //            DataTable dt = new DataTable();
        //                //            cmdExcel.Connection = connExcel;

        //                //            Get the name of First Sheet.
        //                //            connExcel.Open();
        //                //            DataTable dtExcelSchema;
        //                //            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //                //            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        //                //            connExcel.Close();

        //                //            Read Data from First Sheet.
        //                //            connExcel.Open();
        //                //            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
        //                //            odaExcel.SelectCommand = cmdExcel;
        //                //            odaExcel.Fill(dt);
        //                //            connExcel.Close();
        //                //            if (dt.Rows.Count > 0)
        //                //            {

        //                //            }

        //                //        }
        //                //    }
        //                //}
        //            }
        //            catch (Exception e)
        //            {
        //                throw e;
        //            }

        //        }
        //    }
        //}
    }
}