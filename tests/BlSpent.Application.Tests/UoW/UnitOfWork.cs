using BlSpent.Application.Tests.InMemoryDb;
using BlSpent.Application.UoW;
using Microsoft.EntityFrameworkCore.Storage;

namespace BlSpent.Application.Tests.UoW;

internal class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public Guid IdSession { get; } = Guid.NewGuid();

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IUnitOfWork> BeginTransactionAsync()
    {
        await Task.CompletedTask;
        return this;
    }

    public void Dispose()
    {
       
    }

    public async Task<IUnitOfWork> OpenConnectionAsync()
    {
        await Task.CompletedTask;
        return this;
    }

    public async Task RollBackAsync()
    {
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await Task.CompletedTask;
    }
}