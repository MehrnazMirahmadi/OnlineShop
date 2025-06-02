using OnlineShop.API.Helpers;

namespace OnlineShop.API.Data
{
    public class OnlineShopDbContext : DbContext
    {
        public OnlineShopDbContext(DbContextOptions<OnlineShopDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(x => x.Id)
                      .UseIdentityColumn(seed: 2, increment: 2);

                entity.Property(u => u.FirstName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.LastName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.NationalCode)
                      .IsRequired()
                      .HasMaxLength(10)
                      .IsFixedLength();
                //entity.HasQueryFilter(x => !x.IsDelete);
                entity.Property<bool>("IsDeleted");
                entity.HasQueryFilter(u => EF.Property<bool>(u, "IsDeleted") == false);
                entity.HasIndex(u => u.NationalCode).IsUnique();

                entity.Property(u => u.Password)
                      .IsRequired()
                      .HasMaxLength(256);
                

                entity.Property(u => u.PhoneNumber)
                      .IsRequired()
                      .HasMaxLength(15);
                entity.Property(x => x.IsActive)
                 .HasDefaultValue(true)
                  .HasConversion(
                     v => EncryptionHelper.Encrypt(v.ToString()),
                     v => bool.Parse(EncryptionHelper.Decrypt(v))
              )
             .HasMaxLength(10);
                entity.HasIndex(u => u.PhoneNumber).IsUnique();
            });
            #endregion

            #region City
            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Title)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasData(
                    new City { Id = 2, Title = "Tehran" },
                    new City { Id = 4, Title = "Isfahan" },
                    new City { Id = 6, Title = "Shiraz" },
                    new City { Id = 8, Title = "Tabriz" }
                );
            });
            #endregion
        }
    }
}
