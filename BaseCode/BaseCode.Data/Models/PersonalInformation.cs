using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BaseCode.Data.Models
{
    public class PersonalInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("PersonalInformationId")]
        public int Id { get; set; }

        [Column("FirstName", TypeName = "varchar(250)")]
        public string FirstName { get; set; }

        [Column("MiddleName", TypeName = "varchar(250)")]
        public string MiddleName { get; set; }

        [Column("LastName", TypeName = "varchar(250)")]
        public string LastName { get; set; }

        [Column("Sex")]
        public string Sex { get; set; }

        [Column("DateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [Column("Country")]
        public string Country { get; set; }

        [Column("Province")]
        public string Province { get; set; }

        [Column("ZipCode")]
        public string ZipCode { get; set; }
        
        [Column("AddressLine1")]
        public string AddressLine1 { get; set; }

        [Column("AddressLine2")]
        public string AddressLine2 { get; set; }

        [Column("EmailAddress")]
        public string EmailAddress { get; set; }

        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; }


    }
}
