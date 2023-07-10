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
    private readonly IUserRepository _userRepository;

    public RolePageService(
        IUnitOfWork unitOfWork,
        IRolePageRepository rolePageRepository,
        IPageRepository pageRepository,
        ISecurityContext securityContext,
        IUserRepository userRepository) : base(securityContext)
    {
        _uoW = unitOfWork;
        _rolePageRepository = rolePageRepository;
        _pageRepository = pageRepository;
        _userRepository = userRepository;
    }

    public async Task<RolePageModel?> CurrentOwnerRemove(Guid rolePageId)
    {
        await _securityChecker.ThrowIfIsntOwner();

        using var transaction = _uoW.BeginTransactionAsync();

        var tupleInfoCurrentUser = await GetCurrentInfo();

        var rolePage = await _rolePageRepository.GetByPageAndUserOrDefault(tupleInfoCurrentUser.userId, tupleInfoCurrentUser.pageId);

        if (rolePage is null ||
            rolePage.Id != rolePageId)
            throw new Core.Exceptions.NotFoundCoreException("role not found.");

        if (rolePage.UserId == tupleInfoCurrentUser.userId)
            throw new Core.Exceptions.ForbiddenCoreException("Owners can't delete their own roles.");

        var rolePageRemoved = await _rolePageRepository.RemoveByIdOrDefault(rolePageId);

        await _uoW.SaveChangesAsync();

        return rolePageRemoved;
    }

    public async Task<RolePageModel> CurrentOwnerUpdateRoleModifier(RolePageModel rolePageModel)
    {
        await _securityChecker.ThrowIfIsntOwner();

        using var transaction = _uoW.BeginTransactionAsync();

        var tupleInfoCurrentUser = await GetCurrentInfo();

        if (rolePageModel.UserId == tupleInfoCurrentUser.userId)
            throw new Core.Exceptions.ForbiddenCoreException("Owners can't update their own roles.");

        if (rolePageModel.PageId != tupleInfoCurrentUser.pageId)
            throw new Core.Exceptions.ForbiddenCoreException("Invalid page.");

        var roleToUpdate = await _rolePageRepository.GetByPage(tupleInfoCurrentUser.pageId)
            .FirstOrDefaultAsync(role => role.Id == rolePageModel.Id);

        if (roleToUpdate is null)
            throw new Core.Exceptions.NotFoundCoreException("Role not found.");
        
        if (roleToUpdate.Role.Equals(Core.Security.PageClaim.Modifier.Value))
            return Mappings.Mapper.MapToRolePage(roleToUpdate);

        roleToUpdate.Role = Core.Security.PageClaim.Modifier.Value;

        var rolePageUpdated =
            await _rolePageRepository.UpdateByIdOrDefault(
                rolePageModel.Id, Mappings.Mapper.Map(roleToUpdate))
                ?? throw new Core.Exceptions.NotFoundCoreException("Role not found to update.");

        await _uoW.SaveChangesAsync();

        return rolePageUpdated;
    }

    public async Task<RolePageModel> CurrentOwnerUpdateRoleReadOnly(RolePageModel rolePageModel)
    {
        await _securityChecker.ThrowIfIsntOwner();

        using var transaction = _uoW.BeginTransactionAsync();

        var tupleInfoCurrentUser = await GetCurrentInfo();

        if (rolePageModel.UserId == tupleInfoCurrentUser.userId)
            throw new Core.Exceptions.ForbiddenCoreException("Owners can't update their own roles.");

        if (rolePageModel.PageId != tupleInfoCurrentUser.pageId)
            throw new Core.Exceptions.ForbiddenCoreException("Invalid page.");

        var roleToUpdate = await _rolePageRepository.GetByPage(tupleInfoCurrentUser.pageId)
            .FirstOrDefaultAsync(role => role.Id == rolePageModel.Id);

        if (roleToUpdate is null)
            throw new Core.Exceptions.NotFoundCoreException("Role not found.");
        
        if (roleToUpdate.Role.Equals(Core.Security.PageClaim.ReadOnly.Value))
            return Mappings.Mapper.MapToRolePage(roleToUpdate);

        roleToUpdate.Role = Core.Security.PageClaim.ReadOnly.Value;

        var rolePageUpdated =
            await _rolePageRepository.UpdateByIdOrDefault(
                rolePageModel.Id, Mappings.Mapper.Map(roleToUpdate))
                ?? throw new Core.Exceptions.NotFoundCoreException("Role not found to update.");

        await _uoW.SaveChangesAsync();

        return rolePageUpdated;
    }

    public async Task<RoleUserPageModel?> GetByIdOrDefault(Guid id)
    {
        await _securityChecker.ThrowIfIsntAuthorizedInPage();
        
        using var transaction = _uoW.BeginTransactionAsync();

        var tupleInfoCurrentUser = await GetCurrentInfo();

        var rolePageFound = 
            await _rolePageRepository.GetByPageAndUserOrDefault(tupleInfoCurrentUser.userId, id);

        return rolePageFound;
    }

    public async IAsyncEnumerable<RoleUserPageModel> CurrentOwnerGetByPage(Guid pageId)
    {
        await _securityChecker.ThrowIfIsntOwner();
        
        var tupleInfoCurrentUser = await GetCurrentInfo();

        if (pageId != tupleInfoCurrentUser.pageId)
            yield break;

        using var connection = await _uoW.OpenConnectionAsync();

        await foreach(var roleUserPageModel in _rolePageRepository.GetByPage(pageId))
            yield return roleUserPageModel;
    }

    public async IAsyncEnumerable<RoleUserPageModel> GetByPage(Guid pageId)
    {
        await _securityChecker.ThrowIfIsntAuthorizedInPage();
        
        var tupleInfoCurrentUser = await GetCurrentInfo();

        if (pageId != tupleInfoCurrentUser.pageId)
            throw new Core.Exceptions.ForbiddenCoreException("Invalid page.");

        await foreach(var roleUserPageModel in _rolePageRepository.GetByPage(pageId))
            yield return roleUserPageModel;
    }

    public async Task<RolePageModel> InviteRoleModifier(InviteRolePageModel invitePageModel)
    {
        await _securityChecker.ThrowIfIsntInvitation();

        if (!invitePageModel.Role.Equals(Core.Security.PageClaim.Modifier.Value))
            throw new Core.Exceptions.ForbiddenCoreException("Invalid role.");

        using var transaction = await _uoW.BeginTransactionAsync();
        
        var tupleInfoCurrentUser = await GetCurrentInfo();

        if (tupleInfoCurrentUser.pageId != invitePageModel.PageId)
            throw new Core.Exceptions.ForbiddenCoreException("Invalid Page.");

        var user = await _userRepository.GetByIdOrDefault(tupleInfoCurrentUser.userId)
            ?? throw new Core.Exceptions.UnauthorizedCoreException();

        if (!user.Email.Equals(invitePageModel.Email))
            throw new Core.Exceptions.ForbiddenCoreException("Invalid current user.");

        var ownerRolePage = 
            await _rolePageRepository.GetByPageAndUserOrDefault(invitePageModel.InvitationOwner, invitePageModel.PageId)
            ?? throw new Core.Exceptions.NotFoundCoreException("Invitation page not found.");

        if (ownerRolePage.Role != Core.Security.PageClaim.Owner.Value)
            throw new Core.Exceptions.NotFoundCoreException("Invitation owner not found.");

        var roleFoundOfUserPage =
            await _rolePageRepository.GetByPageAndUserOrDefault(tupleInfoCurrentUser.userId, tupleInfoCurrentUser.pageId);

        RolePageModel roleCreatedOrUpdated;
        if (roleFoundOfUserPage is null)
            roleCreatedOrUpdated = await _rolePageRepository.Add(
                Core.Entities.RolePage.CreateModifierRolePage(tupleInfoCurrentUser.userId, invitePageModel.PageId)
            );
        else
            roleCreatedOrUpdated = await _rolePageRepository.UpdateByIdOrDefault(
                roleFoundOfUserPage.Id, Core.Entities.RolePage.CreateModifierRolePage(tupleInfoCurrentUser.userId, invitePageModel.PageId)
            ) ?? throw new Core.Exceptions.NotFoundCoreException("Role not found.");

        await transaction.SaveChangesAsync();

        return roleCreatedOrUpdated;
    }

    public async Task<RolePageModel> InviteRoleReadOnly(InviteRolePageModel invitePageModel)
    {
        await _securityChecker.ThrowIfIsntInvitation();

        if (!invitePageModel.Role.Equals(Core.Security.PageClaim.ReadOnly.Value))
            throw new Core.Exceptions.ForbiddenCoreException("Invalid role.");

        using var transaction = await _uoW.BeginTransactionAsync();
        
        var tupleInfoCurrentUser = await GetCurrentInfo();

        if (tupleInfoCurrentUser.pageId != invitePageModel.PageId)
            throw new Core.Exceptions.ForbiddenCoreException("Invalid Page.");

        var user = await _userRepository.GetByIdOrDefault(tupleInfoCurrentUser.userId)
            ?? throw new Core.Exceptions.UnauthorizedCoreException();

        if (!user.Email.Equals(invitePageModel.Email))
            throw new Core.Exceptions.ForbiddenCoreException("Invalid current user.");

        var ownerRolePage = 
            await _rolePageRepository.GetByPageAndUserOrDefault(invitePageModel.InvitationOwner, invitePageModel.PageId)
            ?? throw new Core.Exceptions.NotFoundCoreException("Invitation page not found.");

        if (ownerRolePage.Role != Core.Security.PageClaim.Owner.Value)
            throw new Core.Exceptions.NotFoundCoreException("Invitation owner not found.");

        var roleFoundOfUserPage =
            await _rolePageRepository.GetByPageAndUserOrDefault(tupleInfoCurrentUser.userId, tupleInfoCurrentUser.pageId);

        RolePageModel roleCreatedOrUpdated;
        if (roleFoundOfUserPage is null)
            roleCreatedOrUpdated = await _rolePageRepository.Add(
                Core.Entities.RolePage.CreateReadOnlyRolePage(tupleInfoCurrentUser.userId, invitePageModel.PageId)
            );
        else
            roleCreatedOrUpdated = await _rolePageRepository.UpdateByIdOrDefault(
                roleFoundOfUserPage.Id, Core.Entities.RolePage.CreateReadOnlyRolePage(tupleInfoCurrentUser.userId, invitePageModel.PageId)
            ) ?? throw new Core.Exceptions.NotFoundCoreException("Role not found.");

        await transaction.SaveChangesAsync();

        return roleCreatedOrUpdated;
    }

    public async Task<RolePageModel> RemoveByIdOrDefault(Guid rolePageId)
    {
        await _securityChecker.ThrowIfIsntAuthorizedInPage();

        using var transaction = await _uoW.BeginTransactionAsync();

        var tupleInfoCurrentUser = await GetCurrentInfo();

        var currentRoleModel =
            await _rolePageRepository.GetByPageAndUserOrDefault(tupleInfoCurrentUser.userId, tupleInfoCurrentUser.pageId);

        if (currentRoleModel is null ||
            currentRoleModel.Id != rolePageId)
            throw new Core.Exceptions.ForbiddenCoreException("Invalid rolePageId.");

        var roleFound = await _rolePageRepository.GetByIdOrDefault(rolePageId)
            ?? throw new Core.Exceptions.NotFoundCoreException("Role not found to remove.");

        if (roleFound.Role == Core.Security.PageClaim.Owner.Value)
            throw new Core.Exceptions.ForbiddenCoreException("User cannot remove owner.");

        var rolePageRemoved =
            await _rolePageRepository.RemoveByIdOrDefault(rolePageId)
            ?? throw new Core.Exceptions.NotFoundCoreException("Role not found to remove.");

        await _uoW.SaveChangesAsync();

        return rolePageRemoved;
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