using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewAcerAdminLogin.Models
{
    public class InterviewDetails
    {
        public string CompanyName { get; set; }
        public int InterviewId { get; set; }
        public string Designation { get; set; }
        public DateTime InterviewDate { get; set; }
        public int InterviewTypeId { get; set; }
        public string HiringIndividualName { get; set; }
        public string CurrentStageName { get; set; }
        public int CurrentStageId { get; set; }
        public string Tag { get; set; }
        public int TotalNumberOfStages { get; set; }
        public string InterviewTypeName { get; set; }
        public int CompletedNumberOfStages { get; set; }
    }
}