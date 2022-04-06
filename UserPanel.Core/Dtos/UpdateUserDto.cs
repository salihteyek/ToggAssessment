using System.ComponentModel.DataAnnotations;

namespace UserPanel.Core.Dtos
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }

    }
}
