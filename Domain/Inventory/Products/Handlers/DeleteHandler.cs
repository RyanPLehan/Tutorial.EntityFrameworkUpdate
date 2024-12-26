using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products.Requests;
using System.Collections.Immutable;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products.Handlers;

internal class DeleteHandler : IRequestHandler<Delete>
{
    private readonly IProductRepository _repository;

    public DeleteHandler(IProductRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task Handle(Delete request, CancellationToken cancellationToken)
    {
        if (request.Product != null)
            await _repository.Delete(request.Product, cancellationToken);
    }
}
