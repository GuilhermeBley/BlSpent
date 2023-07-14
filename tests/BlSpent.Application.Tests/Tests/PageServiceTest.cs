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

        var tuplePage = await PageService.Create(Mocks.PageMock.ValidPage());

        Assert.NotNull(
            tuplePage.Page
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
    
    [Fact]
    public async Task CurrentPageGetByIdOrDefault_TryGetCurrent_Success()
    {
        var tuple = await CreatePageAndUser();

        using var context = CreateContext(tuple.User, tuple.Role);

        var pageGotten = await PageService.CurrentPageGetByIdOrDefault(tuple.Page.Id);

        Assert.NotNull(
            pageGotten
        );
    }

    [Fact]
    public async Task CurrentPageUpdate_TryUpdate_Success()
    {
        var tuple = await CreatePageAndUser();

        using var context = CreateContext(tuple.User, tuple.Role);

        var oldName = tuple.Page.PageName;
        tuple.Page.PageName = "updated"+oldName;
        
        var pageUpdated = 
            await PageService.CurrentPageUpdate(tuple.Page.Id, tuple.Page);

        Assert.NotEqual(oldName, pageUpdated.PageName);
    }

    [Fact]
    public async Task GetByUser_AddTwoPagesAndCheck_Success()
    {
        var tuplePage1 = await CreatePageAndUser();
        var tuplePage2 = await CreatePageAndUser(tuplePage1.User);

        var expectedPages = new Guid[] { tuplePage1.Page.Id, tuplePage2.Page.Id };

        using var context = CreateContext(tuplePage1.User, tuplePage1.Role);

        var pagesIdToCompare = await PageService.GetByUser(tuplePage1.User.Id)
            .Select(page => page.PageId)
            .ToListAsync();

        Assert.Contains(tuplePage1.Page.Id, pagesIdToCompare);
        Assert.Contains(tuplePage2.Page.Id, pagesIdToCompare);
    }
}