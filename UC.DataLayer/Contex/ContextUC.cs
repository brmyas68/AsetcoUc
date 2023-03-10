

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UC.ClassDomain.Domains;
using UC.FluentAPI.Configures;


namespace UC.DataLayer.Contex
{
    public class ContextUC : DbContext
    {
        public DbSet<CompanyInfo> CompanyInfo { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<Tagsknowledge> Tagsknowledge { get; set; }
        public DbSet<TranslateTags> TranslateTags { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<RouteStructure> RouteStructure { get; set; }
        public DbSet<RoleUserPermission> RoleUserPermission { get; set; }
        public DbSet<Token> Token { get; set; }
        public DbSet<Sms> Sms { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<RoleMembers> RoleMembers { get; set; }
        public DbSet<MenuLink> MenuLinks { get; set; }
        public DbSet<BrandCar> BrandCar { get; set; }
        public DbSet<ModelCar> ModelCar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Change Table Name
            modelBuilder.Entity<CompanyInfo>().ToTable("UC_CompanyInfo");
            modelBuilder.Entity<Language>().ToTable("UC_Language");
            modelBuilder.Entity<Log>().ToTable("UC_Log");
            modelBuilder.Entity<Role>().ToTable("UC_Role");
            modelBuilder.Entity<RoleUserPermission>().ToTable("UC_RoleUserPermission");
            modelBuilder.Entity<RouteStructure>().ToTable("UC_RouteStructure");
            modelBuilder.Entity<Sms>().ToTable("UC_Sms");
            modelBuilder.Entity<Tagsknowledge>().ToTable("UC_Tagsknowledge");
            modelBuilder.Entity<Token>().ToTable("UC_Token");
            modelBuilder.Entity<TranslateTags>().ToTable("UC_TranslateTags");
            modelBuilder.Entity<User>().ToTable("UC_User");
            modelBuilder.Entity<UserRole>().ToTable("UC_UserRole");
            modelBuilder.Entity<Province>().ToTable("UC_Province");
            modelBuilder.Entity<City>().ToTable("UC_City");
            modelBuilder.Entity<RoleMembers>().ToTable("UC_RoleMembers");
            modelBuilder.Entity<MenuLink>().ToTable("UC_MenuLink");
            modelBuilder.Entity<BrandCar>().ToTable("UC_BrandCar");
            modelBuilder.Entity<ModelCar>().ToTable("UC_ModelCar");

            // Language
            modelBuilder.Entity<Language>().Property(L => L.Language_Rtl).HasDefaultValue(true);

            // Log
            modelBuilder.Entity<Log>().HasIndex(LG => LG.Log_UserID).HasDatabaseName("Index_LogUserID").IsUnique();
            modelBuilder.Entity<Log>().HasIndex(LG => LG.Log_RouteStructureID).HasDatabaseName("Index_LogRouteStructureID").IsUnique();

            // Token
            modelBuilder.Entity<Token>().HasIndex(T => T.Token_HashCode).HasDatabaseName("Index_HashCode"); //.IsUnique();
            modelBuilder.Entity<Token>().Property(T => T.Token_IsActive).HasDefaultValue(false);

            // Sms
            // modelBuilder.Entity<Sms>().HasIndex(S => S.Sms_Mobile).HasDatabaseName("Index_Mobile").IsUnique();
            modelBuilder.Entity<Sms>().HasIndex(S => S.Sms_ActiveCode).HasDatabaseName("Index_ActiveCode").IsUnique();
            modelBuilder.Entity<Sms>().Property(S => S.Sms_IsActive).HasDefaultValue(false);


            // RoleUserPermission
            // modelBuilder.Entity<RoleUserPermission>().HasIndex(RUP => RUP.RoleUserPermission_RoleID_UserID).HasDatabaseName("Index_RoleID_UserID").IsUnique();
            // modelBuilder.Entity<RoleUserPermission>().HasIndex(RUP => RUP.RoleUserPermission_RouteStructureID).HasDatabaseName("Index_RouteStructureID").IsUnique();
            modelBuilder.Entity<RoleUserPermission>().Property(RUP => RUP.RoleUserPermission_BitMerge).HasDefaultValue(true);


            // User
            // modelBuilder.Entity<User>().HasIndex(U => U.User_HashPassword).HasDatabaseName("Index_HashPassword");
            modelBuilder.Entity<User>().HasIndex(U => U.User_UserName).HasDatabaseName("Index_UserName").IsUnique();
            modelBuilder.Entity<User>().Property(U => U.User_IsActive).HasDefaultValue(false);


            //Set  Configuration
            modelBuilder.ApplyConfiguration(new ConfigureCompanyInfo());
            modelBuilder.ApplyConfiguration(new ConfigureLanguage());
            modelBuilder.ApplyConfiguration(new ConfigureLog());
            modelBuilder.ApplyConfiguration(new ConfigureRole());
            modelBuilder.ApplyConfiguration(new ConfigureRoleUserPermission());
            modelBuilder.ApplyConfiguration(new ConfigureRouteStructure());
            modelBuilder.ApplyConfiguration(new ConfigureSms());
            modelBuilder.ApplyConfiguration(new ConfigureTagsknowledge());
            modelBuilder.ApplyConfiguration(new ConfigureToken());
            modelBuilder.ApplyConfiguration(new ConfigureTranslateTags());
            modelBuilder.ApplyConfiguration(new ConfigureUser());
            modelBuilder.ApplyConfiguration(new ConfigureUserRole());
            modelBuilder.ApplyConfiguration(new ConfigureProvince());
            modelBuilder.ApplyConfiguration(new ConfigureCity());
            modelBuilder.ApplyConfiguration(new ConfigureRoleMembers());
            modelBuilder.ApplyConfiguration(new ConfigureMenuLink());
            modelBuilder.ApplyConfiguration(new ConfigureBrandCar());
            modelBuilder.ApplyConfiguration(new ConfigureModelCar());

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }

        public ContextUC()
        {
        }
        public ContextUC(DbContextOptions<ContextUC> options)
        : base(options)
        { }
 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var Configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json")
                                .Build();

            var SqlConnectionString = Configuration.GetConnectionString("AppDbUC");
            optionsBuilder.UseSqlServer(SqlConnectionString);

        }
    }
}
