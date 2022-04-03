using Microsoft.AspNetCore.Identity;
using UserPanel.Shared.Enums;

namespace UserPanel.Core.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public bool Active { get; set; }
        public UserStatus UserStatus { get; set; }
    }
}
