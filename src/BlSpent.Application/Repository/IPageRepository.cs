using BlSpent.Core.Entities;
using BlSpent.Application.Model;

namespace BlSpent.Application.Repository;

public interface IPageRepository : IBaseRepository<PageModel, Page, Guid>
{
    IAsyncEnumerable<PageModel> GetPagesWhichUserCanAccess(Guid userId);
}