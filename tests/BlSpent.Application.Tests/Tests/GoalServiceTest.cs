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

    [Fact]
    public async Task CreateValidRandomGoal_TryCreateWithoutLogin_FailedForbbiden()
    {
        var provider = ServiceProvider;

        var goalService = provider.GetRequiredService<IGoalService>();

        var userTupleCreated = await CreatePageAndUser(serviceProvider: provider);

        await Assert.ThrowsAnyAsync<Core.Exceptions.ForbiddenCoreException>(
            () => goalService.Add(
                GoalMock.CreateValidRandomGoal(userTupleCreated.Page.Id)
        ));
    }

    [Fact]
    public async Task GetByPageId_TryGetByPage_Success()
    {
        var provider = ServiceProvider;

        var goalService = provider.GetRequiredService<IGoalService>();

        var userTupleCreated = await CreatePageAndUser(serviceProvider: provider);

        using var ctx = CreateContext(userTupleCreated.User, userTupleCreated.Role);

        await goalService.Add(
                GoalMock.CreateValidRandomGoal(userTupleCreated.Page.Id));

        Assert.NotEmpty(
            await goalService.GetByPageId(userTupleCreated.Page.Id).ToListAsync()
        );
    }

    [Fact]
    public async Task GetByPageId_TryGetByPageWithoutLogin_FailedForbbiden()
    {
        var provider = ServiceProvider;

        var goalService = provider.GetRequiredService<IGoalService>();

        var userTupleCreated = await CreatePageAndUser(serviceProvider: provider);

        await Assert.ThrowsAnyAsync<Core.Exceptions.ForbiddenCoreException>(
            () => goalService.GetByPageId(userTupleCreated.Page.Id).ToListAsync().AsTask()
        );
    }

    [Fact]
    public async Task UpdateByIdOrDefault_TryUpdateNonExistent_FailedToUpdate()
    {
        var provider = ServiceProvider;

        var goalService = provider.GetRequiredService<IGoalService>();

        var userTupleCreated = await CreatePageAndUser(serviceProvider: provider);

        using var ctx = CreateContext(userTupleCreated.User, userTupleCreated.Role);

        await Assert.ThrowsAnyAsync<Core.Exceptions.NotFoundCoreException>(
            () => goalService.UpdateByIdOrDefault(Guid.NewGuid(), GoalMock.CreateValidRandomGoal(Guid.NewGuid()))
        );
    }

    [Fact]
    public async Task UpdateByIdOrDefault_TryUpdateWithoutLogin_FailedForbbiden()
    {
        var provider = ServiceProvider;

        var goalService = provider.GetRequiredService<IGoalService>();

        var userTupleCreated = await CreatePageAndUser(serviceProvider: provider);

        await Assert.ThrowsAnyAsync<Core.Exceptions.NotFoundCoreException>(
            () => goalService.UpdateByIdOrDefault(userTupleCreated.Page.Id, GoalMock.CreateValidRandomGoal(Guid.NewGuid()))
        );
    }

    [Fact]
    public async Task UpdateByIdOrDefault_TryUpdate_Success()
    {
        var provider = ServiceProvider;

        var goalService = provider.GetRequiredService<IGoalService>();

        var userTupleCreated = await CreatePageAndUser(serviceProvider: provider);

        using var ctx = CreateContext(userTupleCreated.User, userTupleCreated.Role);

        var goalCreated = await goalService.Add(
                GoalMock.CreateValidRandomGoal(userTupleCreated.Page.Id));

        Assert.NotNull(
            await goalService.UpdateByIdOrDefault(
                goalCreated.Id,
                GoalMock.CreateValidRandomGoal(userTupleCreated.Page.Id))
        );
    }
}
