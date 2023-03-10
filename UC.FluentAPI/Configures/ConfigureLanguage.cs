using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UC.ClassDomain.Domains;

namespace UC.FluentAPI.Configures
{
    public class ConfigureLanguage : IEntityTypeConfiguration<Language>
    {

        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasKey(L => L.Language_ID);
            builder.Property(L => L.Language_ID).IsRequired();
            builder.Property(L => L.Language_Name).HasMaxLength(20).IsRequired();
            builder.Property(L => L.Language_Icon).HasMaxLength(100).IsRequired();

           // builder.HasOne(C => C.CompanyInfo)
           //.WithOne(L => L.Language);

           // builder.HasMany(TG => TG.TranslateTags)
           //     .WithOne(L => L.Language);

        }
    }
}
