using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.ProductTags.Requests;

public class GetById : IRequest<ProductTag?>
{
    public required int ProductId { get; init; }
    public required int Id { get; init; }
}
