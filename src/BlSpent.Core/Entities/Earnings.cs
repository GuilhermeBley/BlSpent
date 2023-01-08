using Smartec.Validations;

namespace BlSpent.Core.Entities;

/// <summary>
/// Represents a earns
/// </summary>
public class Earnings : Entity
{
    /// <summary>
    /// Costs date
    /// </summary>
    public DateTime EarnDate { get; }

    /// <summary>
    /// Earn base date
    /// </summary>
    public BaseDate EntryBaseDate { get; }

    /// <summary>
    /// Value of earn
    /// </summary>
    public double Value { get; }

    private Earnings(DateTime earnDate, BaseDate entryBaseDate, double value)
    {
        EarnDate = earnDate;
        EntryBaseDate = entryBaseDate;
        Value = value;
    }

    /// <summary>
    /// Creates a new earn with current date
    /// </summary>
    /// <param name="value">Value of earn</param>
    /// <returns>new earn</returns>
    public static Earnings CreateWithCurrentDate(double value)
    {
        return Create(DateTime.Now, value);
    }

    /// <summary>
    /// Creates a new earn
    /// </summary>
    /// <param name="earnDate">Date of earn</param>
    /// <param name="value">Value of earn</param>
    /// <returns>new earn</returns>
    public static Earnings Create(DateTime earnDate, double value)
    {
        if (value < 0)
            throw new Exception();

        return new Earnings(earnDate, new BaseDate(earnDate), value);
    }
}