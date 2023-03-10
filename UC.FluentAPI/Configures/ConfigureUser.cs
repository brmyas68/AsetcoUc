
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UC.ClassDomain.Domains;

namespace UC.FluentAPI.Configures
{
    public class ConfigureUser : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(U => U.User_ID);
            builder.Property(U => U.User_ID).IsRequired();
            builder.Property(U => U.User_FirstName).HasMaxLength(100).IsRequired();
            builder.Property(U => U.User_LastName).HasMaxLength(100).IsRequired();
            builder.Property(U => U.User_IdentifyNumber).HasMaxLength(30).IsRequired();
            builder.Property(U => U.User_Mobile).HasMaxLength(20).IsRequired();
            builder.Property(U => U.User_Email).HasMaxLength(100);
            builder.Property(U => U.User_Mobile).IsRequired();
            builder.Property(U => U.User_Tell).HasMaxLength(100);
            builder.Property(U => U.User_DateRegister).IsRequired();
            builder.Property(U => U.User_UserName).HasMaxLength(100).IsRequired();
            builder.Property(U => U.User_HashPassword).HasMaxLength(500).IsRequired();
            builder.Property(U => U.User_Province_ID).IsUnicode();
            builder.Property(U => U.User_City_ID).IsRequired();
            builder.Property(U => U.User_Address).HasMaxLength(500);
            builder.Property(U => U.User_IsActive).IsRequired();
            builder.Property(U => U.User_IsBlock).IsRequired();
            builder.Property(U => U.User_Gender).IsRequired();
            builder.Property(U => U.User_Description).HasMaxLength(1000);
            builder.Property(U => U.User_ShabaNumber).HasMaxLength(50);
            builder.Property(U => U.User_PostalCode).HasMaxLength(50);


            //builder.HasMany(UR => UR.UserRoles)
            //        .WithOne(U => U.User);

            //builder.HasMany(T => T.Tokens)
            //   .WithOne(U => U.User);

            //builder.HasMany(RUP => RUP.RoleUserPermissions)
            //   .WithOne(U => U.User);

            //builder.HasMany(LG => LG.Log)
            // .WithOne(U => U.User);

            //builder.HasOne(P => P.Province)
            //.WithMany(U => U.User).HasForeignKey(U => U.User_Province_ID);

            //builder.HasOne(C => C.City)
            //.WithMany(U => U.User).HasForeignKey(C => C.User_City_ID);


        }
    }
}
