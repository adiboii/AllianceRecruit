using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseCode.Data.Models
{
    public class Subject : Base
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("SubjectId")]
        public int Id { get; set; }

        [Column("Name", TypeName = "varchar(30)")]
        public string Name { get; set; }

        [Column("Description", TypeName = "varchar(100)")]
        public string Description { get; set; }

        [Column("Category")]
        public string Category { get; set; }

        [Column("NumberOfCredits")]
        public int NumberOfCredits { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }
    }
}
