using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UC.ClassDomain.Domains;

namespace UC.FluentAPI.Configures
{
    public  class ConfigureUserRole : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {

            builder.HasKey(UR => UR.UserRole_ID);
            builder.Property(UR => UR.UserRole_ID).IsRequired();
            builder.Property(UR => UR.User_ID).IsRequired();
            builder.Property(UR => UR.Role_ID).IsRequired();


            //builder.HasOne(U => U.User)
            //    .WithMany(UR => UR.UserRoles).HasForeignKey(UR => UR.User_ID);

            //builder.HasOne(R => R.Role)
            //    .WithMany(UR => UR.UserRoles).HasForeignKey(UR => UR.Role_ID);
        }

    }
}
