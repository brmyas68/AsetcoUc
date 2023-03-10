

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UC.ClassDomain.Domains;

namespace UC.FluentAPI.Configures
{
    public class ConfigureModelCar : IEntityTypeConfiguration<ModelCar>
    {
        public void Configure(EntityTypeBuilder<ModelCar> builder)
        {
            builder.HasKey(M => M.ModelCar_ID);
            builder.Property(M => M.ModelCar_ID).IsRequired();
            builder.Property(M => M.ModelCar_Name).HasMaxLength(200).IsRequired();
            builder.Property(M => M.ModelCar_BrandCarID).IsRequired();
        }
    }
}
