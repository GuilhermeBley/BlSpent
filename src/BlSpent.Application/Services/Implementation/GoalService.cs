using BlSpent.Application.Model;
using BlSpent.Core.Entities;
using BlSpent.Application.Services.Interfaces;
using BlSpent.Application.UoW;
using BlSpent.Application.Repository;
using BlSpent.Application.Exceptions;

namespace BlSpent.Application.Services.Implementation;

public class GoalService : IGoalService
{
    private readonly IUnitOfWork _uow;
    private readonly IGoalRepository _goalRepository;
    private readonly IPageRepository _pageRepository;

    public GoalService(IUnitOfWork uow, 
        IGoalRepository goalRepository, IPageRepository pageRepository)
    {
        _uow = uow;
        _goalRepository = goalRepository;
        _pageRepository = pageRepository;
    }

    public async Task<GoalModel> Add(GoalModel model)
    {
        using var transaction = await _uow.BeginTransactionAsync();

        if ((await _pageRepository.GetByIdOrDefault(model.PageId)) is null)
            throw new PageNotFoundCoreException(model.PageId);

        return await _goalRepository.Add(Mappings.Mapper.Map(model));
    }

    public async Task<GoalModel?> GetByIdOrDefault(Guid id)
    {
        using var transaction = await _uow.OpenConnectionAsync();
        return await _goalRepository.GetByIdOrDefault(id);
    }

    public async IAsyncEnumerable<GoalModel> GetByPageId(Guid pageId)
    {
        if (Guid.Empty == pageId)
            yield break;

        using (await _uow.OpenConnectionAsync())
            await foreach (var goal in _goalRepository.GetByPageId(pageId))
            {
                yield return goal;
            }
    }

    public async Task<GoalModel?> RemoveByIdOrDefault(Guid id)
    {
        if (Guid.Empty == id)
            return null;

        using var transaction = await _uow.BeginTransactionAsync();

        if ((await _goalRepository.GetByIdOrDefault(id)) is null)
            return null;

        return await _goalRepository.RemoveByIdOrDefault(id);
    }

    public async Task<GoalModel?> UpdateByIdOrDefault(Guid id, GoalModel model)
    {
         if (Guid.Empty == id)
            return null;

        using var transaction = await _uow.BeginTransactionAsync();

        if ((await _goalRepository.GetByIdOrDefault(id)) is null)
            return null;

        return await _goalRepository.UpdateByIdOrDefault(id, Mappings.Mapper.Map(model));
    }
}