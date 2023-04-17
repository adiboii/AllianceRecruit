using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BaseCode.Data.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [JsonProperty(PropertyName = "uname")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [JsonProperty(PropertyName = "confirm_pass")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [JsonProperty(PropertyName = "email")]
        public string EmailAddress { get; set; }

        [Required]
        [JsonProperty(PropertyName = "role")]
        public string RoleName { get; set; }
    }
}
