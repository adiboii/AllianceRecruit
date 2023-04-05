using BaseCode.Data.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace BaseCode.Data.ViewModels
{
    public class SubjectViewModel
    {
        [JsonProperty("subject_name")]
        [Required(ErrorMessage = "Subject Name is Required.")]
        public string Name { get; set; }

        [JsonProperty("subject_description")]
        public string Description { get; set; }

        [JsonProperty("subject_category")]
        [Required(ErrorMessage = "Category is Required.")]
        [RegularExpression("(?i)^(major|minor)$", ErrorMessage = "Category must be either major or minor.")]
        public string Category { get; set; }

        [JsonProperty("subject_number_of_credits")]
        [Range(1,5)]
        [Required(ErrorMessage = "Number of Credits is Required.")]
        public int NumberOfCredits { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }
    }
}
