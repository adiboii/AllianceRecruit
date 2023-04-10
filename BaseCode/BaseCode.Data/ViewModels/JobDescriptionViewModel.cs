﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace BaseCode.Data.ViewModels
{
    public class JobDescriptionViewModel
    {
        [JsonProperty("job_id")]
        public int JobId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
