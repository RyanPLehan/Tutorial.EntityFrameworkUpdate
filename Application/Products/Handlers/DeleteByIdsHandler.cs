using System.Collections.Generic;
using MediatR;
using Tutorial.EntityFrameworkUpdate.Application.Products.Requests;
using Tutorial.EntityFrameworkUpdate.Application.ProductTags;

namespace Tutorial.EntityFrameworkUpdate.Application.Products.Handlers;

internal class DeleteByIdsHandler : IRequestHandler<DeleteByIds>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductTagRepository _productTagRepository;

    public DeleteByIdsHandler(IProductRepository productRepository,
                              IProductTagRepository productTagRepository)
    {
        ArgumentNullException.ThrowIfNull(productRepository, nameof(productRepository));
        ArgumentNullException.ThrowIfNull(productTagRepository, nameof(productTagRepository));

        _productRepository = productRepository;
        _productTagRepository = productTagRepository;
    }

    public async Task Handle(DeleteByIds request, CancellationToken cancellationToken)
    {
        // TODO: This should be done in batches
        await _productTagRepository.DeleteByProducts(request.Ids, cancellationToken);
        await _productRepository.Delete(request.Ids, cancellationToken);
    }
}
