using BlSpent.Application.Model;

namespace BlSpent.Application.Services.Interfaces;

public interface IPageService
{
    Task<PageModel> Create(PageModel pageModel);
    Task<PageModel?> CurrentPageGetByIdOrDefault(Guid id);
    Task<PageModel> CurrentPageUpdate(Guid pageId, PageModel pageModel);
    Task<PageModel> CurrentPageRemove(Guid pageId);
    IAsyncEnumerable<PageModel> GetByUser(Guid userId);
}