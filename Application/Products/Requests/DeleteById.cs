using MediatR;

namespace Tutorial.EntityFrameworkUpdate.Application.Products.Requests;

public class DeleteById : IRequest
{
    public required int Id { get; init; }
}
