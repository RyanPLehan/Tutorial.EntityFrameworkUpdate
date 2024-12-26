using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products.Requests;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products.Handlers;

internal class DeleteByIdHandler : IRequestHandler<DeleteById>
{
    private readonly IProductRepository _repository;

    public DeleteByIdHandler(IProductRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task Handle(DeleteById request, CancellationToken cancellationToken)
    {
        await _repository.Delete(request.Id, cancellationToken);
    }
}
