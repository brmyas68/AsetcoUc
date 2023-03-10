

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UC.ClassDomain.Domains;

namespace UC.FluentAPI.Configures
{
    public class ConfigureBrandCar : IEntityTypeConfiguration<BrandCar>
    {
        public void Configure(EntityTypeBuilder<BrandCar> builder)
        {
            builder.HasKey(B => B.BrandCar_ID);
            builder.Property(B => B.BrandCar_ID).IsRequired();
            builder.Property(B => B.BrandCar_Name).HasMaxLength(200).IsRequired();
            builder.Property(B => B.BrandCar_Type).IsRequired();
        }
    }
}
