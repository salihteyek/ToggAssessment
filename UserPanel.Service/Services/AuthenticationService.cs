using Microsoft.AspNetCore.Identity;
using UserPanel.Core.Dtos;
using UserPanel.Core.Models;
using UserPanel.Core.Services;
using UserPanel.Core.UnitOfWork;
using UserPanel.Service.Mapping;
using UserPanel.Shared.Dtos;
using UserPanel.Shared.Enums;

namespace UserPanel.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(ITokenService tokenService, UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<TokenDto>> LoginAsync(LoginDto loginDto)
        {
            if (loginDto == null)
                throw new ArgumentNullException(nameof(loginDto));

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return Response<TokenDto>.Fail("Email or Password is wrong", 400, true);

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Response<TokenDto>.Fail("Email or Password is wrong", 400, true);

            if (!user.Active)
                return Response<TokenDto>.Fail("Your account is not active", 400, true);

            if(user.UserStatus != UserStatus.Accept)
                return Response<TokenDto>.Fail(user.UserStatus == UserStatus.Decline ? "Your account has been blocked" : "Your account is awaiting approval", 400, true);

            var token = _tokenService.CreateToken(user);

            await _unitOfWork.SaveAsync();

            return Response<TokenDto>.Success(token, 200);
        }

        public async Task<Response<AppUserDto>> RegisterAsync(RegisterDto registerDto)
        {
            var user = new AppUser { Email = registerDto.Email, UserName = registerDto.UserName, FullName=registerDto.FullName, Active=false, UserStatus=Shared.Enums.UserStatus.Pending };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return Response<AppUserDto>.Fail(new ErrorDto(errors, true), 400);
            }
            return Response<AppUserDto>.Success(ObjectMapper.Mapper.Map<AppUserDto>(user), 200);
        }
    }
}
