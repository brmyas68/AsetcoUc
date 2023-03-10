using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UC.ClassDomain.Domains;

namespace UC.FluentAPI.Configures
{
    public class ConfigureLog : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(LG => LG.Log_ID);
            builder.Property(LG => LG.Log_ID).IsRequired();
            builder.Property(LG => LG.Log_UserID).IsRequired();
            builder.Property(LG => LG.Log_RouteStructureID).IsRequired();
            builder.Property(LG => LG.Log_DateTime).HasMaxLength(100).IsRequired();

            //builder.HasOne(U => U.User)
            // .WithMany(LG => LG.Log).HasForeignKey(LG => LG.Log_UserID);

            //builder.HasOne(ROS => ROS.RouteStructure)
            //.WithMany(LG => LG.Log).HasForeignKey(LG => LG.Log_RouteStructureID);


        }
    }
}
