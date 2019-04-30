using Newtonsoft.Json;
using System.Collections.Generic;

namespace BaseCode.Data.ViewModels.Common
{
    public class EmailViewModel
    {
        [JsonProperty(PropertyName = "to")]
        public List<string> To { get; set; }

        [JsonProperty(PropertyName = "cc")]
        public List<string> CC { get; set; }

        [JsonProperty(PropertyName = "bcc")]
        public List<string> BCC { get; set; }

        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }
    }
}
