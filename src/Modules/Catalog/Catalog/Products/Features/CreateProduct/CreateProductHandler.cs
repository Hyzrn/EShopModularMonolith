namespace Catalog.Products.Features.CreateProduct;

public sealed record CreateProductCommand(ProductDto Product) 
    : ICommand<CreateProductResult>;

public sealed record CreateProductResult(Guid Id);

public class CreateProductHandler(CatalogDbContext dbContext) 
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, 
        CancellationToken cancellationToken)
    {
        var product = CreateNewProduct(command.Product);
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }

    private Product CreateNewProduct(ProductDto productDto)
    {
        var product = Product.Create(
            Guid.NewGuid(),
            productDto.Name,
            productDto.Categories,
            productDto.Description,
            productDto.ImageFile,
            productDto.Price);

        return product;
    }
}