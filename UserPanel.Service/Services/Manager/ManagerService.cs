using Microsoft.AspNetCore.Identity;
using UserPanel.Core.Models;
using UserPanel.Core.Repositories;
using UserPanel.Core.Services.Manager;
using UserPanel.Core.UnitOfWork;

namespace UserPanel.Service.Services.Manager
{
    public class ManagerService : IManagerService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public ManagerService(UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task UpdateUser(AppUser appUser)
        {
            var user = await _userManager.FindByIdAsync(appUser.Id);
            user.Active = appUser.Active;
            user.UserStatus = appUser.UserStatus;

            await _userManager.UpdateAsync(user);
            await _unitOfWork.SaveAsync();
        }
    }
}
