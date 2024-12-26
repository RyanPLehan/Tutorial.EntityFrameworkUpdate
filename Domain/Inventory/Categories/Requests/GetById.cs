﻿using MediatR;
using System.Collections.Immutable;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;

namespace Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories.Requests;

public class GetById : IRequest<Category?>
{
    public required int Id { get; init; }
}
