using BaseCode.Data.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace BaseCode.Data.ViewModels
{
    public class InstructorViewModel
    {
        [JsonProperty("instructor_first_name")]
        [Required(ErrorMessage = "Instructor First Name is Required.")]
        public string FirstName { get; set; }

        [JsonProperty("instructor_last_name")]
        [Required(ErrorMessage = "Instructor Last Name is Required.")]
        public string LastName { get; set; }

        [JsonProperty("instructor_address")]
        public string Address { get; set; }

        [JsonProperty("instructor_phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("instructor_date_hired")]
        public DateTime DateHired { get; set; }

        [JsonProperty("instructor_salary")]
        public float Salary { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }
    }
}
