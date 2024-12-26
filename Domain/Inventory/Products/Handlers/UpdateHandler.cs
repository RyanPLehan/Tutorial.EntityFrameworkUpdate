using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products.Requests;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products.Handlers;

internal class UpdateHandler : IRequestHandler<Update, Product>
{
    private readonly IProductRepository _repository;

    public UpdateHandler(IProductRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<Product> Handle(Update request, CancellationToken cancellationToken)
    {
        // For this particular scenerio, only 2 properties are being updated.
        // At this level, the entity is NOT being tracked by Entity Framework.
        // Therefore, if we attempt to populate the entire Product entity, we will need to read in all its data and then overwrite the 2 properties.
        // Reading in all the data will also read in the Tags, which are not necessary
        // Therefore, we will let the repo implementation handle the work so that only the 2 fields are properly updated.

        return await _repository.Update(request.Id, request.Description, request.Price, request.Quantity, cancellationToken);
    }
}
