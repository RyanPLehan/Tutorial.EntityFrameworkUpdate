using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Requests;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Products;
using System.Collections.Immutable;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Handlers;

internal class ReplaceHandler : IRequestHandler<Replace>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;

    public ReplaceHandler(ICategoryRepository categoryRepository,
                          IProductRepository productRepository)
    {
        ArgumentNullException.ThrowIfNull(categoryRepository, nameof(categoryRepository));
        ArgumentNullException.ThrowIfNull(productRepository, nameof(productRepository));

        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }

    public async Task Handle(Replace request, CancellationToken cancellationToken)
    {
        // Ensure both Category Ids exist prior to trying to replace one for the other
        var existingCategories = await _categoryRepository.Get(new int[] { request.OldCategoryId, request.NewCategoryId }, cancellationToken);
        existingCategories.First(x => x.Id == request.OldCategoryId);
        existingCategories.First(x => x.Id == request.NewCategoryId);


        // Do nothing to Category data, but need to move all products to new category
        await _productRepository.ReplaceCategory(request.OldCategoryId, request.NewCategoryId, cancellationToken);
    }
}
