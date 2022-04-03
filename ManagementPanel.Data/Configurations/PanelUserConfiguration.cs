using ManagementPanel.Core.Enums;
using ManagementPanel.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagementPanel.Data.Configurations
{
    public class PanelUserConfiguration : IEntityTypeConfiguration<PanelUser>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PanelUser> builder)
        {
            builder.Property(x => x.FullName).HasMaxLength(250);
            builder.Property(x => x.Active).HasDefaultValue(false);
            builder.Property(x => x.UserStatus).HasDefaultValue(UserStatus.Pending);
        }
    }
}
