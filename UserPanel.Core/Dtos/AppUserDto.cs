using UserPanel.Shared.Enums;

namespace UserPanel.Core.Dtos
{
    public class AppUserDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public UserStatus UserStatus { get; set; }
    }
}
