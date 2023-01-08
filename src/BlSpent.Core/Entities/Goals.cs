namespace BlSpent.Core.Entities;

/// <summary>
/// Represents a goals
/// </summary>
public class Goals : Entity
{
    /// <summary>
    /// Target date to goal
    /// </summary>
    public DateTime TargetDate { get; }

    /// <summary>
    /// Value to goal
    /// </summary>
    public double Value { get; }

    private Goals(DateTime targetDate, double value)
    {
        TargetDate = targetDate;
        Value = value;
    }

    /// <summary>
    /// Creates a goals
    /// </summary>
    /// <returns>new goals</returns>
    public static Goals Create(DateTime target, double value)
    {
        if (target < DateTime.Now)
            throw new Exception();

        if (value < 0)
            throw new Exception();

        return new Goals(target, value);
    }
}