namespace Catalog.Products.Features.GetProducts;

public sealed record GetProductsQuery()
    : IQuery<GetProductsResult>;

public sealed record GetProductsResult(IEnumerable<ProductDto> Products);

public class GetProductsHandler(CatalogDbContext dbContext)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(
        GetProductsQuery query, 
        CancellationToken cancellationToken)
    {
        var products = await dbContext.Products
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

        var productDtos = products.Adapt<List<ProductDto>>();

        return new GetProductsResult(productDtos);
    }
}