using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UserPanel.Core.Dtos;
using UserPanel.Core.Models;
using UserPanel.Core.Services;
using UserPanel.Service.Mapping;
using UserPanel.Shared.Dtos;

namespace UserPanel.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<Response<AppUserDto>> Me()
        {
            var user = await _userManager.FindByIdAsync(_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (user == null)
                return Response<AppUserDto>.Fail("User Not Found.", 400, true);
            return Response<AppUserDto>.Success(ObjectMapper.Mapper.Map<AppUserDto>(user), 200);
        }

        public async Task<Response<AppUserDto>> Update(UpdateUserDto updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (user == null)
                return Response<AppUserDto>.Fail("User Not Found.", 400, true);

            //user = ObjectMapper.Mapper.Map<AppUser>(updateUserDto);
            user.FullName = updateUserDto.FullName;
            user.Email = updateUserDto.Email;
            user.UserName = updateUserDto.UserName;

            var updatedUserResult = await _userManager.UpdateAsync(user);
            if (!updatedUserResult.Succeeded)
            {
                var errors = updatedUserResult.Errors.Select(x => x.Description).ToList();
                return Response<AppUserDto>.Fail(new ErrorDto(errors, true), 400);
            }

            if (!String.IsNullOrEmpty(updateUserDto.OldPassword) && !String.IsNullOrEmpty(updateUserDto.NewPassword)) { 
                var updatePasswordResult = await _userManager.ChangePasswordAsync(user, updateUserDto.OldPassword, updateUserDto.NewPassword);
                if (!updatePasswordResult.Succeeded)
                {
                    var errors = updatePasswordResult.Errors.Select(x => x.Description).ToList();
                    return Response<AppUserDto>.Fail(new ErrorDto(errors, true), 400);
                }
            }

            user = await _userManager.FindByIdAsync(_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            return Response<AppUserDto>.Success(ObjectMapper.Mapper.Map<AppUserDto>(user), 200);
        }
    }
}
