using BlSpent.Core.Entities;
using BlSpent.Application.Model;

namespace BlSpent.Application.Repository;

public interface IPageRepository : IBaseRepository<PageModel, Page, Guid>
{
    IAsyncEnumerable<PageAndRolePageModel> GetPagesWhichUserCanAccess(Guid userId);
}