
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UC.ClassDomain.Domains;

namespace UC.FluentAPI.Configures
{
    public class ConfigureRoleUserPermission : IEntityTypeConfiguration<RoleUserPermission>
    {
        public void Configure(EntityTypeBuilder<RoleUserPermission> builder)
        {
            builder.HasKey(URP => URP.RoleUserPermission_ID);
            builder.Property(URP => URP.RoleUserPermission_ID).IsRequired();
            builder.Property(URP => URP.RoleUserPermission_RoleID_UserID).IsRequired();
            builder.Property(URP => URP.RoleUserPermission_RouteStructureID).IsRequired();
            builder.Property(URP => URP.RoleUserPermission_BitMerge).IsRequired();


           // builder.HasOne(U => U.User)
           //     .WithMany(URP => URP.RoleUserPermissions)
           // .HasForeignKey(UR => UR.RoleUserPermission_RoleID_UserID);

           // builder.HasOne(R => R.Role)
           //      .WithMany(URP => URP.RoleUserPermissions)
           //.HasForeignKey(UR => UR.RoleUserPermission_RoleID_UserID);


        }
    }
}
