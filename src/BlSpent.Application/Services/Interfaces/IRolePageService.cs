using BlSpent.Application.Model;

namespace BlSpent.Application.Services.Interfaces;

public interface IRolePageService
{
    IAsyncEnumerable<RolePageModel> GetByPage(Guid pageId);
    Task<RolePageModel?> AddOrUpdateRoleReadOnly(RolePageModel pageModel);
    Task<RolePageModel?> AddOrUpdateRoleModifier(RolePageModel pageModel);
    Task<RolePageModel?> UpdateByIdOrDefault(Guid pageId, RolePageModel pageModel);
    Task<RolePageModel?> RemoveByIdOrDefault(Guid pageId);
    Task<RolePageModel?> GetByIdOrDefault(Guid id);
}