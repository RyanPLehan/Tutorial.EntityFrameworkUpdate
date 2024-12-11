using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Requests;
using System.Collections.Immutable;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Handlers;

internal class GetHandler : IRequestHandler<GetRequest, Category?>
{
    private readonly ICategoryRepository _repository;

    public GetHandler(ICategoryRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<Category?> Handle(GetRequest request, CancellationToken cancellationToken)
    {
        return await _repository.Get(request.Id, cancellationToken);
    }
}
