using BaseCode.Data.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace BaseCode.Data.ViewModels
{
    public class ClassViewModel
    {
        [JsonProperty("class_code")]
        [Required(ErrorMessage = "Class Code is Required.")]
        public string ClassCode { get; set; }

        [JsonProperty("class_name ")]
        [Required(ErrorMessage = "Class Name is Required.")]
        public string ClassName { get; set; }

        [JsonProperty("duration_from")]
        [Required(ErrorMessage = "Duration:From is Required.")]
        public DateTime From { get; set; }

        [JsonProperty("duration_to")]
        [Required(ErrorMessage = "Duration:To is Required.")]
        public DateTime To { get; set; }

        [JsonProperty("subject_id")]
        [Required(ErrorMessage = "Subject Name is Required.")]
        public int SubjectId { get; set; }

        [JsonProperty("instructor_id")]
        [Required(ErrorMessage = "Instructor Id is Required.")]
        public int InstructorId { get; set; }

        [JsonProperty("room_number")]
        [Required(ErrorMessage = "Room number is Required.")]
        public string RoomNumber { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }
    }
}
