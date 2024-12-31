using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.ProductTags.Requests;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.ProductTags.Handlers;

internal class GetByIdHandler : IRequestHandler<GetById, ProductTag?>
{
    private readonly IProductTagRepository _repository;

    public GetByIdHandler(IProductTagRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<ProductTag?> Handle(GetById request, CancellationToken cancellationToken)
    {
        return await _repository.Get(request.ProductId, request.Id, cancellationToken);
    }
}
