using MediatR;

namespace Catalog.Products.Features.CreateProduct;

public sealed record CreateProductCommand(
        string Name,
        List<string> Categories,
        string Description,
        string ImagaFile,
        decimal Price) : IRequest<CreateProductResult>;

public sealed record CreateProductResult(Guid Id);

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    public Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        return null;
    }
}