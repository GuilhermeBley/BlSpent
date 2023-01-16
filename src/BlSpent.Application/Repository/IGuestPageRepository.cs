using BlSpent.Core.Entities;
using BlSpent.Application.Model;

namespace BlSpent.Application.Repository;

public interface IGuestPageRepository : IBaseRepository<GuestPageModel, GuestPage, Guid>
{
    Task<CostModel?> GetByUserOrDefault(Guid userId);
    IAsyncEnumerable<CostModel> GetByPage(Guid pageId);
}