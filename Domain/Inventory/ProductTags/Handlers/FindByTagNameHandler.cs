using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.ProductTags.Requests;
using System.Collections.Immutable;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.ProductTags.Handlers;

internal class FindByTagNameHandler : IRequestHandler<FindByTagName, ImmutableArray<ProductTag>>
{
    private readonly IProductTagRepository _repository;

    public FindByTagNameHandler(IProductTagRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<ImmutableArray<ProductTag>> Handle(FindByTagName request, CancellationToken cancellationToken)
    {
        if (String.IsNullOrWhiteSpace(request.TagName))
            return new ImmutableArray<ProductTag>();
        else
            return await _repository.FindByName(request.TagName, cancellationToken);
    }
}
