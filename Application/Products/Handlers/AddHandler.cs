using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Application.Models;
using Tutorial.EntityFrameworkUpdate.Application.Products.Requests;

namespace Tutorial.EntityFrameworkUpdate.Application.Products.Handlers;

internal class AddHandler : IRequestHandler<Add, Product>
{
    private readonly IProductRepository _repository;

    public AddHandler(IProductRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<Product> Handle(Add request, CancellationToken cancellationToken)
    {
        Product entity = new Product()
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Quantity = request.Quantity,
            CategoryId = request.CategoryId,
        };

        return await _repository.Add(entity, cancellationToken);
    }
}

