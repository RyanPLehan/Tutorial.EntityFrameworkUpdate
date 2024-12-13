using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Requests;
using System.Collections.Immutable;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Handlers;

internal class GetByIdHandler : IRequestHandler<GetById, Category?>
{
    private readonly ICategoryRepository _repository;

    public GetByIdHandler(ICategoryRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<Category?> Handle(GetById request, CancellationToken cancellationToken)
    {
        return await _repository.Get(request.Id, cancellationToken);
    }
}
