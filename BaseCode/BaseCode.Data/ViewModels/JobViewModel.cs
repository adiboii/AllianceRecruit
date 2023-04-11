using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCode.Data.ViewModels
{
    public class JobViewModel
    {
        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("job_requirements")]
        public List<string> JobRequirements { get; set; }

        [JsonProperty("job_descriptions")]
        public List<string> JobDescriptions { get; set; }
    }
}
