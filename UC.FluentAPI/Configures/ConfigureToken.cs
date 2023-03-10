using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UC.ClassDomain.Domains;

namespace UC.FluentAPI.Configures
{
    public class ConfigureToken : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {

            builder.HasKey(T => T.Token_ID);
            builder.Property(T => T.Token_ID).IsRequired();
            builder.Property(T => T.Token_HashCode).HasMaxLength(300).IsRequired();

            //builder.HasOne(U => U.User)
            //  .WithMany(T => T.Tokens).HasForeignKey(T => T.Token_UserID);

        }
    }
}
