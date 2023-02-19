using AutoMapper;
using BlSpent.Application.Tests.InMemoryDb;
using BlSpent.Application.Tests.UoW;

namespace BlSpent.Application.Tests.Repositories;

internal abstract class RepositoryBase
{
    protected readonly IMemorySession _memorySession;
    protected AppDbContext _context => _memorySession.Context;
    protected readonly IMapper _mapper;

    public RepositoryBase(
        IMemorySession memorySession,
        IMapper mapper)
    {
        _memorySession = memorySession;
        _mapper = mapper;
    }
}