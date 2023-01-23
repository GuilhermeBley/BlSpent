using BlSpent.Application.Model;
using BlSpent.Core.Entities;

namespace BlSpent.Application.Services.Interfaces;

public interface IEarningService
{
    public Task<EarningModel> Add(Earning entity);

    public Task<EarningModel?> GetByIdOrDefault(Guid id);

    public IAsyncEnumerable<EarningModel> GetByPageId(Guid pageId);

    public Task<EarningModel?> RemoveByIdOrDefault(Guid id);

    public Task<EarningModel?> UpdateByIdOrDefault(Guid id, Earning entity);
}