using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Requests;
using System.Collections.Immutable;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Handlers;

internal class DeleteByIdHandler : IRequestHandler<DeleteById>
{
    private readonly ICategoryRepository _repository;

    public DeleteByIdHandler(ICategoryRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task Handle(DeleteById request, CancellationToken cancellationToken)
    {
        var entity = await _repository.Get(request.Id);
        if (entity != null)
            await _repository.Delete(entity);
    }
}
