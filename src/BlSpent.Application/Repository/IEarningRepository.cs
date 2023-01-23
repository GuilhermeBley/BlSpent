using BlSpent.Core.Entities;
using BlSpent.Application.Model;

namespace BlSpent.Application.Repository;

public interface IEarningRepository : IBaseRepository<Earning, EarningModel, Guid>
{
    IAsyncEnumerable<CostModel> GetByPageId(Guid pageId);
}