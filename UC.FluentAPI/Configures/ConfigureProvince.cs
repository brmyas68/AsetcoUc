using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UC.ClassDomain.Domains;

namespace UC.FluentAPI.Configures
{
    public class ConfigureProvince : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.HasKey(P => P.Province_ID);
            builder.Property(P => P.Province_ID).IsRequired();
            builder.Property(P => P.Province_Name).HasMaxLength(100).IsRequired();

           // builder.HasMany(C=> C.City)
           //.WithOne(P => P.Province);

           // builder.HasMany(U => U.User)
           //     .WithOne(P => P.Province);

        }
    }
}
