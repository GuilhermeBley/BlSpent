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
    public async Task CurrentOwnerRemove_RemovePage_Success()
    {
        var tupleContext = await CreatePageAndUser();

        using var context = CreateContext(tupleContext.User, tupleContext.Role);

        var pageRemoved = await _rolePageService.CurrentOwnerRemove(tupleContext.Page.Id);

        Assert.Null(
            await _rolePageService.GetByIdOrDefault(tupleContext.Page.Id)
        );
    }


    [Fact]
    public async Task CurrentOwnerUpdateRoleModifier_RemovePage_Success()
    {
        var tupleContext = await CreatePageAndUser();

        var userToAddInPage = await CreateUser();

        using var context = CreateContext(tupleContext.User, tupleContext.Role);

        await _rolePageService.InviteRoleModifier(new InviteRolePageModel{
            Email = userToAddInPage.Email,
            CreateDate = DateTime.Now,
            InvitationOwner = tupleContext.User.Id,
            Role = Core.Security.PageClaim.Modifier.Value,
            PageId = tupleContext.Page.Id
        });

        Assert.NotNull(
            await _rolePageService.CurrentOwnerUpdateRoleReadOnly(new RolePageModel{
                PageId = tupleContext.Page.Id,
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