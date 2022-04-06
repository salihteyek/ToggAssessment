using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserPanel.Core.Models;

namespace UserPanel.Data
{
    public class AppTestDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public AppTestDbContext()
        {
        }

        public AppTestDbContext(DbContextOptions<AppTestDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(builder);
        }
    }
}
