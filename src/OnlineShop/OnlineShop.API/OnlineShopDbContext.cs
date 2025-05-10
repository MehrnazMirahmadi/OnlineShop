namespace OnlineShop.API;

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

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100); // به دلخواه - مثلاً 100 کاراکتر

            entity.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(u => u.NationalCode)
                .IsRequired()
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasIndex(u => u.NationalCode)
                .IsUnique();

            entity.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(256); // اگر رمز هش می‌کنی، طول رو بیشتر در نظر بگیر

            entity.Property(u => u.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15); // برای پوشش حالت‌های +98 یا 0098

            entity.HasIndex(u => u.PhoneNumber)
                .IsUnique(); // اگر شماره تلفن باید یکتا باشد
        });
    }
}
