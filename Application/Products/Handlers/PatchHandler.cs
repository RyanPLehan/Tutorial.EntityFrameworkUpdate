﻿using MediatR;
using Tutorial.EntityFrameworkUpdate.Application.Products.Requests;
using Tutorial.EntityFrameworkUpdate.Application.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.Products.Handlers;

internal class PatchHandler : IRequestHandler<Patch, Product>
{
    private readonly IProductRepository _repository;

    public PatchHandler(IProductRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task<Product?> Handle(Patch request, CancellationToken cancellationToken)
    {
        // For this particular scenerio, we have the following
        // 1.  Zero to three properties/fields can be updated
        // 2.  Each field must be checked individually
        // 3.  Each field has its own update routine
        // 4.  Instead of checking to see if the entity exists first, just issue the updates and then retrieve the entity at the end

        // It is important to note that for this example, there are up to 3 potential database calls to update the entity.
        // The entire operation should be ATOMIC, therefore, should be wrapped within a transaction.
        //   - I would normally use Transaction Scope here, but SQLite does not support Implicit Transaction Scopes.

        // It should be noted, that I personally do not like using the HTTP Patch method to update specific fields.
        // I believe that it should be used for the purpose of modifying collection items (ie add/delete items to/from a list)

        if (!string.IsNullOrWhiteSpace(request.Description))
            await _repository.UpdateDescription(request.Id, request.Description, cancellationToken);

        if (request.Price != null)
            await _repository.UpdatePrice(request.Id, request.Price.Value, cancellationToken);

        if (request.Quantity != null)
            await _repository.UpdateQuantity(request.Id, request.Quantity.Value, cancellationToken);

        return await _repository.Get(request.Id, cancellationToken);
    }
}
