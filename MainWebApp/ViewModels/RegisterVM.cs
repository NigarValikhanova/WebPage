using System.ComponentModel.DataAnnotations;

namespace MainWebApp.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Eyni deyil")]
        public string ConfirmPassword { get; set; }

    }
}
