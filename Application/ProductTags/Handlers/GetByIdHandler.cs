using MediatR;
using Tutorial.EntityFrameworkUpdate.Application.ProductTags.Requests;
using Tutorial.EntityFrameworkUpdate.Application.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.ProductTags.Handlers;

internal class GetByIdHandler : IRequestHandler<GetById, ProductTag?>
{
    private readonly IProductTagRepository _repository;

    public GetByIdHandler(IProductTagRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<ProductTag?> Handle(GetById request, CancellationToken cancellationToken)
    {
        return await _repository.Get(request.ProductId, request.Id, cancellationToken);
    }
}
