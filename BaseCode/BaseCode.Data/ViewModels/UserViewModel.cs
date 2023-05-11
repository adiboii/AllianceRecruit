using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace BaseCode.Data.ViewModels
{
    public class UserViewModel
    {
        [JsonProperty("user_id")]
        public string UserID { get; set; }

        [JsonProperty("user_firstName")]
        [Required(ErrorMessage = "First Name is Required.")]
        public string FirstName { get; set; }

        [JsonProperty("user_lastName")]
        [Required(ErrorMessage = "Last Name is Required.")]
        public string LastName { get; set; }

        [JsonProperty("user_email")]
        [Required(ErrorMessage = "Email is Required.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [JsonProperty("user_phoneNumber")]
        [Required(ErrorMessage = "Phone Number is Required.")]
        public string PhoneNumber { get; set; }

        [JsonProperty("user_username")]
        [Required(ErrorMessage = "Username is Required.")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [JsonProperty(PropertyName = "user_password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [JsonProperty(PropertyName = "confirm_pass")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [JsonProperty("user_createdBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("user_createdDate")]
        public DateTime? CreatedDate { get; set; }

        [JsonProperty("user_modifiedBy")]
        public string ModifiedBy { get; set; }

        [JsonProperty("user_modifiedDate")]
        public DateTime? ModifiedDate { get; set; }

        [JsonProperty("user_isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("user_role")]
        public string RoleName { get; set; }
    }
}
