using MediatR;
using Tutorial.EntityFrameworkUpdate.Application.Categories.Requests;
using Tutorial.EntityFrameworkUpdate.Domain.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Repositories;

namespace Tutorial.EntityFrameworkUpdate.Application.Categories.Handlers;

internal class GetByIdHandler : IRequestHandler<GetById, Category?>
{
    private readonly ICategoryRepository _repository;

    public GetByIdHandler(ICategoryRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<Category?> Handle(GetById request, CancellationToken cancellationToken)
    {
        return await _repository.Get(request.Id, cancellationToken);
    }
}
