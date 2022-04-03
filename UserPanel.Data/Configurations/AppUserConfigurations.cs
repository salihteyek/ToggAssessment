using Microsoft.EntityFrameworkCore;
using UserPanel.Core.Models;
using UserPanel.Shared.Enums;

namespace UserPanel.Data.Configurations
{
    public class AppUserConfigurations : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.FullName).HasMaxLength(250);
            builder.Property(x => x.Active).HasDefaultValue(false);
            builder.Property(x => x.UserStatus).HasDefaultValue(UserStatus.Pending);
        }
    }
}
