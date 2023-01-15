namespace BlSpent.Core.Tests.Entities;

public class GoalTest
{
    [Fact]  
    public void Create_TryCreateGoal_Success()
    {
        var goal = Goal.Create(
            target: DateTime.Now.AddHours(1), 
            value: 100.00, 
            pageId: Guid.NewGuid());

        Assert.NotNull(goal);
    }

    [Fact]  
    public void Create_TryCreateGoalWithEmptyGuid_Failed()
    {
        Assert.ThrowsAny<CoreException>(()=> 
            Goal.Create(
            target: DateTime.Now.AddHours(1), 
            value: 100.00, 
            pageId: Guid.Empty)
        );
    }

    [Fact]  
    public void Create_TryCreateGoalTargeSmallerThanNow_Failed()
    {
        Assert.ThrowsAny<CoreException>( () =>
            Goal.Create(
            target: DateTime.MinValue,
            value: 100.00,
            pageId: Guid.NewGuid()));
    }

    [Fact]  
    public void Create_TryCreateGoalWithNegativeValue_Failed()
    {
        Assert.ThrowsAny<CoreException>(()=> 
            Goal.Create(
            target: DateTime.Now.AddHours(1), 
            value: -100.00, 
            pageId: Guid.NewGuid())
        );
    }

    [Fact]  
    public void Equals_TryCheckEquals_Failed()
    {
        var goal1 = Goal.Create(
            target: DateTime.Now.AddHours(1),
            value: 100.00, 
            pageId: Guid.NewGuid());

        var goal2 = Goal.Create(
            target: DateTime.Now.AddHours(1),
            value: 200.00, 
            pageId: Guid.NewGuid());

        Assert.False(goal1.Equals(goal2));
    }

    [Fact]  
    public void Equals_TryCheckEquals_Success()
    {
        var goal1 = Goal.Create(
            target: DateTime.Now.AddHours(1),
            value: 100.00, 
            pageId: Guid.NewGuid());

        Assert.True(goal1.Equals(goal1));
    }
}