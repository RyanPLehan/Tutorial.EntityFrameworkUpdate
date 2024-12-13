using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Requests;

public class GetAll : IRequest<ImmutableArray<Category>>
{
}
