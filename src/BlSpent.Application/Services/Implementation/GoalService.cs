using BlSpent.Application.Model;
using BlSpent.Application.Services.Interfaces;
using BlSpent.Application.UoW;
using BlSpent.Application.Repository;
using BlSpent.Application.Exceptions;
using BlSpent.Application.Security;

namespace BlSpent.Application.Services.Implementation;

public class GoalService : BaseService, IGoalService
{
    private readonly IUnitOfWork _uow;
    private readonly IGoalRepository _goalRepository;
    private readonly IPageRepository _pageRepository;

    public GoalService(
        IUnitOfWork uow, 
        IGoalRepository goalRepository, 
        IPageRepository pageRepository,
        ISecurityContext securityContext)
        : base(securityContext)
    {
        _uow = uow;
        _goalRepository = goalRepository;
        _pageRepository = pageRepository;
    }

    public async Task<GoalModel> Add(GoalModel model)
    {
        await _securityChecker.ThrowIfCantModify();

        using var transaction = await _uow.BeginTransactionAsync();

        if ((await _pageRepository.GetByIdOrDefault(model.PageId)) is null)
            throw new PageNotFoundCoreException(model.PageId);

        var goalAdded = await _goalRepository.Add(Mappings.Mapper.Map(model));

        await _uow.SaveChangesAsync();

        return goalAdded;
    }

    public async Task<GoalModel?> GetByIdOrDefault(Guid id)
    {
        await _securityChecker.ThrowIfCantRead();

        using var transaction = await _uow.OpenConnectionAsync();
        return await _goalRepository.GetByIdOrDefault(id);
    }

    public async IAsyncEnumerable<GoalModel> GetByPageId(Guid pageId)
    {
        await _securityChecker.ThrowIfCantRead();

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
        await _securityChecker.ThrowIfCantModify();

        if (Guid.Empty == id)
            return null;

        using var transaction = await _uow.BeginTransactionAsync();

        if ((await _goalRepository.GetByIdOrDefault(id)) is null)
            return null;

        var goalRemoved = await _goalRepository.RemoveByIdOrDefault(id);

        await _uow.SaveChangesAsync();

        return goalRemoved;
    }

    public async Task<GoalModel?> UpdateByIdOrDefault(Guid id, GoalModel model)
    {
        await _securityChecker.ThrowIfCantModify();

        if (Guid.Empty == id)
            return null;

        using var transaction = await _uow.BeginTransactionAsync();

        if ((await _goalRepository.GetByIdOrDefault(id)) is null)
            return null;

        var goalUpdated = await _goalRepository.UpdateByIdOrDefault(id, Mappings.Mapper.Map(model));

        return goalUpdated;
    }
}