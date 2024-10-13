namespace Catalog.Products.Features.GetProductById;

public sealed record GetProductByIdQuery(Guid Id)
    : IQuery<GetProductByIdResult>;

public sealed record GetProductByIdResult(ProductDto Product);

public class GetProductByIdHandler(CatalogDbContext dbContext)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(
        GetProductByIdQuery query, 
        CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == query.Id, cancellationToken);

        if (product is null)
        {
            throw new Exception($"Product not found: {query.Id}");
        }

        var productDto = product.Adapt<ProductDto>();

        return new GetProductByIdResult(productDto);
    }
}