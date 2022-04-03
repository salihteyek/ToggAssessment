using System.ComponentModel.DataAnnotations;

namespace UserPanel.Core.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "FullName is required")]
        [StringLength(50, ErrorMessage = "Maximum characters exceeded")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Wrong email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
