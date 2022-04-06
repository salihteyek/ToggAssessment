using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UserPanel.Core.Models;
using UserPanel.Core.Test.Model;
using UserPanel.Data;

namespace UserPanel.Test.IntegrationTest
{
    public class UserControllerTest
    {
        protected DbContextOptions<AppTestDbContext> _contextOptions { get; private set; }
        protected UserManager<TestAppUser> _userManager { get; set; }

        public async Task SetContextOptions(DbContextOptions<AppTestDbContext> contextOptions, UserManager<TestAppUser> userManager)
        {
            _contextOptions = contextOptions;
            _userManager = userManager;
            Seed();
        }

        public async Task Seed()
        {
            using var context = new AppTestDbContext(_contextOptions);

            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            var user = new TestAppUser() { Email = "salih@teyek.com", UserName = "salihteyek", FullName = "Salih Teyek", Active = false, UserStatus = Shared.Enums.UserStatus.Pending };
            await _userManager.CreateAsync(user, "St.123");

            await context.SaveChangesAsync();
        }
    }
}
