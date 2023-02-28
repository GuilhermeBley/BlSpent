using Microsoft.Extensions.DependencyInjection;
using BlSpent.Application.Services.Interfaces;
using BlSpent.Application.Model;

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
    public async Task CurrentOwnerUpdateRoleModifier_RemovePage_Success()
    {
        var tupleOwner = await CreatePageAndUser();

        var userToAddInPage = await CreateUser();

        using var contextUserToAdd = CreateContext(userToAddInPage, 
            new RolePageModel{ PageId = tupleOwner.Page.Id }, isInvite: true);

        await _rolePageService.InviteRoleModifier(new InviteRolePageModel{
            Email = userToAddInPage.Email,
            CreateDate = DateTime.Now,
            InvitationOwner = tupleOwner.User.Id,
            Role = Core.Security.PageClaim.Modifier.Value,
            PageId = tupleOwner.Page.Id
        });

        using var contextOwner = CreateContext(tupleOwner.User, tupleOwner.Role);

        Assert.NotNull(
            await _rolePageService.CurrentOwnerUpdateRoleReadOnly(new RolePageModel{
                Id = tupleOwner.Role.Id,
                PageId = tupleOwner.Page.Id,
                Role = Core.Security.PageClaim.ReadOnly.Value,
                UserId = userToAddInPage.Id
            })
        );
    }

    private async Task<(UserModel User, PageModel Page, RolePageModel Role)> CreatePageAndUser(UserModel? user = null)
    {
        if (user is null)
            user = await CreateUser();

        CreateContext(user);

        var pageAndRole = await ServiceProvider.GetRequiredService<IPageService>()
            .Create(Mocks.PageMock.ValidPage());

        return (user, pageAndRole.Page, pageAndRole.RolePage);
    }

    private async Task<UserModel> CreateUser()
    {
        var userService =
            ServiceProvider.GetRequiredService<IUserService>();

        return await userService.Create(Mocks.UserMock.ValidUser());
    }
}