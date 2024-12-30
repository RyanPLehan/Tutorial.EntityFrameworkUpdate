using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.ProductTags.Requests;
using System.Collections.Immutable;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.ProductTags.Handlers;

internal class AddHandler : IRequestHandler<Add, ProductTag>
{
    private readonly IProductTagRepository _repository;

    public AddHandler(IProductTagRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<ProductTag> Handle(Add request, CancellationToken cancellationToken)
    {
        ProductTag entity = new ProductTag()
        {
            ProductId = request.ProductId,
            Name = request.Name,
            Value = request.Value,
        };

        return await _repository.Add(entity, cancellationToken);
    }
}

