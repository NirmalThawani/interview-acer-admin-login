using System.Collections.Generic;

namespace InterviewAcerAdminLogin.Common.ResponseClasses
{
    public class GroupDetails
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public List<CheckListDetails> GroupCheckList { get; set; }
        public int Sequence { get; set; }
    }

    public class CheckListDetails
    {
        public int CheckListId { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
    }
}
