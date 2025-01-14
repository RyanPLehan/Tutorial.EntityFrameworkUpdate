﻿using MediatR;
using Tutorial.EntityFrameworkUpdate.Application.Models;

namespace Tutorial.EntityFrameworkUpdate.Application.Categories.Requests;

public class Replace : IRequest
{
    public required int OldCategoryId { get; init; }
    public required int NewCategoryId { get; init; }
}
