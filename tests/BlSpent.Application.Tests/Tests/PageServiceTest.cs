using Microsoft.Extensions.DependencyInjection;
using BlSpent.Application.Services.Interfaces;
using BlSpent.Application.Model;

namespace BlSpent.Application.Tests.Tests;

public class PageServiceTest : BaseTest
{
    private IPageService PageService => ServiceProvider.GetRequiredService<IPageService>();
    
    [Fact]
    public async Task Create_TryCreatePage_Success()
    {
        var newUser = await CreateUser();

        using var context = CreateContext(newUser);

        Assert.NotNull(
            await PageService.Create(Mocks.PageMock.ValidPage())
        );
    }
    
    [Fact]
    public async Task CurrentPageRemove_TryRemove_Success()
    {
        var tuple = await CreatePageAndUser();

        using var context = CreateContext(tuple.User, tuple.Role);

        var pageRemoved = await PageService.CurrentPageRemove(tuple.Page.Id);

        Assert.Null(
            await PageService.GetByUser(tuple.User.Id)
                .FirstOrDefaultAsync(page => page.PageId == pageRemoved.Id)
        );
    }

    private async Task<(UserModel User, PageModel Page, RolePageModel Role)> CreatePageAndUser()
    {
        var user =
            await CreateUser();

        CreateContext(user);

        var pageAndRole = await PageService.Create(Mocks.PageMock.ValidPage());

        return (user, pageAndRole.Page, pageAndRole.RolePage);
    }

    private async Task<UserModel> CreateUser()
    {
        var userService =
            ServiceProvider.GetRequiredService<IUserService>();

        return await userService.Create(Mocks.UserMock.ValidUser());
    }

    private Task<RolePageModel> GetByPage(Guid pageId)
    {
        throw new NotImplementedException();
    }
}