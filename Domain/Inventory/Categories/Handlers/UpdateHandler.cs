using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Requests;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Handlers;

internal class UpdateHandler : IRequestHandler<Update, Category>
{
    private readonly ICategoryRepository _repository;

    public UpdateHandler(ICategoryRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<Category> Handle(Update request, CancellationToken cancellationToken)
    {
        // Since the database entity is NOT being tracked by Entity Framework, need to understand the following
        // 1. EF will generate an UPDATE statement will ALL fields that are defined in the dbcontext configuration file
        // 2. If updating a subset of the fields, this will require a read of the full data object from the database and populate the missing fields.
        // 3. This can be done within the Request Handler, because we are using the Interface Get method (which does not track data)

        Category entity = null;
        var dbEntity = await _repository.Get(request.Id, cancellationToken);
        if (dbEntity != null)
        {
            entity = MapUpdatedFields(request, dbEntity);
            await _repository.Update(entity, cancellationToken);
        }

        return entity;
    }

    private Category MapUpdatedFields(Update source, Category target)
    {
        return new Category()
        {
            // Must have the Primary Key
            Id = target.Id,

            // Commenting out the following line will cause an error b/c EF will put in a null value
            Name = target.Name,

            // This is the field wa want to update
            Description = source.Description,
        };
    }
}
