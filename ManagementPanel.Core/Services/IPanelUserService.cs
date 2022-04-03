using ManagementPanel.Core.Models;

namespace ManagementPanel.Core.Services
{
    public interface IPanelUserService
    {
        Task SaveRegisteredUserAsync(PanelUser entity);
    }
}
