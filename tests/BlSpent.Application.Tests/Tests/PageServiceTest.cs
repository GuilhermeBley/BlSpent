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
    public Task CurrentPageRemove_TryRemove_Success()
    {
        throw new NotImplementedException();
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