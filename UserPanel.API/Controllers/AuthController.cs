using Microsoft.AspNetCore.Mvc;
using UserPanel.Core.Dtos;
using UserPanel.Core.Services;

namespace UserPanel.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _authenticationService.LoginAsync(loginDto);
            return ActionResultInstance(result);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            return ActionResultInstance(await _authenticationService.RegisterAsync(registerDto));
        }
    }
}
