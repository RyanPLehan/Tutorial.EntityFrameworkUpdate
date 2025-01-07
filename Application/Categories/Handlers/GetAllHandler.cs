using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Application.Models;
using Tutorial.EntityFrameworkUpdate.Application.Categories.Requests;

namespace Tutorial.EntityFrameworkUpdate.Application.Categories.Handlers;

internal class GetAllHandler : IRequestHandler<GetAll, ImmutableArray<Category>>
{
    private readonly ICategoryRepository _repository;

    public GetAllHandler(ICategoryRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<ImmutableArray<Category>> Handle(GetAll request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(cancellationToken);
    }
}
