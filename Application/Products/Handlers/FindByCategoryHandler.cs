using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Application.Models;
using Tutorial.EntityFrameworkUpdate.Application.Products.Requests;

namespace Tutorial.EntityFrameworkUpdate.Application.Products.Handlers;

internal class FindByCategoryHandler : IRequestHandler<FindByCategory, ImmutableArray<Product>>
{
    private readonly IProductRepository _repository;

    public FindByCategoryHandler(IProductRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<ImmutableArray<Product>> Handle(FindByCategory request, CancellationToken cancellationToken)
    {
        return await _repository.FindByCategory(request.CategoryId, cancellationToken);
    }
}
