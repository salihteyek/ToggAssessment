using ManagementPanel.Core.Models;

namespace ManagementPanel.Core.Services
{
    public interface IPanelUserService
    {
        Task<PanelUser> GetUserByIdAsync(string id);
        Task<IEnumerable<PanelUser>> GetUsersAsync();
        Task SaveRegisteredUserAsync(PanelUser entity);
        Task EditPanelUser(PanelUser entity);
        Task SendEditedPanelUser(PanelUser user);
    }
}
