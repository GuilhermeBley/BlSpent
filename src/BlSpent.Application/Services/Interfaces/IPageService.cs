using BlSpent.Application.Model;

namespace BlSpent.Application.Services.Interfaces;

public interface IPageService
{
    IAsyncEnumerable<PageModel> GetByUser(Guid userId);
    Task<PageModel?> Add(PageModel pageModel);
    Task<PageModel?> UpdateByIdOrDefault(Guid pageId, PageModel pageModel);
    Task<PageModel?> RemoveByIdOrDefault(Guid pageId);
    Task<PageModel?> GetByIdOrDefault(Guid id);
}