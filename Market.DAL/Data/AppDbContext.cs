using Market.DAL.Entities.Identity;
using Market.DAL.Entities.Market;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Market.DAL.Data
{
    public class AppDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Product> Products { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config.GetConnectionString("MAXSOFTShop")
                ?? throw new InvalidOperationException(
                    "Connection string 'MAXSOFTShop' not found.");

            optionsBuilder.UseSqlServer(connectionString, builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                builder.MigrationsAssembly("Market.WebAPI");
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Проверка на уникальность имени товара в рамках магазина

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Shop)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.ShopId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasIndex(p => new { p.Name, p.ShopId })
                .IsUnique();

            modelBuilder.Entity<Shop>()
                .HasOne(s => s.Manager)
                .WithOne()
                .HasForeignKey<Shop>(s => s.ManagerId)
                .IsRequired(false);
            modelBuilder.Entity<User>()
                .HasOne(u => u.Shop)
                .WithMany(s => s.Sellers)
                .HasForeignKey(u => u.ShopId)
                .IsRequired(false);

            modelBuilder.Entity<Role>().HasData(new Role { Id = 3, Name = "Seller", NormalizedName = "SELLER".ToUpper() });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 2, Name = "Manager", NormalizedName = "MANAGER".ToUpper() });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = "Admin", NormalizedName = "ADMIN".ToUpper() });

            var hasher = new PasswordHasher<User>();
            
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "freezedmail@gmail.com",
                NormalizedEmail = "FREEZEDMAIL@GMAIL.COM",
                PasswordHash = hasher.HashPassword(null, "masterpass"),
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                FullName = "Аутахунов Аширхан Адылжанович",
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 3,
                UserName = "seller",
                NormalizedUserName = "SELLER",
                Email = "seller@example.com",
                NormalizedEmail = "TEST1@EXAMPLE.COM",
                PasswordHash = hasher.HashPassword(null, "masterpass"),
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                FullName = "Селлер Селлеров Селлерович",
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 2,
                UserName = "manager",
                NormalizedUserName = "MANAGER",
                Email = "manager@example.com",
                NormalizedEmail = "manager@EXAMPLE.COM",
                PasswordHash = hasher.HashPassword(null, "masterpass"),
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                FullName = "Менеджер Менеджеров Менеджерович",
            });
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                RoleId = 1,
                UserId = 1,
            });
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                RoleId = 2,
                UserId = 2,
            });
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                RoleId = 3,
                UserId = 3,
            });
        }
    }
}