using MediatR;
using System.Collections.Generic;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Application.Categories.Requests;
using Tutorial.EntityFrameworkUpdate.Application.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.Categories.Handlers;

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
