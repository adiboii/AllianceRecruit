using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BaseCode.Data.Models
{
    public class JobRequirement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("JobRequirementId")]
        public int Id { get; set; }

        [Column("JobId")]
        public int JobId { get; set; }

        [Column("JobDescription")]
        public string JobDescription { get; set; }
    }
}
