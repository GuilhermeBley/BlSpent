namespace BlSpent.Core.Entities;

/// <summary>
/// Represents a goals
/// </summary>
public class Goal : Entity
{
    /// <summary>
    /// Target date to goal
    /// </summary>
    public DateTime TargetDate { get; }

    /// <summary>
    /// Value to goal
    /// </summary>
    public double Value { get; }

    private Goal(DateTime targetDate, double value)
    {
        TargetDate = targetDate;
        Value = value;
    }

    public override bool Equals(object? obj)
    {
        if (!base.Equals(obj))
            return false;
        
        var goals = obj as Goal;
        if (goals is null)
            return false;

        if (!this.TargetDate.Equals(goals.TargetDate) ||
            !this.Value.Equals(goals.Value))
            return false;

        return true;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() * 458969457;
    }

    /// <summary>
    /// Creates a goals
    /// </summary>
    /// <returns>new goals</returns>
    public static Goal Create(DateTime target, double value)
    {
        if (target < DateTime.Now)
            throw new GenericCoreException("Target must be more than now date.");

        if (value < 0)
            throw new GenericCoreException("Invalid value, must be more than '0'.");

        return new Goal(target, value);
    }
}