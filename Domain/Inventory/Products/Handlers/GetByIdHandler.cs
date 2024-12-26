using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products.Requests;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products.Handlers;

internal class GetByIdHandler : IRequestHandler<GetById, Product?>
{
    private readonly IProductRepository _repository;

    public GetByIdHandler(IProductRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<Product?> Handle(GetById request, CancellationToken cancellationToken)
    {
        return await _repository.Get(request.Id, cancellationToken);

        // TODO: Append Tags
    }
}
