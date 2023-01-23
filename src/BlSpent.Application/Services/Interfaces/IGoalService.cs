using BlSpent.Application.Model;
using BlSpent.Core.Entities;

namespace BlSpent.Application.Services.Interfaces;

public interface IGoalService
{
    public Task<GoalModel> Add(Goal entity);

    public Task<GoalModel?> GetByIdOrDefault(Guid id);

    public IAsyncEnumerable<GoalModel> GetByPageId(Guid pageId);

    public Task<GoalModel?> RemoveByIdOrDefault(Guid id);

    public Task<GoalModel?> UpdateByIdOrDefault(Guid id, Goal entity);
}