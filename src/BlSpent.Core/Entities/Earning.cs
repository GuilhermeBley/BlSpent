using Smartec.Validations;

namespace BlSpent.Core.Entities;

/// <summary>
/// Represents a earns
/// </summary>
public class Earning : Entity
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

    private Earning(DateTime earnDate, BaseDate entryBaseDate, double value)
    {
        EarnDate = earnDate;
        EntryBaseDate = entryBaseDate;
        Value = value;
    }

    public override bool Equals(object? obj)
    {
        if (!base.Equals(obj))
            return false;
        
        var earnings = obj as Earning;
        if (earnings is null)
            return false;

        if (!this.EarnDate.Equals(earnings.EarnDate) ||
            !this.EntryBaseDate.Equals(earnings.EntryBaseDate) ||
            !this.Value.Equals(earnings.Value))
            return false;

        return true;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() * 56785687;
    }

    /// <summary>
    /// Creates a new earn with current date
    /// </summary>
    /// <param name="value">Value of earn</param>
    /// <returns>new earn</returns>
    public static Earning CreateWithCurrentDate(double value)
    {
        return Create(DateTime.Now, value);
    }

    /// <summary>
    /// Creates a new earn
    /// </summary>
    /// <param name="earnDate">Date of earn</param>
    /// <param name="value">Value of earn</param>
    /// <returns>new earn</returns>
    public static Earning Create(DateTime earnDate, double value)
    {
        if (value < 0)
            throw new GenericCoreException("Invalid value, must be more than '0'.");

        return new Earning(earnDate, new BaseDate(earnDate), value);
    }
}