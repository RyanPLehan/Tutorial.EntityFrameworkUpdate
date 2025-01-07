using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Application.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.Products.Requests;

public class GetById : IRequest<Product?>
{
    public int Id { get; init; }
}
