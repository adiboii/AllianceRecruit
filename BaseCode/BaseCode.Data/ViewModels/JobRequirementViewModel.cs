using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace BaseCode.Data.ViewModels
{
    public class JobRequirementViewModel
    {
        [JsonProperty("job_requirement")]
        public string Requirement { get; set; }
    }
}
