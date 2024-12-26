using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products.Requests;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products.Handlers;

internal class DeleteByCategoryHandler : IRequestHandler<DeleteByCategory>
{
    private readonly IProductRepository _repository;

    public DeleteByCategoryHandler(IProductRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task Handle(DeleteByCategory request, CancellationToken cancellationToken)
    {
        await _repository.DeleteByCategory(request.CategoryId, cancellationToken);
    }
}
