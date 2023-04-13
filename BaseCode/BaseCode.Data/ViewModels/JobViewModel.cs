using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCode.Data.ViewModels
{
    public class JobViewModel
    {
        [JsonProperty("job_title")]
        public string JobTitle { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("job_requirement")]
        public List<string> JobRequirements { get; set; }

        [JsonProperty("job_description")]
        public List<string> JobDescriptions { get; set; }
    }
}
