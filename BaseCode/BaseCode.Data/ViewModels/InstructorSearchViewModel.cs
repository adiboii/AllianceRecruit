using Newtonsoft.Json;

namespace BaseCode.Data.ViewModels.Common
{
    public class InstructorSearchViewModel
    {
        [JsonProperty("page")]
        public int Page { get; set; }
    }
}
