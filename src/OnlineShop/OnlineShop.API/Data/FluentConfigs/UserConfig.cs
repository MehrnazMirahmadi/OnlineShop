using OnlineShop.API.Extensions;

namespace OnlineShop.API.Data.FluentConfigs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .ValueGeneratedOnAdd()
                   .UseIdentityColumn(seed: 2, increment: 2);

            builder.Property(u => u.NationalCode)
                .IsRequired()
                .HasConversion(
                    // Convert to DB: Before saving
                    v => NationalCodeHelper.NormalizeNationalCode(v),
                    // Convert from DB: After reading
                    v => NationalCodeHelper.NormalizeNationalCode(v)  // استفاده از Helper
                );

            builder.Property(u => u.FirstName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.LastName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.PhoneNumber)
                   .HasMaxLength(20);

            builder.Property(u => u.IsActive)
                   .IsRequired();

            builder.ToTable("Users");
        }
    }
}
