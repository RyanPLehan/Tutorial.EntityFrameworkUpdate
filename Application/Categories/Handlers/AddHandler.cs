using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Application.Models;
using Tutorial.EntityFrameworkUpdate.Application.Categories.Requests;

namespace Tutorial.EntityFrameworkUpdate.Application.Categories.Handlers;

internal class AddHandler : IRequestHandler<Add, Category>
{
    private readonly ICategoryRepository _repository;

    public AddHandler(ICategoryRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<Category> Handle(Add request, CancellationToken cancellationToken)
    {
        Category entity = new Category()
        {
            Name = request.Name,
            Description = request.Description,
        };

        return await _repository.Add(entity, cancellationToken);
    }
}
