namespace Catalog.Products.Features.UpdateProduct;

public sealed record UpdateProductCommand(ProductDto Product)
    : ICommand<UpdateProductResult>;

public sealed record UpdateProductResult(bool isSuccess);
    
public class UpdateProductHandler(CatalogDbContext dbContext) 
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(
        UpdateProductCommand command, 
        CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .FindAsync(command.Product.Id, cancellationToken);

        if (product is null)
        {
            throw new Exception($"Product not found: {command.Product.Id}");
        }

        UpdateProductWithNewValues(product, command.Product);
        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }

    private void UpdateProductWithNewValues(Product product, ProductDto productDto)
    {
        product.Update(
            productDto.Name,
            productDto.Categories,
            productDto.Description,
            productDto.ImageFile,
            productDto.Price
            );
    }
}