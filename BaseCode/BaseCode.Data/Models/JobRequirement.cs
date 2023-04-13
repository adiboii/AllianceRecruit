using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace BaseCode.Data.Models
{
    public class JobRequirement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("JobRequirementId")]
        public int Id { get; set; }

        [ForeignKey("Job")]
        public int JobId { get; set; }

        [Column("Requirement")]
        public string Requirement { get; set; }

        public virtual Job Job { get; set; }

        [JsonIgnore] // ignore this property during serialization
        public virtual ICollection<JobRequirement> JobRequirements { get; set; }

    }
}
