using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BaseCode.Data.Models
{
    public class JobDescription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("JobDescriptionId")]
        public int Id { get; set; }

        [Column("JobId")]
        public int JobId { get; set; }

        [Column("Description")]
        public string Description { get; set; }
    }
}
