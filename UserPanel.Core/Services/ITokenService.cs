using UserPanel.Core.Dtos;
using UserPanel.Core.Models;

namespace UserPanel.Core.Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(AppUser userApp);
    }
}
