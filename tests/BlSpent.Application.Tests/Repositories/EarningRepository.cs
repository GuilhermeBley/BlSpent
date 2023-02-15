using AutoMapper;
using BlSpent.Application.Model;
using BlSpent.Application.Repository;
using BlSpent.Application.Tests.InMemoryDb;
using BlSpent.Application.Tests.Models;
using BlSpent.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlSpent.Application.Tests.Repositories;

internal class EarningRepository : RepositoryBase, IEarningRepository
{
    public EarningRepository(AppDbContext contex, IMapper mapper) 
        : base(contex, mapper)
    {
    }

    public async Task<EarningModel> Add(Earning entity)
    {
        var earningModelDb = _mapper.Map<EarningDbModel>(entity);

        await _context.Earnings.AddAsync(earningModelDb);
        await _context.SaveChangesAsync();

        return _mapper.Map<EarningModel>(earningModelDb);
    }

    public async Task<IEnumerable<EarningModel>> GetAll()
    {
        return (await _context.Earnings.ToListAsync())
            .Select(earningDb => _mapper.Map<EarningModel>(earningDb));
    }

    public async Task<EarningModel?> GetByIdOrDefault(Guid id)
    {
        var earningDb = await _context.Earnings.FirstOrDefaultAsync(u => u.Id == id);

        if (earningDb is null)
            return null;

        return _mapper.Map<EarningModel>(earningDb);
    }

    public async IAsyncEnumerable<EarningModel> GetByPageId(Guid pageId)
    {
        await foreach (var earningModelDb in _context.Earnings.Where(c => c.PageId == pageId).AsAsyncEnumerable())
            yield return _mapper.Map<EarningModel>(earningModelDb);
    }

    public async Task<EarningModel?> RemoveByIdOrDefault(Guid id)
    {
        var earningDb = await _context.Earnings.FirstOrDefaultAsync(u => u.Id == id);

        if (earningDb is null)
            return null;

        _context.Earnings.Remove(earningDb);

        await _context.SaveChangesAsync();

        return _mapper.Map<EarningModel>(earningDb);
    }

    public async Task<EarningModel?> UpdateByIdOrDefault(Guid id, Earning entity)
    {
        var earningDb = await _context.Earnings.FirstOrDefaultAsync(u => u.Id == id);

        if (earningDb is null)
            return null;

        var earningToUpdate = _mapper.Map<EarningDbModel>(entity);
        earningDb.EarnDate = earningToUpdate.EarnDate;
        earningDb.EntryBaseDate = earningToUpdate.EntryBaseDate;
        earningDb.PageId = earningToUpdate.PageId;
        earningDb.Value = earningToUpdate.Value;

        _context.Earnings.Update(earningDb);

        await _context.SaveChangesAsync();

        return _mapper.Map<EarningModel>(
            await _context.Earnings.FirstOrDefaultAsync(u => u.Id == id)
        );
    }
}