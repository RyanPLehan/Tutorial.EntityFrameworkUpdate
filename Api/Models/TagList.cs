﻿using Tutorial.EntityFrameworkUpdate.Domain.Models;

namespace Tutorial.EntityFrameworkUpdate.Api.Models;

public class TagList
{
    public IEnumerable<Tag> Tags { get; set; }
}
