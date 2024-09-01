namespace Catalog.Products.Models;

public class Product : Aggregate<Guid>
{
    public string Name { get; private set; } = default!;
    public List<string> Categories { get; private set; } = new();
    public string Description { get; private set; } = default!;
    public string ImageFile { get; private set; } = default!;
    public decimal Price { get; private set; }

    public static Product Create(
        Guid id,
        string name,
        List<string> categories,
        string description,
        string imageFile,
        decimal price)
    {
       ArgumentException.ThrowIfNullOrEmpty(name);
       ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

       var product = new Product
       {
           Id = id,
           Name = name,
           Categories = categories,
           Description = description,
           ImageFile = imageFile,
           Price = price
       };

       product.AddDomainEvent(new ProductCreatedEvent(product));
       
       return product;
    }

    public void Update(
        string name,
        List<string> categories,
        string description,
        string imageFile,
        decimal price)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        Name = name;
        Categories = categories;
        Description = description;
        ImageFile = imageFile;

        if (Price != price)
        {
            Price = price;
            AddDomainEvent(new ProductPriceChangedEvent(this));
        }
    }
}