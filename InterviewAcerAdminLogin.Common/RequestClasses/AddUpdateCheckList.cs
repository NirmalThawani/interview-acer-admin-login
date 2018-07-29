using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewAcerAdminLogin.Common.RequestClasses
{
    public class AddUpdateCheckList
    {
        public int CheckListId { get; set; }
        public int GroupId { get; set; }
        public string CheckListDescription { get; set; }
        public int CheckListScore { get; set; }
    }
}
