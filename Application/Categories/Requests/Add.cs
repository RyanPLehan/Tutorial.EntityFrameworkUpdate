using MediatR;
using Tutorial.EntityFrameworkUpdate.Application.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.Categories.Requests;

public class Add : IRequest<Category>
{
    public required string Name { get; init; }
    public required string Description { get; init; }
}
