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

    public async Task<RolePageModel?> CurrentOwnerRemove(Guid rolePageId)
    {
        await _securityChecker.ThrowIfIsntOwner();

        using var transaction = _uoW.BeginTransactionAsync();

        var tuple = await GetCurrentInfo();

        var rolePage = await _rolePageRepository.GetByPageAndUserOrDefault(tuple.userId, tuple.pageId);

        if (rolePage is null ||
            rolePage.Id != rolePageId)
            throw new Core.Exceptions.NotFoundCoreException("role not found.");

        if (rolePage.UserId == tuple.userId)
            throw new Core.Exceptions.ForbiddenCoreException("Owners can't delete their own roles.");

        var rolePageRemoved = await _rolePageRepository.RemoveByIdOrDefault(rolePageId);

        await _uoW.SaveChangesAsync();

        return rolePageRemoved;
    }

    public async Task<RolePageModel> CurrentOwnerUpdateRoleModifier(RolePageModel rolePageModel)
    {
        await _securityChecker.ThrowIfIsntOwner();

        using var transaction = _uoW.BeginTransactionAsync();

        var tuple = await GetCurrentInfo();

        if (rolePageModel.UserId == tuple.userId)
            throw new Core.Exceptions.ForbiddenCoreException("Owners can't update their own roles.");

        if (rolePageModel.PageId != tuple.pageId)
            throw new Core.Exceptions.ForbiddenCoreException("Invalid page.");

        var roleToUpdate = await _rolePageRepository.GetByPage(tuple.pageId)
            .FirstOrDefaultAsync(role => role.Id == rolePageModel.Id);

        if (roleToUpdate is null)
            throw new Core.Exceptions.NotFoundCoreException("Role not found.");
        
        if (roleToUpdate.Role.Equals(Core.Security.PageClaim.Modifier.Value))
            return Mappings.Mapper.MapToRolePage(roleToUpdate);

        roleToUpdate.Role = Core.Security.PageClaim.Modifier.Value;

        var rolePageUpdated =
            await _rolePageRepository.UpdateByIdOrDefault(
                rolePageModel.Id, Mappings.Mapper.Map(roleToUpdate));

        if (rolePageUpdated is null)
            throw new Core.Exceptions.GenericCoreException($"Failed to update role '{roleToUpdate.Id}'");

        await _uoW.SaveChangesAsync();

        return rolePageUpdated;
    }

    public async Task<RolePageModel> CurrentOwnerUpdateRoleReadOnly(RolePageModel rolePageModel)
    {
        await _securityChecker.ThrowIfIsntOwner();

        using var transaction = _uoW.BeginTransactionAsync();

        var tuple = await GetCurrentInfo();

        if (rolePageModel.UserId == tuple.userId)
            throw new Core.Exceptions.ForbiddenCoreException("Owners can't update their own roles.");

        if (rolePageModel.PageId != tuple.pageId)
            throw new Core.Exceptions.ForbiddenCoreException("Invalid page.");

        var roleToUpdate = await _rolePageRepository.GetByPage(tuple.pageId)
            .FirstOrDefaultAsync(role => role.Id == rolePageModel.Id);

        if (roleToUpdate is null)
            throw new Core.Exceptions.NotFoundCoreException("Role not found.");
        
        if (roleToUpdate.Role.Equals(Core.Security.PageClaim.ReadOnly.Value))
            return Mappings.Mapper.MapToRolePage(roleToUpdate);

        roleToUpdate.Role = Core.Security.PageClaim.ReadOnly.Value;

        var rolePageUpdated =
            await _rolePageRepository.UpdateByIdOrDefault(
                rolePageModel.Id, Mappings.Mapper.Map(roleToUpdate));

        if (rolePageUpdated is null)
            throw new Core.Exceptions.GenericCoreException($"Failed to update role '{roleToUpdate.Id}'");

        await _uoW.SaveChangesAsync();

        return rolePageUpdated;
    }

    public async Task<RoleUserPageModel?> GetByIdOrDefault(Guid id)
    {
        await _securityChecker.ThrowIfIsntAuthorizedInPage();
        
        using var transaction = _uoW.BeginTransactionAsync();

        var tuple = await GetCurrentInfo();

        var rolePageFound = 
            await _rolePageRepository.GetByPageAndUserOrDefault(tuple.userId, id);

        return rolePageFound;
    }

    public async IAsyncEnumerable<RoleUserPageModel> CurrentOwnerGetByPage(Guid pageId)
    {
        await _securityChecker.ThrowIfIsntOwner();
        
        var tuple = await GetCurrentInfo();

        if (pageId != tuple.pageId)
            throw new Core.Exceptions.ForbiddenCoreException("Invalid page.");

        await foreach(var roleUserPageModel in _rolePageRepository.GetByPage(pageId))
            yield return roleUserPageModel;
    }

    public async IAsyncEnumerable<RoleUserPageModel> GetByPage(Guid pageId)
    {
        await _securityChecker.ThrowIfIsntAuthorizedInPage();
        
        var tuple = await GetCurrentInfo();

        if (pageId != tuple.pageId)
            throw new Core.Exceptions.ForbiddenCoreException("Invalid page.");

        await foreach(var roleUserPageModel in _rolePageRepository.GetByPage(pageId))
            yield return roleUserPageModel;
    }

    public async Task<RolePageModel> InviteRoleModifier(InviteRolePageModel invitePageModel)
    {
        await _securityChecker.ThrowIfIsntInvitation();

        if (!invitePageModel.Role.Equals(Core.Security.PageClaim.Modifier.Value))
            throw new Core.Exceptions.ForbiddenCoreException("Invalid role.");
        
        var tuple = await GetCurrentInfo();

        throw new NotImplementedException();
    }

    public async Task<RolePageModel> InviteRoleReadOnly(InviteRolePageModel invitePageModel)
    {
        await _securityChecker.ThrowIfIsntInvitation();
        throw new NotImplementedException();
    }

    public async Task<RolePageModel> RemoveByIdOrDefault(Guid rolePageId)
    {
        await _securityChecker.ThrowIfIsntAuthorizedInPage();
        throw new NotImplementedException();
    }

    private async Task<(Guid userId, Guid pageId)> GetCurrentInfo()
    {
        var claimModel = (await _securityContext.GetCurrentClaim()) ??
             throw new Core.Exceptions.ForbiddenCoreException();

        if (claimModel.UserId is null ||
            claimModel.PageId is null)
            throw new Core.Exceptions.ForbiddenCoreException();

        return (claimModel.UserId.Value, claimModel.PageId.Value);
    }
}