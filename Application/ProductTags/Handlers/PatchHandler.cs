using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Application.ProductTags.Requests;
using Tutorial.EntityFrameworkUpdate.Domain.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Repositories;

namespace Tutorial.EntityFrameworkUpdate.Application.ProductTags.Handlers;

internal class PatchHandler : IRequestHandler<Patch, ImmutableArray<ProductTag>>
{
    private readonly IProductTagRepository _repository;

    public PatchHandler(IProductTagRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<ImmutableArray<ProductTag>> Handle(Patch request, CancellationToken cancellationToken)
    {
        // For this particular scenerio, we have been given a list of Tags.
        // 1.  No tags are ever updated
        // 2.  New tags are added to the list
        // 3.  Existing tags that are to be deleted are not in (non existent) the list

        await DeleteTags(request, cancellationToken);
        await AddTags(request, cancellationToken);
        return await _repository.FindByProduct(request.ProductId);
    }

    private async Task DeleteTags(Patch request, CancellationToken cancellationToken)
    {
        // Get latest just incase any were added.
        var existingTags = await _repository.FindByProduct(request.ProductId);

        var changeTagIds = request.Tags.Select(x => x.Id);
        var missingTagIds = existingTags.Where(x => !changeTagIds.Contains(x.Id))
                                        .Select(x => x.Id);

        await _repository.Delete(missingTagIds, cancellationToken);
    }

    private async Task AddTags(Patch request, CancellationToken cancellationToken)
    {
        // Get latest just incase any were deleted.
        var existingTags = await _repository.FindByProduct(request.ProductId);

        var existingTagNames = existingTags.Select(x => x.Name);
        var addProductTags = request.Tags.Where(x => x.Id == 0 &&
                                                     !existingTagNames.Contains(x.Name, StringComparer.OrdinalIgnoreCase))
                                         .Select(x => new ProductTag()
                                         {
                                             Id = x.Id,
                                             Name = x.Name,
                                             Value = x.Value,
                                             ProductId = request.ProductId,
                                         });

        await _repository.Add(addProductTags, cancellationToken);
    }

}
