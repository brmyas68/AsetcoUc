using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UC.ClassDomain.Domains;

namespace UC.FluentAPI.Configures
{
    public class ConfigureCity : IEntityTypeConfiguration<City>
    {

        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(C => C.City_ID);
            builder.Property(C => C.City_ID).IsRequired();
            builder.Property(C => C.City_Name).HasMaxLength(100).IsRequired();
            builder.Property(C => C.City_Province_ID).IsRequired();

           // builder.HasOne(P => P.Province)
           // .WithMany(C => C.City).HasForeignKey(C => C.City_Province_ID);

           // builder.HasMany(U => U.User)
           //.WithOne(C => C.City);

        }

    }
}
