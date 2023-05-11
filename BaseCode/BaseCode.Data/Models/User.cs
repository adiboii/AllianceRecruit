using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;

namespace BaseCode.Data.Models
{
    public class User
    {
        [Key]
        [Column("ID")]
        public string ID { get; set; }

        [Column("UserID")]
        public string UserID { get; set; }

        [Column("FirstName", TypeName = "varchar(250)")]
        public string FirstName { get; set; }

        [Column("LastName", TypeName = "varchar(250)")]
        public string LastName { get; set; }

        [Column("Email", TypeName = "varchar(250)")]
        public string Email { get; set; }

        [Column("PhoneNumber", TypeName = "varchar(11)")]
        public string PhoneNumber { get; set; }

        [Column("Username", TypeName = "varchar(25)")]
        public string Username { get; set; }

        [Column("Password", TypeName = "varchar(25)")]
        public string Password { get; set; }

        [Column("ProfilePhoto")]
        public string ProfilePhoto { get; set; }

        [Column("CreatedBy")]
        public string CreatedBy { get; set; }

        [Column("CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [Column("ModifiedBy")]
        public string ModifiedBy { get; set; }

        [Column("ModifiedDate")]
        public DateTime? ModifiedDate { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }
    }
}