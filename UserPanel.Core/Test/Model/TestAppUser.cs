using Microsoft.AspNetCore.Identity;
using UserPanel.Shared.Enums;

namespace UserPanel.Core.Test.Model
{
    public class TestAppUser : IdentityUser
    {
        public string FullName { get; set; }
        public bool Active { get; set; }
        public UserStatus UserStatus { get; set; }
    }
}
