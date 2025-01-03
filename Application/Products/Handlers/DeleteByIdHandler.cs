using MediatR;
using Tutorial.EntityFrameworkUpdate.Application.Products.Requests;
using Tutorial.EntityFrameworkUpdate.Domain.Repositories;

namespace Tutorial.EntityFrameworkUpdate.Application.Products.Handlers;

internal class DeleteByIdHandler : IRequestHandler<DeleteById>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductTagRepository _productTagRepository;

    public DeleteByIdHandler(IProductRepository productRepository,
                             IProductTagRepository productTagRepository)
    {
        ArgumentNullException.ThrowIfNull(productRepository, nameof(productRepository));
        ArgumentNullException.ThrowIfNull(productTagRepository, nameof(productTagRepository));

        _productRepository = productRepository;
        _productTagRepository = productTagRepository;
    }

    public async Task Handle(DeleteById request, CancellationToken cancellationToken)
    {
        await _productTagRepository.DeleteByProduct(request.Id, cancellationToken);
        await _productRepository.Delete(request.Id, cancellationToken);
    }
}
