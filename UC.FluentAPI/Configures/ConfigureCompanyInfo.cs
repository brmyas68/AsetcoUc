using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UC.ClassDomain.Domains;

namespace UC.FluentAPI.Configures
{
    public class ConfigureCompanyInfo : IEntityTypeConfiguration<CompanyInfo>
    {
        public void Configure(EntityTypeBuilder<CompanyInfo> builder)
        {
            builder.HasKey(C => C.CompanyInfo_ID);
            builder.Property(C => C.CompanyInfo_ID).IsRequired();
            builder.Property(C => C.CompanyInfo_TagID).IsRequired();
            builder.Property(C => C.CompanyInfo_Mobile).HasMaxLength(30).IsRequired();
            builder.Property(C => C.CompanyInfo_Email).HasMaxLength(50);
            builder.Property(C => C.CompanyInfo_Fax).HasMaxLength(30);
            builder.Property(C => C.CompanyInfo_Instagram).HasMaxLength(50);
            builder.Property(C => C.CompanyInfo_Phone).HasMaxLength(30);
            builder.Property(C => C.CompanyInfo_Address).HasMaxLength(300);
            builder.Property(C => C.CompanyInfo_Site).HasMaxLength(150).IsRequired();
            builder.Property(C => C.CompanyInfo_About).HasMaxLength(3000); // nvarchar(4000)(varchar(8000)
            builder.Property(C => C.CompanyInfo_SmsNumber).IsRequired();
            builder.Property(C => C.CompanyInfo_LanguageID);
            builder.Property(C => C.CompanyInfo_TypeDateTime).IsRequired();


            //builder.HasOne(L => L.Language)
            //.WithOne(C => C.CompanyInfo).HasForeignKey<CompanyInfo>(C => C.CompanyInfo_LanguageID);

        }

    }
}
