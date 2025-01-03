using System.Collections.Generic;
using MediatR;

namespace Tutorial.EntityFrameworkUpdate.Application.Products.Requests;

public class DeleteByIds : IRequest
{
    public required IEnumerable<int> Ids { get; init; }
}
