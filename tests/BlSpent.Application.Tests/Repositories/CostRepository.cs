using AutoMapper;
using BlSpent.Application.Model;
using BlSpent.Application.Repository;
using BlSpent.Application.Tests.InMemoryDb;
using BlSpent.Application.Tests.Models;
using BlSpent.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlSpent.Application.Tests.Repositories;

internal class CostRepository : RepositoryBase, ICostRepository
{
    public CostRepository(AppDbContext contex, IMapper mapper) 
        : base(contex, mapper)
    {
    }

    public async Task<CostModel> Add(Cost entity)
    {
        var costModelDb = _mapper.Map<CostDbModel>(entity);

        await _context.Costs.AddAsync(costModelDb);
        await _context.SaveChangesAsync();

        return _mapper.Map<CostModel>(costModelDb);
    }

    public async Task<IEnumerable<CostModel>> GetAll()
    {
        return (await _context.Costs.ToListAsync())
            .Select(costDb => _mapper.Map<CostModel>(costDb));
    }

    public async Task<CostModel?> GetByIdOrDefault(Guid id)
    {
        var costDb = await _context.Costs.FirstOrDefaultAsync(u => u.Id == id);

        if (costDb is null)
            return null;

        return _mapper.Map<CostModel>(costDb);
    }

    public async IAsyncEnumerable<CostModel> GetByPageId(Guid pageId)
    {
        await foreach (var costModelDb in _context.Costs.Where(c => c.PageId == pageId).AsAsyncEnumerable())
            yield return _mapper.Map<CostModel>(costModelDb);
    }

    public async Task<CostModel?> RemoveByIdOrDefault(Guid id)
    {
        var costDb = await _context.Costs.FirstOrDefaultAsync(u => u.Id == id);

        if (costDb is null)
            return null;

        _context.Costs.Remove(costDb);

        await _context.SaveChangesAsync();

        return _mapper.Map<CostModel>(costDb);
    }

    public async Task<CostModel?> UpdateByIdOrDefault(Guid id, Cost entity)
    {
        var costDb = await _context.Costs.FirstOrDefaultAsync(u => u.Id == id);

        if (costDb is null)
            return null;

        var costToUpdate = _mapper.Map<CostDbModel>(entity);
        costDb.CostDate = costToUpdate.CostDate;
        costDb.EntryBaseDate = costToUpdate.EntryBaseDate;
        costDb.PageId = costToUpdate.PageId;
        costDb.Value = costDb.Value;

        _context.Costs.Update(costDb);

        await _context.SaveChangesAsync();

        return _mapper.Map<CostModel>(
            await _context.Costs.FirstOrDefaultAsync(u => u.Id == id)
        );
    }
}