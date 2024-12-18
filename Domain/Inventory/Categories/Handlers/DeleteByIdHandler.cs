﻿using MediatR;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Requests;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Handlers;

internal class DeleteByIdHandler : IRequestHandler<DeleteById>
{
    private readonly ICategoryRepository _repository;

    public DeleteByIdHandler(ICategoryRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));

        _repository = repository;
    }

    public async Task Handle(DeleteById request, CancellationToken cancellationToken)
    {
        await _repository.Delete(request.Id, cancellationToken);
    }
}
