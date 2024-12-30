using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products.Requests;
using System.Collections.Immutable;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products.Handlers;

internal class FindByCategoryHandler : IRequestHandler<FindByCategory, ImmutableArray<Product>>
{
    private readonly IProductRepository _repository;

    public FindByCategoryHandler(IProductRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<ImmutableArray<Product>> Handle(FindByCategory request, CancellationToken cancellationToken)
    {
        return await _repository.FindByCategory(request.CategoryId, cancellationToken);
    }
}
