using BlSpent.Application.Tests.InMemoryDb;

namespace BlSpent.Application.Tests.Repositories;

internal abstract class RepositoryBase
{
    protected readonly AppDbContext _context;

    public RepositoryBase(AppDbContext contex)
    {
        _context = contex;
    }
}