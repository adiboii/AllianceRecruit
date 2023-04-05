using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseCode.Data.Models
{
    public class Instructor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("InstructorId")]
        public int Id { get; set; }

        [Column("FirstName", TypeName = "varchar(250)")]
        public string FirstName { get; set; }

        [Column("LastName", TypeName = "varchar(250)")]
        public string LastName { get; set; }

        [Column("Address", TypeName = "varchar(500)")]
        public string Address { get; set; }

        [Column("PhoneNumber", TypeName = "varchar(11)")]
        public string PhoneNumber { get; set; }

        [Column("DateHired")]
        public DateTime DateHired { get; set; }

        [Column("Salary")]
        public float Salary { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }
    }
}
