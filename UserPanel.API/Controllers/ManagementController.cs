using Microsoft.AspNetCore.Mvc;
using UserPanel.Core.Models;
using UserPanel.Core.Services.Manager;

namespace UserPanel.API.Controllers
{
    [Route("api/manager")]
    [ApiController]
    public class ManagementController : Controller
    {
        private readonly IManagerService _service;
        public ManagementController(IManagerService service)
        {
            _service = service;
        }

        [HttpPut]
        [Route("update-user")]
        public async Task<IActionResult> Update([FromBody] AppUser user)
        {
            await _service.UpdateUser(user);

            return Ok(new
            {
                Success = true,
                Message = "Success"
            });
        }
    }
}
