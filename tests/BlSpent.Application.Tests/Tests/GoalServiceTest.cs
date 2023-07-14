using BlSpent.Application.Services.Interfaces;
using BlSpent.Application.Tests.Mocks;
using Microsoft.Extensions.DependencyInjection;

namespace BlSpent.Application.Tests.Tests;

public class GoalServiceTest : BaseTest
{
    [Fact]
    public async Task CreateValidRandomGoal_TryCreateLogged_Success()
    {
        var provider = ServiceProvider;

        var goalService = provider.GetRequiredService<IGoalService>();

        var userTupleCreated = await CreatePageAndUser(serviceProvider: provider);

        using var ctx = CreateContext(userTupleCreated.User, userTupleCreated.Role);

        Assert.NotNull(
            await goalService.Add(
                GoalMock.CreateValidRandomGoal(userTupleCreated.Page.Id)
        ));
    }
}
