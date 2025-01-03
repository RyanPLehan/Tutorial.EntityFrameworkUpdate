using MediatR;
using Tutorial.EntityFrameworkUpdate.Application.Categories.Requests;
using Tutorial.EntityFrameworkUpdate.Domain.Repositories;
using ProductReq = Tutorial.EntityFrameworkUpdate.Application.Products.Requests;

namespace Tutorial.EntityFrameworkUpdate.Application.Categories.Handlers;

internal class DeleteByIdHandler : IRequestHandler<DeleteById>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMediator _mediator;

    public DeleteByIdHandler(ICategoryRepository categoryRepository,
                             IProductRepository productRepository,
                             IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(categoryRepository, nameof(categoryRepository));
        ArgumentNullException.ThrowIfNull(productRepository, nameof(productRepository));
        ArgumentNullException.ThrowIfNull(mediator, nameof(mediator));

        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
        _mediator = mediator;
    }

    public async Task Handle(DeleteById request, CancellationToken cancellationToken)
    {
        // Delete Products first
        var productIds = await _productRepository.FindIdByCategory(request.Id, cancellationToken);
        var delProductsReq = new ProductReq.DeleteByIds() { Ids = productIds };
        await _mediator.Send(delProductsReq);

        await _categoryRepository.Delete(request.Id, cancellationToken);
    }
}
