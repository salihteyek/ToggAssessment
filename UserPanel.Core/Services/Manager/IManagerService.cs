using UserPanel.Core.Models;

namespace UserPanel.Core.Services.Manager
{
    public interface IManagerService
    {
        Task UpdateUser(AppUser appUser);
    }
}
