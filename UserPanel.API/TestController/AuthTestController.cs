using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserPanel.Core.Test.Model;
using UserPanel.Data;

namespace UserPanel.API.TestController
{
    public class AuthTestController : Controller
    {
        private readonly AppTestDbContext _context;
        private readonly UserManager<TestAppUser> _userManager;
        public AuthTestController(AppTestDbContext context, UserManager<TestAppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TestAppUser user)
        {
            await _userManager.CreateAsync(user);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Success = true,
            });
        }
    }
}
