using System.Collections.Generic;
using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Requests;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Handlers;

internal class DeleteByIdsHandler : IRequestHandler<DeleteByIds>
{
    private readonly ICategoryRepository _repository;

    public DeleteByIdsHandler(ICategoryRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task Handle(DeleteByIds request, CancellationToken cancellationToken)
    {
        await _repository.Delete(request.Ids, cancellationToken);
    }
}
