using BlSpent.Core.Entities;
using BlSpent.Application.Model;

namespace BlSpent.Application.Repository;

public interface IGoalRepository : IBaseRepository<Goal, GoalModel, Guid>
{
    IAsyncEnumerable<GoalModel> GetByPageId(Guid pageId);
}