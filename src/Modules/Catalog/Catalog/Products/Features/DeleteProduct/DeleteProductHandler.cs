namespace Catalog.Products.Features.DeleteProduct;

public sealed record DeleteProductCommand(Guid productId) 
    : ICommand<DeleteProductResult>;

public sealed record DeleteProductResult(bool IsSuccess);
    
public class DeleteProductHandler(CatalogDbContext dbContext) 
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, 
        CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .FindAsync(command.productId);

        if (product is null)
        {
            throw new Exception($"Product not found: {command.productId}");
        }

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}