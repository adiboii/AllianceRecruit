using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BaseCode.Data.ViewModels
{
    public class ApplicationViewModel
    {
        [JsonProperty("job_id")]
        public int JobId { get; set; }

        [JsonProperty("personal_information_id")]
        public int PersonalInformationId { get; set; }

        [Column("attachment_id")]
        public int AttachmentId { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("date_time_applied")]
        public DateTime DateTimeApplied { get; set; }
    }
}
