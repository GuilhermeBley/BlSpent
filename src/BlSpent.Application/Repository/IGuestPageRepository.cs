using BlSpent.Core.Entities;
using BlSpent.Application.Model;

namespace BlSpent.Application.Repository;

public interface IGuestPageRepository : IBaseRepository<GuestPageModel, GuestPage, Guid>
{
    IAsyncEnumerable<GuestPageModel> GetByUserOrDefault(Guid userId);
    IAsyncEnumerable<GuestPageModel> GetByPage(Guid pageId);
}