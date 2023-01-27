using BlSpent.Application.Model;

namespace BlSpent.Application.Services.Interfaces;

public interface IPageService
{
    Task<IEnumerable<PageModel>> GetAllByUser(Guid userId);
    Task<PageModel?> Create(PageModel pageModel);
    Task<PageModel?> Update(Guid pageId, PageModel pageModel);
    Task<PageModel?> Delete(Guid pageId);
}