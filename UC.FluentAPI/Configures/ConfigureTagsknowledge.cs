

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UC.ClassDomain.Domains;

namespace UC.FluentAPI.Configures
{

    public class ConfigureTagsknowledge : IEntityTypeConfiguration<Tagsknowledge>
    {
        public void Configure(EntityTypeBuilder<Tagsknowledge> builder)
        {

            builder.HasKey(TGL => TGL.Tagsknowledge_ID);
            builder.Property(TGL => TGL.Tagsknowledge_ID).IsRequired();
            builder.Property(TGL => TGL.Tagsknowledge_ParentID).IsRequired();
            builder.Property(TGL => TGL.Tagsknowledge_Name).HasMaxLength(150).IsRequired();
            builder.Property(TGL => TGL.Tagsknowledge_Type).IsRequired();



            //builder.HasMany(TG => TG.TranslateTags)
            //    .WithOne(TGL => TGL.Tagsknowledge);

            //builder.HasMany(RUP => RUP.RoleUserPermission)
            //    .WithOne(TGL => TGL.Tagsknowledge);

            //builder.HasMany(ROS => ROS.RouteStructure)
            //    .WithOne(TGL => TGL.Tagsknowledge);

        }
    }
}
