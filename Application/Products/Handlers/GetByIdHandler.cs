using MediatR;
using Tutorial.EntityFrameworkUpdate.Application.Products.Requests;
using Tutorial.EntityFrameworkUpdate.Application.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.Products.Handlers;

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
