
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UC.ClassDomain.Domains;


namespace UC.FluentAPI.Configures
{
    public class ConfigureSms : IEntityTypeConfiguration<Sms>
    {
        public void Configure(EntityTypeBuilder<Sms> builder)
        {
            builder.HasKey(S => S.Sms_ID);
            builder.Property(S => S.Sms_ID).IsRequired();
            builder.Property(S => S.Sms_ActiveCode).HasMaxLength(10).IsRequired();
           // builder.Property(S => S.Sms_Mobile).HasMaxLength(20).IsRequired();
            builder.Property(S => S.Sms_Time).HasMaxLength(5).IsRequired();
            builder.Property(S => S.Sms_IsActive).IsRequired();
            builder.Property(S => S.Sms_Status).IsRequired();
            builder.Property(S => S.Sms_MessageID).HasMaxLength(50).IsRequired();

        }
    }
}
