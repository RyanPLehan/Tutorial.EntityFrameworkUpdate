using MediatR;

namespace Tutorial.EntityFrameworkUpdate.Application.ProductTags.Requests;

public class DeleteById : IRequest
{
    public required int ProductId { get; init; }
    public required int Id { get; init; }
}
