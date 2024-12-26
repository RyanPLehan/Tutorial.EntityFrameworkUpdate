using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products.Requests;
using System.Collections.Immutable;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products.Handlers;

internal class GetByCategoryHandler : IRequestHandler<GetByCategory, ImmutableArray<Product>>
{
    private readonly IProductRepository _repository;

    public GetByCategoryHandler(IProductRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<ImmutableArray<Product>> Handle(GetByCategory request, CancellationToken cancellationToken)
    {
        return await _repository.GetByCategory(request.CategoryId, cancellationToken);
    }
}
