using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Repositories;
using Tutorial.EntityFrameworkUpdate.Application.ProductTags.Requests;

namespace Tutorial.EntityFrameworkUpdate.Application.ProductTags.Handlers;

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
