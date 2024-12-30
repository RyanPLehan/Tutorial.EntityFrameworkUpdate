using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.ProductTags.Requests;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.ProductTags.Handlers;

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
        await _repository.Delete(request.Id, cancellationToken);
    }
}
