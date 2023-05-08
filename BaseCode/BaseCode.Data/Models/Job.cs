using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace BaseCode.Data.Models
{
    public class Job
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("JobId")]
        public int Id { get; set; }

        [Column("JobTitle")]
        public string JobTitle { get; set; }

        [Column("Location")]
        public string Location { get; set; }
   
        public virtual ICollection<JobRequirement> JobRequirements { get; set; }
       
        public virtual ICollection<JobDescription> JobDescriptions { get; set; }
    }
}
