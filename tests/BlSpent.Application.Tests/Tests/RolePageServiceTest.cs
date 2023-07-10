using Microsoft.Extensions.DependencyInjection;
using BlSpent.Application.Services.Interfaces;
using BlSpent.Application.Model;
using BlSpent.Application.Services.Implementation;

namespace BlSpent.Application.Tests.Tests;

public class RolePageServiceTest : BaseTest
{
    private IRolePageService _rolePageService 
        => ServiceProvider.GetRequiredService<IRolePageService>();

    [Fact]
    public async Task CurrentOwnerGetByPage_CheckIfContainsSingleUser_Success()
    {
        var tupleContext = await CreatePageAndUser();

        using var context = CreateContext(tupleContext.User, tupleContext.Role);

        Assert.Single(
            await _rolePageService.CurrentOwnerGetByPage(tupleContext.Page.Id).ToListAsync()
        );
    }

    [Fact]
    public async Task CurrentOwnerRemove_CurrentOwnerRemoveOwnPage_FailedCantRemoveOwnPage()
    {
        var tupleContext = await CreatePageAndUser();
        
        using var context = CreateContext(tupleContext.User, tupleContext.Role);

        await Assert.ThrowsAnyAsync<BlSpent.Core.Exceptions.ForbiddenCoreException>(
            () => _rolePageService.CurrentOwnerRemove(tupleContext.Role.Id)
        );
    }

    [Fact]
    public async Task CurrentOwnerRemove_RemoveOtherUser_Success()
    {
        var tupleContext = await CreatePageUserAndInvite();

        using var context = CreateContext(tupleContext.Owner, tupleContext.RoleOwner);

        Assert.NotNull(
            await _rolePageService.CurrentOwnerRemove(tupleContext.RoleUser.Id)
        );
    }

    [Fact]
    public async Task CurrentOwnerUpdateRoleReadOnly_UpdateRoleToReadOnly_Success()
    {
        var tupleInvite = await CreatePageUserAndInvite();

        using var contextOwner = CreateContext(tupleInvite.Owner, tupleInvite.RoleOwner);

        Assert.NotNull(
            await _rolePageService.CurrentOwnerUpdateRoleReadOnly(new RolePageModel{
                Id = tupleInvite.RoleOwner.Id,
                PageId = tupleInvite.Page.Id,
                Role = Core.Security.PageClaim.ReadOnly.Value,
                UserId = tupleInvite.RoleUser.UserId
            })
        );
    }

    [Fact]
    public async Task CurrentOwnerUpdateRoleModifier_UpdateRoleToModifier_Success()
    {
        var tupleInvite = await CreatePageUserAndInvite(role: Core.Security.PageClaim.ReadOnly.Value);

        using var contextOwner = CreateContext(tupleInvite.Owner, tupleInvite.RoleOwner);
        
        Assert.NotNull(
            await _rolePageService.CurrentOwnerUpdateRoleModifier(new RolePageModel{
                Id = tupleInvite.RoleOwner.Id,
                PageId = tupleInvite.Page.Id,
                Role = Core.Security.PageClaim.Modifier.Value,
                UserId = tupleInvite.RoleUser.UserId
            })
        );
    }

    [Fact]
    public async Task RemoveByIdOrDefault_OwnerTryRemoveOwnRole_FailedToRemoveOwnerPage()
    {
        var provider = ServiceProvider;

        var tupleInvite = await CreatePageUserAndInvite(role: Core.Security.PageClaim.ReadOnly.Value, provider: provider);

        using var contextOwner = CreateContext(tupleInvite.Owner, tupleInvite.RoleOwner);

        var rolePageService = provider.GetRequiredService<IRolePageService>();

        await Assert.ThrowsAnyAsync<Core.Exceptions.ForbiddenCoreException>(
            () => rolePageService.RemoveByIdOrDefault(tupleInvite.RoleOwner.Id)
        );
    }

    [Fact]
    public async Task RemoveByIdOrDefault_UserTryRemove_Success()
    {
        var tupleInvite = await CreatePageUserAndInvite(role: Core.Security.PageClaim.ReadOnly.Value);

        using var contextOwner = CreateContext(tupleInvite.UserInvite, tupleInvite.RoleUser);
        
        Assert.NotNull(
            await _rolePageService.RemoveByIdOrDefault(tupleInvite.RoleUser.Id)
        );
    }

    private async Task<(UserModel Owner, PageModel Page, RolePageModel RoleOwner, UserModel UserInvite, RolePageModel RoleUser)> CreatePageUserAndInvite(
        string? role = null,
        IServiceProvider? provider = null)
    {
        provider = provider ?? ServiceProvider;

        var rolePageService = provider.GetRequiredService<IRolePageService>();

        var tupleOwner = await CreatePageAndUser(provider: provider);

        var userToAddInPage = await CreateUser(provider: provider);

        using var contextUserToAdd = CreateContext(userToAddInPage, 
            new RolePageModel{ PageId = tupleOwner.Page.Id }, isInvite: true);

        if (role is null)
            role = Core.Security.PageClaim.Modifier.Value;

        RolePageModel roleUserAdded;

        if (role == Core.Security.PageClaim.Modifier.Value)
            roleUserAdded = await rolePageService.InviteRoleModifier(new InviteRolePageModel{
                Email = userToAddInPage.Email,
                CreateDate = DateTime.Now,
                InvitationOwner = tupleOwner.User.Id,
                Role = role,
                PageId = tupleOwner.Page.Id
            });
        else
            roleUserAdded = await rolePageService.InviteRoleReadOnly(new InviteRolePageModel{
                Email = userToAddInPage.Email,
                CreateDate = DateTime.Now,
                InvitationOwner = tupleOwner.User.Id,
                Role = role,
                PageId = tupleOwner.Page.Id
            });

        return (tupleOwner.User, tupleOwner.Page, tupleOwner.Role, userToAddInPage, roleUserAdded);
    }

    private async Task<(UserModel User, PageModel Page, RolePageModel Role)> CreatePageAndUser(
        UserModel? user = null, 
        IServiceProvider? provider = null)
    {
        provider = provider ?? ServiceProvider;

        if (user is null)
            user = await CreateUser();

        CreateContext(user);

        var pageAndRole = await provider.GetRequiredService<IPageService>()
            .Create(Mocks.PageMock.ValidPage());

        return (user, pageAndRole.Page, pageAndRole.RolePage);
    }

    private async Task<UserModel> CreateUser(IServiceProvider? provider = null)
    {
        provider = provider ?? ServiceProvider;

        var userService =
            provider.GetRequiredService<IUserService>();

        return await userService.Create(Mocks.UserMock.ValidUser());
    }
}