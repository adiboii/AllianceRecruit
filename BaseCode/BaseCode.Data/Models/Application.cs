using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace BaseCode.Data.Models
{
    public class Application
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ApplicationId")]
        public int Id { get; set; }
       
        [Column("JobId")]
        public int JobId { get; set; }
       
        [Column("PersonalInformationId")]
        public int PersonalInformationId { get; set; }

        [Column("AttachmentId")]
        public int AttachmentId { get; set; }

        [Column("Status")]
        public string Status { get; set; }

        [Column("DateTimeApplied")]
        public DateTime DateTimeApplied { get; set; }
        [ForeignKey("JobId")]
        [JsonIgnore]
        public virtual Job Job { get; set; }

        [ForeignKey("PersonalInformationId")]
        [JsonIgnore]
        public virtual PersonalInformation PersonalInformation { get; set; }

        [ForeignKey("AttachmentId")]
        [JsonIgnore]
        public virtual Attachment Attachment { get; set; }
    }
}
