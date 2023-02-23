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

    public async Task<(PageModel Page, RolePageModel RolePage)> Create(PageModel pageModel)
    {
        await _securityChecker.ThrowIfIsntLogged();

        var currentUserId = await GetCurrentUser();

        using var transaction = await _uoW.BeginTransactionAsync();

        var pageAdded = await _pageRepository.Add(Mappings.Mapper.Map(pageModel));

        var rolePageAdded = 
            await _rolePageRepository.Add(Core.Entities.RolePage.CreateOwnerRolePage(currentUserId, pageAdded.Id));

        await _uoW.SaveChangesAsync();

        return (pageAdded, rolePageAdded);
    }

    public async Task<PageModel?> CurrentPageGetByIdOrDefault(Guid id)
    {
        await _securityChecker.ThrowIfIsntLogged();

        using var connection = await _uoW.OpenConnectionAsync();

        var tupleCurrUserAndPage = await GetCurrentUserAndPage();

        if (tupleCurrUserAndPage.PageId != id)
            throw new Core.Exceptions.ForbiddenCoreException("Cannot access this page.");

        return await _pageRepository.GetByIdOrDefault(id);
    }

    public async Task<PageModel> CurrentPageUpdate(Guid pageId, PageModel pageModel)
    {
        await _securityChecker.ThrowIfIsntOwner();

        using var transaction = await _uoW.BeginTransactionAsync();

        var currentUserAndPage = await GetCurrentUserAndPage();

        if (pageId != currentUserAndPage.PageId)
            throw new Core.Exceptions.ForbiddenCoreException("Cannot update this page.");

        var pageUpdated = await _pageRepository.UpdateByIdOrDefault(pageId, 
            Mappings.Mapper.Map(pageModel));
        
        if (pageUpdated is null)
            throw new Core.Exceptions.NotFoundCoreException("Page not found.");

        await _uoW.SaveChangesAsync();

        return pageUpdated;
    }
    
    public async Task<PageModel> CurrentPageRemove(Guid pageId)
    {
        await _securityChecker.ThrowIfIsntOwner();

        using var transaction = await _uoW.BeginTransactionAsync();

        var currentUserAndPage = await GetCurrentUserAndPage();

        if (currentUserAndPage.PageId != pageId)
            throw new Core.Exceptions.ForbiddenCoreException("Cannot remove this page.");

        var pageRemoved = await _pageRepository.GetByIdOrDefault(pageId);

        if (pageRemoved is null)
            throw new Core.Exceptions.NotFoundCoreException("Page not found.");

        var rolePages = await _rolePageRepository.GetByPage(pageId)
            .ToListAsync();

        foreach (var rolePage in rolePages)
        {
            var roleRemoved = await _rolePageRepository.RemoveByIdOrDefault(rolePage.Id)
                ?? throw new Core.Exceptions.NotFoundCoreException("Role cannot be removed.");
        }

        await _uoW.SaveChangesAsync();

        return pageRemoved;
    }

    public async IAsyncEnumerable<PageAndRolePageModel> GetByUser(Guid userId)
    {
        await _securityChecker.ThrowIfIsntLogged();

        using var connection = await _uoW.OpenConnectionAsync();

        var currentUserId = await GetCurrentUser();

        if (currentUserId != userId)
            throw new Core.Exceptions.UnauthorizedCoreException();

        await foreach (var pageAndRole in _pageRepository.GetPagesWhichUserCanAccess(userId))
            yield return pageAndRole;
    }

    private async Task<Guid> GetCurrentUser()
    {
        return (await _securityContext.GetCurrentClaim())?.UserId
            ?? throw new Core.Exceptions.UnauthorizedCoreException();
    }

    private async Task<(Guid UserId, Guid PageId)> GetCurrentUserAndPage()
    {
        var currentUser = await _securityContext.GetCurrentClaim()
            ?? throw new Core.Exceptions.UnauthorizedCoreException();

        if (currentUser.UserId is null)
            throw new Core.Exceptions.UnauthorizedCoreException();

        if (currentUser.PageId is null)
            throw new Core.Exceptions.ForbiddenCoreException("Null or empty current page.");

        return (currentUser.UserId.Value, currentUser.PageId.Value);
    }    
}