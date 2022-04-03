namespace UserPanel.Core.Dtos
{
    public class UpdateUserDto
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }

    }
}
