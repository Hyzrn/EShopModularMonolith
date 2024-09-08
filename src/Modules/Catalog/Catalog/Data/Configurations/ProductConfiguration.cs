namespace Catalog.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
        builder.Property(p => p.Categories).IsRequired();
        builder.Property(p => p.Description).HasMaxLength(200);
        builder.Property(p => p.ImageFile).HasMaxLength(100);
        builder.Property(p => p.Price).IsRequired();
    }
}