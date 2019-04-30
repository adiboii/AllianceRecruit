using Newtonsoft.Json;

namespace BaseCode.Data.ViewModels.Common
{
    public class StudentSearchViewModel
    {
        [JsonProperty("studID")]
        public string StudId { get; set; }

        [JsonProperty("studName")]
        public string StudName { get; set; }

        [JsonProperty("studEmail")]
        public string StudEmail { get; set; }

        [JsonProperty("studClass")]
        public string StudClass { get; set; }

        [JsonProperty("studEnrollYear")]
        public string StudEnrollYear { get; set; }

        [JsonProperty("studCity")]
        public string StudCity { get; set; }

        [JsonProperty("studCountry")]
        public string StudCountry { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("sortBy")]
        public string SortBy { get; set; }

        [JsonProperty("sortOrder")]
        public string SortOrder { get; set; }
    }
}
