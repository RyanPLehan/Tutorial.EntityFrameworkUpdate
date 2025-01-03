using MediatR;
using Tutorial.EntityFrameworkUpdate.Application.ProductTags.Requests;
using Tutorial.EntityFrameworkUpdate.Domain.Repositories;

namespace Tutorial.EntityFrameworkUpdate.Application.ProductTags.Handlers;

internal class DeleteByIdHandler : IRequestHandler<DeleteById>
{
    private readonly IProductTagRepository _repository;

    public DeleteByIdHandler(IProductTagRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task Handle(DeleteById request, CancellationToken cancellationToken)
    {
        await _repository.Delete(request.ProductId, request.Id, cancellationToken);
    }
}
