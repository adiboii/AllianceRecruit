using Newtonsoft.Json;

namespace BaseCode.Data.ViewModels.Common
{
    public class ClassSearchViewModel
    {
        [JsonProperty("page")]
        public int Page { get; set; }
    }
}
