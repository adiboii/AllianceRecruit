using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCode.Data.ViewModels
{
    public class AttachmentSearchViewModel
    {
        [JsonProperty("linkedinProfile")]
        public string LinkedInProfile { get; set; }

        [JsonProperty("portfolioUrl")]
        public string PortfolioUrl { get; set; }

        [JsonProperty("formalPhoto")]
        public string FormalPhoto { get; set; }

        [JsonProperty("resume")]
        public string Resume { get; set; }

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
