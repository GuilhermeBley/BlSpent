using BlSpent.Core.Entities;
using BlSpent.Application.Model;

namespace BlSpent.Application.Repository;

public interface IRolePageRepository : IBaseRepository<RolePageModel, RolePage, Guid>
{
    IAsyncEnumerable<RolePageModel> GetByUserOrDefault(Guid userId);
    IAsyncEnumerable<RoleUserPageModel> GetByPage(Guid pageId);
    Task<RoleUserPageModel?> GetRoleUserByIdOrDefault(Guid rolePageId);
    Task<RoleUserPageModel?> GetByPageAndUserOrDefault(Guid userId, Guid pageId);
}