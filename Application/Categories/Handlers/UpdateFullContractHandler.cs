using MediatR;
using Tutorial.EntityFrameworkUpdate.Application.Categories.Requests;
using Tutorial.EntityFrameworkUpdate.Application.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.Categories.Handlers;

internal class UpdateFullContractHandler : IRequestHandler<UpdateFullContract, Category>
{
    private readonly ICategoryRepository _repository;

    public UpdateFullContractHandler(ICategoryRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<Category> Handle(UpdateFullContract request, CancellationToken cancellationToken)
    {
        // For this scenario, the API defines a the entire Category Contract
        // Since our repository update routine only accepts a full Category contract, all we need to do is just call the
        // Therefore, we need to do the following
        // 1.  Query to get the full Category contract first.  However, the repository GET routine only returns non-tracked entities
        // 2.  Update field(s)
        // 3.  Call repository to update the Category

        // Since the database entity is NOT being tracked by Entity Framework, need to understand the following
        // 1. EF will generate an UPDATE statement will ALL fields that are defined in the dbcontext configuration file
        // 2. If updating a subset of the fields, this will require a read of the full data object from the database and populate the missing fields.
        // 3. This can be done within the Request Handler, because we are using the Interface Get method (which does not track data)

        Category entity = new Category()
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
        };

        return await _repository.Update(entity, cancellationToken);
    }
}
