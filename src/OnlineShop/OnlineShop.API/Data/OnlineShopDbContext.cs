using OnlineShop.API.Data.FluentConfigs;

namespace OnlineShop.API.Data;

public class OnlineShopDbContext : DbContext
{
    public OnlineShopDbContext(DbContextOptions<OnlineShopDbContext> options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfig());
    }
}
