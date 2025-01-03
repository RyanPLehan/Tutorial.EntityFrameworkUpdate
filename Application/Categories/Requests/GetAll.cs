using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.Categories.Requests;

public class GetAll : IRequest<ImmutableArray<Category>>
{
}
