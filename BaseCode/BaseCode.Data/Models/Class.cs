using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseCode.Data.Models
{
    public class Class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ClassId")]
        public int Id { get; set; }

        [Column("ClassCode")]
        public string ClassCode { get; set; }

        [Column("ClassName")]
        public string ClassName { get; set; }

        [Column("DurationFrom")]
        public DateTime From { get; set; }

        [Column("DurationTo")]
        public DateTime To { get; set; }

        [Column("SubjectId")]
        public int SubjectId { get; set; }

        [Column("InstructorId")]
        public int InstructorId { get; set; }

        [Column("RoomNumber")]
        public string RoomNumber { get; set; }

        [ForeignKey("SubjectId")]
        [JsonIgnore]
        public virtual Subject Subject { get; set; }

        [ForeignKey("InstructorId")]
        [JsonIgnore]
        public virtual Instructor Instructor { get; set; }
    }
}
