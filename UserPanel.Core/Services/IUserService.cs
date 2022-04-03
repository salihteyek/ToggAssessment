using UserPanel.Core.Dtos;
using UserPanel.Shared.Dtos;

namespace UserPanel.Core.Services
{
    public interface IUserService
    {
        Task<Response<AppUserDto>> Me();
        Task<Response<AppUserDto>> Update(UpdateUserDto updateUserDto);
    }
}
