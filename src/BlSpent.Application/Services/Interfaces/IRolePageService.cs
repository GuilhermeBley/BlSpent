using BlSpent.Application.Model;

namespace BlSpent.Application.Services.Interfaces;

public interface IRolePageService
{
    /// <summary>
    /// Owners can view who is activated in your page
    /// </summary>
    /// <param name="pageId">page id</param>
    IAsyncEnumerable<RoleUserPageModel> GetByPage(Guid pageId);

    /// <summary>
    /// Current Owner of page  update user to role 'ReadOnly'
    /// </summary>
    /// <param name="rolePageModel">role page model</param>
    /// <returns>role page updated</returns>
    Task<RolePageModel> CurrentOwnerUpdateRoleReadOnly(RolePageModel rolePageModel);

    /// <summary>
    /// Current Owner of page update user to role 'Modifier'
    /// </summary>
    /// <param name="rolePageModel">role page model</param>
    /// <returns>role page updated</returns>
    Task<RolePageModel> CurrentOwnerUpdateRoleModifier(RolePageModel rolePageModel);

    /// <summary>
    /// User logged accept 'ReadOnly' role in page
    /// </summary>
    /// <param name="rolePageModel">role page model</param>
    /// <returns>role page added</returns>
    Task<RolePageModel> InviteRoleReadOnly(RolePageModel rolePageModel);

    /// <summary>
    /// User logged accept 'Modifier' role in page
    /// </summary>
    /// <param name="rolePageModel">role page model</param>
    /// <returns>role page added</returns>
    Task<RolePageModel> InviteRoleModifier(RolePageModel pageModel);

    /// <summary>
    /// Current owner of page remove role of user
    /// </summary>
    /// <param name="rolePageId"></param>
    /// <returns></returns>
    Task<RolePageModel?> CurrentOwnerRemove(Guid rolePageId);

    /// <summary>
    /// Current user remove page
    /// </summary>
    /// <param name="rolePageId">role page id</param>
    /// <returns>page removed</returns>
    Task<RolePageModel> RemoveByIdOrDefault(Guid rolePageId);

    /// <summary>
    /// Get role page with access by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Role page or null if not found.</returns>
    Task<RoleUserPageModel?> GetByIdOrDefault(Guid id);
}