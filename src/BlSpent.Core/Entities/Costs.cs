using Smartec.Validations;

namespace BlSpent.Core.Entities;

/// <summary>
/// Represent a costs
/// </summary>
public class Costs : Entity
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

    private Costs(DateTime entryDate, BaseDate entryBaseDate, double value)
    {
        CostDate = entryDate;
        EntryBaseDate = entryBaseDate;
        Value = value;
    }

    /// <summary>
    /// Creates a costs
    /// </summary>
    /// <returns>new costs</returns>
    public static Costs Create(double value)
    {
        if (value > 0)
            throw new Exception();

        var dtTimeNow = DateTime.Now;
        return new Costs(dtTimeNow, new BaseDate(dtTimeNow), value);
    }
}