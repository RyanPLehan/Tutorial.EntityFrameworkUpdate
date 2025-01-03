using MediatR;
using System.Collections.Generic;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Application.Products.Requests;
using Tutorial.EntityFrameworkUpdate.Domain.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Repositories;

namespace Tutorial.EntityFrameworkUpdate.Application.Products.Handlers;

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
