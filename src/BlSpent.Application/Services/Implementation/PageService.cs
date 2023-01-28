using BlSpent.Application.Model;
using BlSpent.Application.Security;
using BlSpent.Application.Services.Interfaces;
using BlSpent.Application.UoW;
using BlSpent.Application.Repository;

namespace BlSpent.Application.Services.Implementation;

public class PageService : BaseService, IPageService
{
    private readonly IPageRepository _pageRepository;
    private readonly IRolePageRepository _rolePageRepository;
    private readonly IUnitOfWork _uoW;

    public PageService(
        IPageRepository pageRepository,
        IRolePageRepository rolePageRepository,
        IUnitOfWork uoW,
        ISecurityContext securityContext) : base(securityContext)
    {
        _pageRepository = pageRepository;
        _rolePageRepository = rolePageRepository;
        _uoW = uoW;
    }

    public async Task<PageModel?> Add(PageModel pageModel)
    {
        await _securityChecker.ThrowIfIsntLogged();

        var currentUserId = (await _securityContext.GetCurrentClaim())?.UserId
            ?? throw new Core.Exceptions.UnauthorizedCoreException();

        using var transaction = await _uoW.BeginTransactionAsync();

        var pageAdded = await _pageRepository.Add(Mappings.Mapper.Map(pageModel));

        var rolePageModel = new RolePageModel()
        {
            Id = Guid.NewGuid(),
            CreateDate = DateTime.Now,
            PageId = pageAdded.Id,
            Role = Core.Security.PageClaim.Owner.Value,
            UserId = currentUserId
        };

        var rolePage = await _rolePageRepository.Add(Mappings.Mapper.Map(rolePageModel));

        await _uoW.SaveChangesAsync();

        return pageAdded;
    }

    public async Task<PageModel?> GetByIdOrDefault(Guid id)
    {
        await _securityChecker.ThrowIfIsntLogged();

        using var connection = await _uoW.OpenConnectionAsync();

        return await _pageRepository.GetByIdOrDefault(id);
    }

    public async IAsyncEnumerable<PageModel> GetByUser(Guid userId)
    {
        await _securityChecker.ThrowIfIsntLogged();

        using var connection = await _uoW.OpenConnectionAsync();

        await foreach (var page in _pageRepository.GetPagesWhichUserCanAccess(userId))
            yield return page;
    }

    public async Task<PageModel?> RemoveByIdOrDefault(Guid pageId)
    {
        await _securityChecker.ThrowIfIsntOwner();

        await _securityChecker.ThrowIfIsntAuthorizedInPage();

        var currentUserId = (await _securityContext.GetCurrentClaim())?.UserId
            ?? throw new Core.Exceptions.UnauthorizedCoreException();

        using var transaction = await _uoW.BeginTransactionAsync();

        var pageRemoved = await _pageRepository.GetByIdOrDefault(pageId);

        if (pageRemoved is null)
            return null;

        var rolePages = await _rolePageRepository.GetByPage(pageId)
            .ToListAsync();

        foreach (var rolePage in rolePages)
            await _rolePageRepository.RemoveByIdOrDefault(rolePage.Id);

        await _uoW.SaveChangesAsync();

        return pageRemoved;
    }

    public async Task<PageModel?> UpdateByIdOrDefault(Guid pageId, PageModel pageModel)
    {
        await _securityChecker.ThrowIfIsntOwner();

        var currentUserId = (await _securityContext.GetCurrentClaim())?.UserId
            ?? throw new Core.Exceptions.UnauthorizedCoreException();

        using var transaction = await _uoW.BeginTransactionAsync();

        var pageUpdated = await _pageRepository.UpdateByIdOrDefault(pageId, 
            Mappings.Mapper.Map(pageModel));
        
        if (pageUpdated is null)
            return null;

        await _uoW.SaveChangesAsync();

        return pageUpdated;
    }
}