using BlSpent.Application.Model;
using BlSpent.Core.Entities;

namespace BlSpent.Application.Services.Interfaces;

public interface ICostService
{
    public Task<CostModel> Add(Cost entity);

    public Task<CostModel?> GetByIdOrDefault(Guid id);

    public IAsyncEnumerable<CostModel> GetByPageId(Guid pageId);

    public Task<CostModel?> RemoveByIdOrDefault(Guid id);

    public Task<CostModel?> UpdateByIdOrDefault(Guid id, Cost entity);
}