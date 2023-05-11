using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BaseCode.Data.ViewModels
{
    public class ApplicationSearchViewModel
    {
        [JsonProperty("jobId")]
        public string JobId { get; set; }

        [JsonProperty("personalInformationId")]
        public string PersonalInformationId { get; set; }

        [Column("attachmentId")]
        public string AttachmentId { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("remarks")]
        public string Remarks { get; set; }

        [Column("dateTimeApplied")]
        public DateTime DateTimeApplied { get; set; }

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
