using MediatR;
using System.Collections.Generic;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products.Requests;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products.Handlers;

internal class GetByIdsHandler : IRequestHandler<GetByIds, ImmutableArray<Product>>
{
    private readonly IProductRepository _repository;

    public GetByIdsHandler(IProductRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<ImmutableArray<Product>> Handle(GetByIds request, CancellationToken cancellationToken)
    {
        return await _repository.Get(request.Ids, cancellationToken);
    }
}
