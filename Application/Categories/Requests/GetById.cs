using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Application.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.Categories.Requests;

public class GetById : IRequest<Category?>
{
    public required int Id { get; init; }
}
