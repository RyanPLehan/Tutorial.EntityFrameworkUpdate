using System.Collections.Generic;
using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products.Requests;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products.Handlers;

internal class DeleteByIdsHandler : IRequestHandler<DeleteByIds>
{
    private readonly IProductRepository _repository;

    public DeleteByIdsHandler(IProductRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task Handle(DeleteByIds request, CancellationToken cancellationToken)
    {
        await _repository.Delete(request.Ids, cancellationToken);
    }
}
