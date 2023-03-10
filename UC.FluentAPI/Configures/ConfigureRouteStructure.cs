
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UC.ClassDomain.Domains;

namespace UC.FluentAPI.Configures
{
    public class ConfigureRouteStructure : IEntityTypeConfiguration<RouteStructure>
    {
        public void Configure(EntityTypeBuilder<RouteStructure> builder)
        {
            builder.HasKey(RS => RS.RouteStructure_ID);
            builder.Property(RS => RS.RouteStructure_ID).IsRequired();
            builder.Property(RS => RS.RouteStructure_ParentID).IsRequired();
            builder.Property(RS => RS.RouteStructure_Tagsknowledge_ID).IsRequired();
            builder.Property(RS => RS.RouteStructure_TypeRoute).IsRequired();

            //builder.HasOne(TGL => TGL.Tagsknowledge)
            //    .WithMany(ROS => ROS.RouteStructure)
            //    .HasForeignKey(ROS => ROS.RouteStructure_Tagsknowledge_ID);

            //builder.HasMany(LG => LG.Log)
            //.WithOne(RS => RS.RouteStructure);

        }
    }
}
