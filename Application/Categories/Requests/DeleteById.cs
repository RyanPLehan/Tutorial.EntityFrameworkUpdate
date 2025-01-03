using MediatR;

namespace Tutorial.EntityFrameworkUpdate.Application.Categories.Requests;

public class DeleteById : IRequest
{
    public required int Id { get; init; }
}
