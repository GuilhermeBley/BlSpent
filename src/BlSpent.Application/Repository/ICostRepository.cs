using BlSpent.Core.Entities;
using BlSpent.Application.Model;

namespace BlSpent.Application.Repository;

public interface ICostRepository : IBaseRepository<CostModel, Cost, Guid>
{
    IAsyncEnumerable<CostModel> GetByPageId(Guid pageId);
}