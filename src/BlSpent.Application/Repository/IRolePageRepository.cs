using BlSpent.Core.Entities;
using BlSpent.Application.Model;

namespace BlSpent.Application.Repository;

public interface IRolePageRepository : IBaseRepository<RolePageModel, RolePage, Guid>
{
    IAsyncEnumerable<RolePageModel> GetByUserOrDefault(Guid userId);
    IAsyncEnumerable<RolePageModel> GetByPage(Guid pageId);
    Task<RolePageModel?> CreateOrUpdate(Guid id, RolePageModel rolePageModel);
}