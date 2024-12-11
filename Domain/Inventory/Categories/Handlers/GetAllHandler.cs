using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Requests;
using System.Collections.Immutable;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Handlers;

internal class GetAllHandler : IRequestHandler<GetAllRequest, ImmutableArray<Category>>
{
    private readonly ICategoryRepository _repository;

    public GetAllHandler(ICategoryRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<ImmutableArray<Category>> Handle(GetAllRequest request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(cancellationToken);
    }
}
