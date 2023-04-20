using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace BaseCode.Data.Models
{
    public class JobDescription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("JobDescriptionId")]
        public int Id { get; set; }

        [ForeignKey("Job")]
        public int JobId { get; set; }

        [Column("Description")]
        public string Description { get; set; }
        [JsonIgnore]
        public virtual Job Job { get; set; }

        [JsonIgnore] // ignore this property during serialization
        public virtual ICollection<JobDescription> JobDescriptions { get; set; }
    }
}
