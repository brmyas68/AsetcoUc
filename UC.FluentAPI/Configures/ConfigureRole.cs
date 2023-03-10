

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UC.ClassDomain.Domains;

namespace UC.FluentAPI.Configures
{
    public class ConfigureRole : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(R => R.Role_ID);
            builder.Property(R => R.Role_ID).IsRequired();
            builder.Property(R => R.Role_TagID).IsRequired();
            builder.Property(R => R.Role_TenantID).IsRequired();
            builder.Property(R => R.Role_IsAdmin).IsRequired();
            builder.Property(R => R.Role_IsSystemRole).IsRequired();
            builder.Property(R => R.Role_ReadOnly).IsRequired();

            //builder.HasMany(UR => UR.UserRoles)
            //    .WithOne(R => R.Role);

            //builder.HasMany(RUP => RUP.RoleUserPermissions)
            //    .WithOne(R => R.Role);
        }
    }
}
