using BlSpent.Core.Entities;
using BlSpent.Application.Model;

namespace BlSpent.Application.Repository;

public interface IGoalRepository : IBaseRepository<GoalModel, Goal, Guid>
{
    IAsyncEnumerable<GoalModel> GetByPageId(Guid pageId);
}