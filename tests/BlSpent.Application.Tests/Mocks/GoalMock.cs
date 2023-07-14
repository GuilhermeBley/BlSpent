using System;

namespace BlSpent.Application.Tests.Mocks;

public static class GoalMock
{
    private static Random _random = new();

    public static Model.GoalModel CreateValidRandomGoal(Guid pageId, DateTime? targetDateutc = null, double? value = null)
        => new Model.GoalModel
        {
            PageId = pageId,
            TargetDate = targetDateutc ?? DateTime.UtcNow.AddDays(1),
            Value = value ?? _random.NextDouble()
        };
}
