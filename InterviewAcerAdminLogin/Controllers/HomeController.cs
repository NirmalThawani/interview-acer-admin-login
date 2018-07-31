using InterviewAcerAdminLogin.Common.RequestClasses;
using InterviewAcerAdminLogin.Common.ResponseClasses;
using InterviewAcerAdminLogin.CustomAtribute;
using InterviewAcerAdminLogin.Models;
using InterviewAcerAdminLogin.Service;
using InterviewAcerAdminLogin.ViewModel;
using LinqToExcel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        public static string ExcelContentType
        {
            get
            { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; }
        }

        public static byte[] ExportExcel(DataTable dataTable, bool isTemplateOnly)
        {
            byte[] result = null;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(String.Format("Sheet1"));
                //int startRowFrom = String.IsNullOrEmpty(heading) ? 1 : 3;
                workSheet.Cells["A1"].Value = "CheckListId";
                workSheet.Cells["B1"].Value = "CheckListDescription";
                workSheet.Cells["C1"].Value = "CheckListScore";
                workSheet.Column(1).AutoFit();
                workSheet.Column(2).AutoFit();
                workSheet.Column(3).AutoFit();

                // format header - bold, yellow on black  
                using (ExcelRange r = workSheet.Cells[1, 1, 1, 3])
                {
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Bold = true;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
                }

                var startRowFrom = 2;
                // autofit width of cells with small content  
                if (!isTemplateOnly)
                {
                    // add the content into the Excel file  
                    workSheet.Cells["A" + startRowFrom.ToString()].LoadFromDataTable(dataTable, false);
                    int columnIndex = 1;
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        ExcelRange columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex, workSheet.Dimension.End.Row, columnIndex];
                        int maxLength = columnCells.Max(cell => cell.Value.ToString().Count());
                        if (maxLength < 150)
                        {
                            workSheet.Column(columnIndex).AutoFit();
                        }


                        columnIndex++;
                    }

                    // format cells - add borders  
                    using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom + dataTable.Rows.Count, dataTable.Columns.Count])
                    {
                        r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                    }
                }





                result = package.GetAsByteArray();
            }
            return result;
        }

        public static DataTable ListToDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dataTable = new DataTable();

            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }

            object[] values = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }

                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public async Task<ActionResult> GetCheckListExcel(int groupId, bool isTemplateOnly)
        {
            byte[] filecontent;
            if (isTemplateOnly)
            {
                filecontent = ExportExcel(new DataTable(), isTemplateOnly);
            }
            else
            {
                var checkListDTOList = await _stageService.GetCheckList(groupId, _tokenContainer.ApiToken.ToString());
                if (checkListDTOList != null && checkListDTOList.Any())
                    filecontent = ExportExcel(ListToDataTable(checkListDTOList), isTemplateOnly);
                else
                    filecontent = ExportExcel(new DataTable(), true);
            }
            return File(filecontent, ExcelContentType, "CheckListData.xlsx");
        }


        //public async Task<ActionResult> GetCheckListExcel(int groupId, bool isTemplateOnly)
        //{
        //    try
        //    {

        //        var checkListDTOList = await _stageService.GetCheckList(groupId, _tokenContainer.ApiToken.ToString());

        //        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
        //        excel.Workbooks.Add();

        //        // Create Worksheet from active sheet
        //        Microsoft.Office.Interop.Excel._Worksheet workSheet = excel.ActiveSheet;
        //        string fileFullPath = string.Empty;
        //        writeLog("excel created");

        //        try
        //        {
        //            // ------------------------------------------------
        //            // Creation of header cells
        //            // ------------------------------------------------
        //            workSheet.Cells[1, "A"] = "CheckListId";
        //            workSheet.Cells[1, "B"] = "CheckListDescription";
        //            workSheet.Cells[1, "C"] = "CheckListScore";

        //            writeLog("headers addded");

        //            // ------------------------------------------------
        //            // Populate sheet 
        //            // ------------------------------------------------
        //            if (checkListDTOList != null && !isTemplateOnly)
        //            {
        //                int row = 2; // start row (in row 1 are header cells)
        //                foreach (CheckLisDetails checkListItem in checkListDTOList)
        //                {
        //                    workSheet.Cells[row, "A"] = checkListItem.CheckListId;
        //                    workSheet.Cells[row, "B"] = checkListItem.Name;
        //                    workSheet.Cells[row, "C"] = checkListItem.Points;

        //                    row++;
        //                }
        //            }
        //            writeLog("data added");

        //            // Apply some predefined styles for data to look nicely
        //            workSheet.Range["A1"].AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic1);

        //            writeLog("data formatted");
        //            // Define filename
        //            fileFullPath = Server.MapPath("~/Content/CheckListExcelDocuments/ExcelData" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss")+".xlsx");

        //            writeLog("excel saved");

        //            // Save this data as a file
        //            workSheet.SaveAs(fileFullPath);


        //            return File(fileFullPath, "application/vnd.ms-excel", "CheckListData.xlsx");
        //        }
        //        catch (Exception exception)
        //        {
        //            throw exception;
        //        }
        //        finally
        //        {
        //            // Quit Excel application
        //            excel.Quit();

        //            // Release COM objects (very important!)
        //            if (excel != null)
        //                System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);

        //            if (workSheet != null)
        //                System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);

        //            // Empty variables
        //            excel = null;
        //            workSheet = null;
        //            GC.Collect();
        //            GC.WaitForPendingFinalizers();
        //        }
        //    }
        //    catch(Exception e)
        //    {
        //        throw e;
        //    }

        //}

        //private void writeLog(string ex)
        //{


        //    StreamWriter log;

        //    string _SitePath = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/Content/Log/"));
        //    if(!Directory.Exists(_SitePath))
        //    {
        //        Directory.CreateDirectory(_SitePath);
        //    }
        //    _SitePath = _SitePath + "log.txt";
        //    if (!System.IO.File.Exists(_SitePath))
        //    {
        //        log = new StreamWriter(_SitePath, true);
        //    }
        //    else
        //    {
        //        log = System.IO.File.AppendText(_SitePath);
        //    }

        //    log.WriteLine(DateTime.Now);

        //    log.WriteLine(ex);

        //    log.WriteLine();

        //    log.Close();
        //}


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

        public async Task<bool> UploadExcelsheet()
        {
            bool uploadWasSuccessfull = false;
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                var selectedGroupId = Request["groupId"].ToString();
                //List<ProductModel> _lstProductMaster = new List<ProductModel>();
                string filePath = string.Empty;
                if (file.ContentType == "application/vnd.ms-excel" || file.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    try
                    {
                        int groupId = 0;
                        string path = Server.MapPath("~/Content/CheckListExcelDocuments/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        filePath = path + Path.GetFileName(Path.GetFileNameWithoutExtension(file.FileName) + "-" + DateTime.Now.ToString("dd-MMM-yyyy-HH-mm-ss-ff") + Path.GetExtension(file.FileName));
                        string extension = Path.GetExtension(Path.GetFileNameWithoutExtension(file.FileName) + "-" + DateTime.Now.ToString("dd-MMM-yyyy-HH-mm-ss-ff") + Path.GetExtension(file.FileName));
                        file.SaveAs(filePath);
                        var connectionString = "";
                        switch (extension)
                        {
                            case ".xls": //Excel 97-03.
                                connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", filePath);
                                break;
                            case ".xlsx": //Excel 07 and above.
                                connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", filePath);
                                break;
                        }

                        var excelFile = new ExcelQueryFactory(filePath);
                        var workSheets = excelFile.GetWorksheetNames();
                        var checkListItemSheet = from a in excelFile.Worksheet<CheckListExcelModel>(workSheets.First()) select a;
                        var checkListRequestList = new List<AddUpdateCheckList>();
                        foreach (var checkListItemRow in checkListItemSheet)
                        {

                            int checkListScore = 0;
                            int CheckListId = 0;
                            int.TryParse(checkListItemRow.CheckListId, out CheckListId);
                            if (int.TryParse(checkListItemRow.CheckListScore, out checkListScore) && !string.IsNullOrEmpty(checkListItemRow.CheckListDescription)
                                && !string.IsNullOrEmpty(checkListItemRow.CheckListScore) && int.TryParse(checkListItemRow.CheckListScore, out checkListScore) && int.TryParse(selectedGroupId, out groupId))
                            {
                                AddUpdateCheckList item = new AddUpdateCheckList()
                                {
                                    CheckListId = CheckListId,
                                    CheckListDescription = checkListItemRow.CheckListDescription,
                                    CheckListScore = checkListScore,
                                    GroupId = groupId
                                };
                                checkListRequestList.Add(item);
                            }

                        }

                        var response = await _stageService.AddUpdateCheckList(checkListRequestList, _tokenContainer.ApiToken.ToString());

                        if (response.IsSuccessStatusCode)
                        {
                            uploadWasSuccessfull = true;
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
            return uploadWasSuccessfull;
        }

        public async Task<bool> UpdateGroupName(int groupId, string groupName)
        {
            var response = await _stageService.UpdateGroupName(new UpdateGroupName() { GroupId = groupId, GroupName = groupName }, _tokenContainer.ApiToken.ToString());
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}