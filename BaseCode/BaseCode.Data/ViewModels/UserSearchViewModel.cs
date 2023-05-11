using Newtonsoft.Json;

namespace BaseCode.Data.ViewModels.Common
{
    public class UserSearchViewModel
    {
        [JsonProperty("userID")]
        public string UserId { get; set; }

        [JsonProperty("userFirstName")]
        public string UserFirstName { get; set; }

        [JsonProperty("userLastName")]
        public string UserLastName { get; set; }

        [JsonProperty("userEmail")]
        public string UserEmail { get; set; }

        [JsonProperty("userPhoneNumber")]
        public string UserPhoneNumber { get; set; }

        [JsonProperty("userUsername")]
        public string UserUsername { get; set; }

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