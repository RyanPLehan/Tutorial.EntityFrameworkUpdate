using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Repositories;
using Tutorial.EntityFrameworkUpdate.Application.ProductTags.Requests;

namespace Tutorial.EntityFrameworkUpdate.Application.ProductTags.Handlers;

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
        if (string.IsNullOrWhiteSpace(request.TagName))
            return new ImmutableArray<ProductTag>();
        else
            return await _repository.FindByName(request.TagName, cancellationToken);
    }
}
