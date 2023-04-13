using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCode.Data.ViewModels
{
    public class ApplicationSearchViewModel
    {
        [JsonProperty("page")]
        public int Page { get; set; }
    }
}
