﻿using MediatR;
using Tutorial.EntityFrameworkUpdate.Application.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.Categories.Requests;

public class UpdatePartialContract : IRequest<Category>
{
    public required int Id { get; init; }
    public required string Description { get; init; }
}