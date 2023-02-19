using AutoMapper;
using BlSpent.Application.Model;
using BlSpent.Application.Repository;
using BlSpent.Application.Tests.Models;
using BlSpent.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlSpent.Application.Tests.Repositories;

internal class GoalRepository : RepositoryBase, IGoalRepository
{
    public GoalRepository(UoW.IMemorySession memorySession, IMapper mapper) 
        : base(memorySession, mapper)
    {
    }

    public async Task<GoalModel> Add(Goal entity)
    {
        var goalModelDb = _mapper.Map<GoalDbModel>(entity);

        await _context.Goals.AddAsync(goalModelDb);
        await _context.SaveChangesAsync();

        return _mapper.Map<GoalModel>(goalModelDb);
    }

    public async Task<IEnumerable<GoalModel>> GetAll()
    {
        return (await _context.Goals.ToListAsync())
            .Select(goalDb => _mapper.Map<GoalModel>(goalDb));
    }

    public async Task<GoalModel?> GetByIdOrDefault(Guid id)
    {
        var goalDb = await _context.Goals.FirstOrDefaultAsync(u => u.Id == id);

        if (goalDb is null)
            return null;

        return _mapper.Map<GoalModel>(goalDb);
    }

    public async IAsyncEnumerable<GoalModel> GetByPageId(Guid pageId)
    {
        await foreach (var goalModelDb in _context.Goals.Where(c => c.PageId == pageId).AsAsyncEnumerable())
            yield return _mapper.Map<GoalModel>(goalModelDb);
    }

    public async Task<GoalModel?> RemoveByIdOrDefault(Guid id)
    {
        var goalDb = await _context.Goals.FirstOrDefaultAsync(u => u.Id == id);

        if (goalDb is null)
            return null;

        _context.Goals.Remove(goalDb);

        await _context.SaveChangesAsync();

        return _mapper.Map<GoalModel>(goalDb);
    }

    public async Task<GoalModel?> UpdateByIdOrDefault(Guid id, Goal entity)
    {
        var goalDb = await _context.Goals.FirstOrDefaultAsync(u => u.Id == id);

        if (goalDb is null)
            return null;

        var goalToUpdate = _mapper.Map<GoalDbModel>(entity);
        goalDb.TargetDate = goalToUpdate.TargetDate;
        goalDb.PageId = goalToUpdate.PageId;
        goalDb.Value = goalDb.Value;

        _context.Goals.Update(goalDb);

        await _context.SaveChangesAsync();

        return _mapper.Map<GoalModel>(
            await _context.Goals.FirstOrDefaultAsync(u => u.Id == id)
        );
    }
}