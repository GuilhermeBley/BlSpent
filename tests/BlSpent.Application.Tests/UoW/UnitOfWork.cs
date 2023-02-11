using BlSpent.Application.Tests.InMemoryDb;
using BlSpent.Application.UoW;
using Microsoft.EntityFrameworkCore.Storage;

namespace BlSpent.Application.Tests.UoW;

internal class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;

    public Guid IdSession { get; } = Guid.NewGuid();

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IUnitOfWork> BeginTransactionAsync()
    {
        _transaction ??= await _context.Database.BeginTransactionAsync();
        return this;
    }

    public void Dispose()
    {
        try
        {
            _transaction?.Dispose();
        }
        finally
        {
            _transaction = null;
        }
    }

    public async Task<IUnitOfWork> OpenConnectionAsync()
    {
        await Task.CompletedTask;
        return this;
    }

    public async Task RollBackAsync()
    {
        try
        {
            if (_transaction is not null)
                await _transaction.RollbackAsync();
        }
        finally
        {
            _transaction = null;
        }
    }

    public async Task SaveChangesAsync()
    {
        try
        {
            if (_transaction is not null)
                await _transaction.CommitAsync();
        }
        finally
        {
            _transaction = null;
        }
    }
}