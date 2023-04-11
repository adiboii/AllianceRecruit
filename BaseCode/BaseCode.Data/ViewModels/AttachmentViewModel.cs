using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BaseCode.Data.ViewModels
{
    public class AttachmentViewModel
    {
        [JsonProperty("linkedin_profile")]
        public string LinkedInProfile { get; set; }

        [JsonProperty("portfolio_url")]
        public string PortfolioUrl { get; set; }

        [JsonProperty("formal_photo")]
        public string FormalPhoto { get; set; }

        [JsonProperty("resume")]
        public string Resume { get; set; }
    }
}
