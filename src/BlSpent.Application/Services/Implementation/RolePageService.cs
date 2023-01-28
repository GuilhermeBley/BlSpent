using BlSpent.Application.Model;
using BlSpent.Application.Security;
using BlSpent.Application.Services.Interfaces;
using BlSpent.Application.UoW;
using BlSpent.Application.Repository;

namespace BlSpent.Application.Services.Implementation;

public class RolePageService : BaseService, IRolePageService
{
    private readonly IUnitOfWork _uoW;
    private readonly IRolePageRepository _rolePageRepository;
    private readonly IPageRepository _pageRepository;

    public RolePageService(
        IUnitOfWork unitOfWork,
        IRolePageRepository rolePageRepository,
        IPageRepository pageRepository,
        ISecurityContext securityContext) : base(securityContext)
    {
        _uoW = unitOfWork;
        _rolePageRepository = rolePageRepository;
        _pageRepository = pageRepository;
    }

    public async Task<RolePageModel?> AddOrUpdateRoleModifier(RolePageModel pageModel)
    {
        pageModel.Role = Core.Security.PageClaim.Modifier.Value;
        return await CreateOrUpdate(pageModel);
    }

    public async Task<RolePageModel?> AddOrUpdateRoleReadOnly(RolePageModel pageModel)
    {
        pageModel.Role = Core.Security.PageClaim.ReadOnly.Value;
        return await CreateOrUpdate(pageModel);
    }

    public async Task<RolePageModel?> GetByIdOrDefault(Guid id)
    {
        await _securityChecker.ThrowIfIsntLogged();
        using var connection = await _uoW.OpenConnectionAsync();
        return await _rolePageRepository.GetByIdOrDefault(id);
    }

    public async IAsyncEnumerable<RolePageModel> GetByPage(Guid rolePageId)
    {
        await _securityChecker.ThrowIfIsntLogged();
        using var connection = await _uoW.OpenConnectionAsync();
        await foreach (var rolePageModel in _rolePageRepository.GetByPage(rolePageId))
            yield return rolePageModel;
    }

    public async Task<RolePageModel?> RemoveByIdOrDefault(Guid rolePageId)
    {
        await _securityChecker.ThrowIfIsntLogged();

        var userId = (await _securityContext.GetCurrentClaim())?.UserId
            ?? throw new Core.Exceptions.UnauthorizedCoreException();

        using var connection = await _uoW.BeginTransactionAsync();

        var page = await _pageRepository.GetPagesWhichUserCanAccess(userId)
            .FirstOrDefaultAsync(page => page.Id == rolePageId);

        if (page is null)
            return null;

        _rolePageRepository.
    }

    public Task<RolePageModel?> UpdateByIdOrDefault(Guid rolePageId, RolePageModel pageModel)
    {
        throw new NotImplementedException();
    }

    private async Task<RolePageModel?> CreateOrUpdate(RolePageModel rolePageModel)
    {
        var rolePage = Mappings.Mapper.Map(rolePageModel);

        using var transaction = await _uoW.BeginTransactionAsync();

        var rolePageModelCreatedOrUpdated =
            await _rolePageRepository.CreateOrUpdate(rolePageModel.Id, rolePageModel);

        return rolePageModelCreatedOrUpdated;
    }
}