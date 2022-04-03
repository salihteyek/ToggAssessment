using UserPanel.Core.Dtos;
using UserPanel.Shared.Dtos;

namespace UserPanel.Core.Services
{
    public interface IAuthenticationService
    {
        Task<Response<TokenDto>> LoginAsync(LoginDto loginDto);
        Task<Response<AppUserDto>> RegisterAsync(RegisterDto registerDto);
    }
}
