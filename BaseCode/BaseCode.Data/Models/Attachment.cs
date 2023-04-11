using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BaseCode.Data.Models
{
    public class Attachment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("AttachmentId")]
        public int Id { get; set; }

        [Column("LinkedInProfile")]
        public string LinkedInProfile { get; set; }

        [Column("PortfolioUrl")]
        public string PortfolioUrl { get; set; }

        [Column("FormalPhoto")]
        public string FormalPhoto { get; set; }

        [Column("Resume")]
        public string Resume { get; set; }

    }
}
