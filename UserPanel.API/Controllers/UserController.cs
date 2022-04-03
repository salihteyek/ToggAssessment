using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserPanel.Core.Dtos;
using UserPanel.Core.Services;

namespace UserPanel.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : CustomBaseController
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("me")]
        
        public async Task<IActionResult> Get()
        {
            var result = await _service.Me();
            return ActionResultInstance(result);
        }

        [HttpPut]
        [Route("me")]
        public async Task<IActionResult> Update(UpdateUserDto updateUserDto)
        {
            var result = await _service.Update(updateUserDto);
            return ActionResultInstance(result);
        }
    }
}
