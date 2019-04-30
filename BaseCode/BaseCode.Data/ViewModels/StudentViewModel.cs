using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace BaseCode.Data.ViewModels
{
    public class StudentViewModel
    {
        [JsonProperty("stud_id")]
        public int StudentId { get; set; }

        [JsonProperty("stud_name")]
        [Required(ErrorMessage = "Student Name is Required.")]
        public string Name { get; set; }

        [JsonProperty("stud_email")]
        [Required(ErrorMessage = "Email is Required.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [JsonProperty("stud_class")]
        public string Class { get; set; }

        [JsonProperty("stud_enrollYear")]
        public string EnrollYear { get; set; }

        [JsonProperty("stud_city")]
        public string City { get; set; }

        [JsonProperty("stud_country")]
        public string Country { get; set; }

        [JsonProperty("stud_createdBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("stud_createdDate")]
        public DateTime? CreatedDate { get; set; }

        [JsonProperty("stud_modifiedBy")]
        public string ModifiedBy { get; set; }

        [JsonProperty("stud_modifiedDate")]
        public DateTime? ModifiedDate { get; set; }
    }
}
