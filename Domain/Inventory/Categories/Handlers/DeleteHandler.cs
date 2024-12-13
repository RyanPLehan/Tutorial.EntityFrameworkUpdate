using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Requests;
using System.Collections.Immutable;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Handlers;

internal class DeleteHandler : IRequestHandler<Delete>
{
    private readonly ICategoryRepository _repository;

    public DeleteHandler(ICategoryRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task Handle(Delete request, CancellationToken cancellationToken)
    {
        if (request.Category != null)
            await _repository.Delete(request.Category);
    }
}
