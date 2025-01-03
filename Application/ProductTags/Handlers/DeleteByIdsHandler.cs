using System.Collections.Generic;
using MediatR;
using Tutorial.EntityFrameworkUpdate.Application.ProductTags.Requests;
using Tutorial.EntityFrameworkUpdate.Domain.Repositories;

namespace Tutorial.EntityFrameworkUpdate.Application.ProductTags.Handlers;

internal class DeleteByIdsHandler : IRequestHandler<DeleteByIds>
{
    private readonly IProductTagRepository _repository;

    public DeleteByIdsHandler(IProductTagRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task Handle(DeleteByIds request, CancellationToken cancellationToken)
    {
        await _repository.Delete(request.Ids, cancellationToken);
    }
}
