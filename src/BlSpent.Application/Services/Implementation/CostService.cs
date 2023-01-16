using BlSpent.Application.Model;
using BlSpent.Core.Entities;
using BlSpent.Application.Services.Interfaces;

namespace BlSpent.Application.Services.Implementation;

public class CostService : ICostService
{
    public Task<CostModel> Add(Cost entity)
    {
        throw new NotImplementedException();
    }

    public Task<CostModel?> GetByIdOrDefault(Guid id)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<CostModel> GetByPageId(Guid pageId)
    {
        throw new NotImplementedException();
    }

    public Task<CostModel?> RemoveByIdOrDefault(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<CostModel?> UpdateByIdOrDefault(Guid id, Cost entity)
    {
        throw new NotImplementedException();
    }
}