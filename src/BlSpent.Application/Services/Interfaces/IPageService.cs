using BlSpent.Application.Model;

namespace BlSpent.Application.Services.Interfaces;

public interface IPageService
{
    Task<(PageModel Page, RolePageModel RolePage)> Create(PageModel pageModel);
    Task<PageModel?> CurrentPageGetByIdOrDefault(Guid id);
    Task<PageModel> CurrentPageUpdate(Guid pageId, PageModel pageModel);
    Task<PageModel> CurrentPageRemove(Guid pageId);
    IAsyncEnumerable<PageAndRolePageModel> GetByUser(Guid userId);
}