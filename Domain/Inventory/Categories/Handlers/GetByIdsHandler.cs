using MediatR;
using System.Collections.Generic;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Requests;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Handlers;

internal class GetByIdsHandler : IRequestHandler<GetByIds, ImmutableArray<Category>>
{
    private readonly ICategoryRepository _repository;

    public GetByIdsHandler(ICategoryRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<ImmutableArray<Category>> Handle(GetByIds request, CancellationToken cancellationToken)
    {
        return await _repository.Get(request.Ids, cancellationToken);
    }
}
