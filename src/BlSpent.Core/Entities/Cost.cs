using Smartec.Validations;

namespace BlSpent.Core.Entities;

/// <summary>
/// Represent a costs
/// </summary>
public class Cost : Entity
{
    /// <summary>
    /// Costs date
    /// </summary>
    public DateTime CostDate { get; }

    /// <summary>
    /// Cost base date
    /// </summary>
    public BaseDate EntryBaseDate { get; }

    /// <summary>
    /// Value of cost
    /// </summary>
    public double Value { get; }

    private Cost(DateTime entryDate, BaseDate entryBaseDate, double value)
    {
        CostDate = entryDate;
        EntryBaseDate = entryBaseDate;
        Value = value;
    }

    public override bool Equals(object? obj)
    {
        if (!base.Equals(obj))
            return false;
        
        var cost = obj as Cost;
        if (cost is null)
            return false;
        if (!this.CostDate.Equals(cost.CostDate) ||
            !this.EntryBaseDate.Equals(cost.EntryBaseDate) ||
            !this.Value.Equals(cost.Value))
            return false;

        return true;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() * 1293129312;
    }

    /// <summary>
    /// Creates a new cost with current date
    /// </summary>
    /// <param name="value">Value of earn</param>
    /// <returns>new earn</returns>
    public static Cost CreateWithCurrentDate(double value)
    {
        return Create(DateTime.Now, value);
    }

    /// <summary>
    /// Creates a costs
    /// </summary>
    /// <param name="costDate">Date of costs</param>
    /// <param name="value">Value of earn</param>
    /// <returns>new costs</returns>
    public static Cost Create(DateTime costDate, double value)
    {
        if (value > 0)
            throw new GenericCoreException("Invalid value, must be less than '0'.");

        return new Cost(costDate, new BaseDate(costDate), value);
    }
}