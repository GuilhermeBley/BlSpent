using BlSpent.Core.Entities;
using BlSpent.Application.Model;

namespace BlSpent.Application.Repository;

public interface IEarningRepository : IBaseRepository<Earning, EarningModel, Guid>
{
    IAsyncEnumerable<EarningModel> GetByPageId(Guid pageId);
}