using Microsoft.EntityFrameworkCore;

namespace Tutorial.EntityFrameworkUpdate.Infrastructure.Repositories;

public interface IContextFactory<TContext>
    where TContext : DbContext
{
    TContext CreateCommandContext();
    TContext CreateQueyContext();
}
