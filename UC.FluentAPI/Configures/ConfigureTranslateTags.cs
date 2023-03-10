

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UC.ClassDomain.Domains;

namespace UC.FluentAPI.Configures
{
    public class ConfigureTranslateTags : IEntityTypeConfiguration<TranslateTags>
    {
        public void Configure(EntityTypeBuilder<TranslateTags> builder)
        {
            builder.HasKey(TG => TG.TranslateTags_ID);
            builder.Property(TG => TG.TranslateTags_ID).IsRequired();
            builder.Property(TG => TG.TranslateTags_Text).HasMaxLength(200).IsRequired();
            builder.Property(TG => TG.TranslateTags_TagsknowledgeID).IsRequired();
            builder.Property(TG => TG.TranslateTags_LanguageID).IsRequired();


            //builder.HasOne(L => L.Language)
            //   .WithMany(TG => TG.TranslateTags).HasForeignKey(TG => TG.TranslateTags_LanguageID);

            //builder.HasOne(TGL => TGL.Tagsknowledge)
            //   .WithMany(TG => TG.TranslateTags).HasForeignKey(TG => TG.TranslateTags_TagsknowledgeID);
        }
    }
}
