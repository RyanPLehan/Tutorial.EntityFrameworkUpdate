using MediatR;
using Tutorial.EntityFrameworkUpdate.Application.Products.Requests;
using Tutorial.EntityFrameworkUpdate.Application.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.Products.Handlers;

internal class PerformantUpdateHandler : IRequestHandler<PerformantUpdate, Product>
{
    private readonly IProductRepository _repository;

    public PerformantUpdateHandler(IProductRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<Product?> Handle(PerformantUpdate request, CancellationToken cancellationToken)
    {
        // For this particular scenerio, this will combine the ability of a non-tracked entity but have EF produce a partial UPDATE statement
        // This requires a small subset of fields that are able to be updated
        // Just like a Full UPDATE, all the fields must have their values (original or modified) pass in.
        // Unlike the Full UPDATE, the small subset clarifies what can be modified.


        var entity = new PerformantProductUpdate()
        {
            Id = request.Id,
            Description = request.Description,
            Price = request.Price,
            Quantity = request.Quantity,
        };

        return await _repository.PerformantUpdate(entity, cancellationToken);
    }
}
