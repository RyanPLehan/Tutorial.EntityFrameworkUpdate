using MediatR;
using Tutorial.EntityFrameworkUpdate.Application.Products.Requests;
using Tutorial.EntityFrameworkUpdate.Domain.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Repositories;

namespace Tutorial.EntityFrameworkUpdate.Application.Products.Handlers;

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
        // For this particular scenerio, one, two or all three properties are being updated.
        // The main point is even just one property is being updated, all three must be present.
        // At this level, the entity is NOT being tracked by Entity Framework.
        // Therefore, if we attempt to populate the entire Product entity, we will need to read in all its data and then overwrite the 3 properties.
        // Reading in all the data will also read in the Tags, which are not necessary
        // Therefore, we will let the repo implementation handle the work so that only the 3 fields are properly updated.

        return await _repository.Update(request.Id, request.Description, request.Price, request.Quantity, cancellationToken);
    }
}
