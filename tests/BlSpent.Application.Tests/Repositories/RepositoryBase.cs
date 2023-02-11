using AutoMapper;
using BlSpent.Application.Tests.InMemoryDb;

namespace BlSpent.Application.Tests.Repositories;

internal abstract class RepositoryBase
{
    protected readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public RepositoryBase(
        AppDbContext contex,
        IMapper mapper)
    {
        _context = contex;
        _mapper = mapper;
    }
}