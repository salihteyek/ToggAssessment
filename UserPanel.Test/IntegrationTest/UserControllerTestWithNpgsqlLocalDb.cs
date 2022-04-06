using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UserPanel.API.TestController;
using UserPanel.Core.Test.Model;
using UserPanel.Data;
using Xunit;

namespace UserPanel.Test.IntegrationTest
{
    public class UserControllerTestWithNpgsqlLocalDb : UserControllerTest
    {
        public UserControllerTestWithNpgsqlLocalDb()
        {
            UserManager<TestAppUser> userManager = new UserManager<TestAppUser>(null, null, null, null, null, null, null, null, null);
            
            var sqlCon = @"Host=localhost;Database=togguserpanel_testdb;Username=admin;Password=1";
            
            SetContextOptions(new DbContextOptionsBuilder<AppTestDbContext>().UseNpgsql(sqlCon).Options, userManager);
        }

        [Fact]
        public async Task Create_ModelValidUser_Successfull()
        {
            var newUser = new TestAppUser { Email = "salih1@teyek.com", UserName = "salihteyek1", FullName = "Salih Teyek 1", Active = false, UserStatus = Shared.Enums.UserStatus.Pending };
            using var context = new AppTestDbContext(_contextOptions);

            using UserStore<TestAppUser> _store = new UserStore<TestAppUser>(context);

            using UserManager<TestAppUser> _userManager = new UserManager<TestAppUser>(_store, null, new PasswordHasher<TestAppUser>(), null, null, null, null, null, null);
            
            var controller = new AuthTestController(context, _userManager);

            var result = await controller.Create(newUser);

            var okResult = Assert.IsType<OkObjectResult>(result);
        }
    }
}
