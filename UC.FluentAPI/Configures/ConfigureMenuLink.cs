using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UC.ClassDomain.Domains;

namespace UC.FluentAPI.Configures
{
    public class ConfigureMenuLink : IEntityTypeConfiguration<MenuLink>
    {
        public void Configure(EntityTypeBuilder<MenuLink> builder)
        {
            builder.HasKey(M => M.MenuLink_ID);
            builder.Property(M => M.MenuLink_ID).IsRequired();
            builder.Property(M => M.MenuLink_TagID).IsRequired();
            builder.Property(M => M.MenuLink_Icon).HasMaxLength(50);
            builder.Property(M => M.MenuLink_NavigationPath).HasMaxLength(300);
        }
    }
}
