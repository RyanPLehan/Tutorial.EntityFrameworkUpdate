using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Requests;
using System.Collections.Immutable;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Handlers;

internal class AddHandler : IRequestHandler<AddRequest, Category>
{
    private readonly ICategoryRepository _repository;

    public AddHandler(ICategoryRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<Category> Handle(AddRequest request, CancellationToken cancellationToken)
    {
        Category entity = new Category()
        {
            Name = request.Name,
            Description = request.Description,
        };

        return await _repository.Add(entity, cancellationToken);
    }
}
