using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewAcerAdminLogin.Common.RequestClasses
{
    public class UpdateGroupName
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }
}
