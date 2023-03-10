

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UC.ClassDomain.Domains;

namespace UC.FluentAPI.Configures
{
    public class ConfigureRoleMembers : IEntityTypeConfiguration<RoleMembers>
    {
        public void Configure(EntityTypeBuilder<RoleMembers> builder)
        {
            builder.HasKey(RM => RM.RoleMembers_ID);
            builder.Property(RM => RM.RoleMembers_ID).IsRequired();
            builder.Property(RM => RM.RoleMembers_RoleID).IsRequired();
            builder.Property(RM => RM.RoleMembers_RoleMemberID).IsRequired();

        }
    }
}
