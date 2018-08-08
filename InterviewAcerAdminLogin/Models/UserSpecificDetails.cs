using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewAcerAdminLogin.Models
{
    public class UserSpecificDetails:UserGeneralDetails
    {
        public string Specialization { get; set; }
        public string AcadamicScore { get; set; }
        public int OngoingInterviews { get; set; }
        public int TotalNumberOfCheckListCompleted { get; set; }
        public int TotalNumberOfPointsScored { get; set; }
        public List<InterviewDetails> InterviewDetails { get; set; }
        public string imagePath { get; set; }
    }
}