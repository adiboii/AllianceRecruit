using Newtonsoft.Json;

namespace BaseCode.Data.ViewModels.Common
{
    public class SubjectSearchViewModel
    {
        [JsonProperty("page")]
        public int Page { get; set; }
    }
}
