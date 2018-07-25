using Newtonsoft.Json;

namespace InterviewAcerAdminLogin.Common.ResponseClasses
{
    public class StageResponse
    {
        [JsonProperty("StageId")]
        public int StageId { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Sequence")]
        public int Sequence { get; set; }
    }
}
