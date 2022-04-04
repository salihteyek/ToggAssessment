using UserPanel.Core.Dtos;
using UserPanel.Core.Models;

namespace UserPanel.Core.Services
{
    public interface ITokenService
    {
        Task<TokenDto> CreateToken(AppUser userApp);
    }
}
