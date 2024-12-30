using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.ProductTags.Requests;
using System.Collections.Immutable;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.ProductTags.Handlers;

internal class FindByProductHandler : IRequestHandler<FindByProduct, ImmutableArray<ProductTag>>
{
    private readonly IProductTagRepository _repository;

    public FindByProductHandler(IProductTagRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<ImmutableArray<ProductTag>> Handle(FindByProduct request, CancellationToken cancellationToken)
    {
        return await _repository.FindByProduct(request.ProductId, cancellationToken);
    }
}
