using Smartec.Validations;

namespace BlSpent.Core.Entities;

/// <summary>
/// Represent a costs
/// </summary>
public class Costs : Entity
{
    /// <summary>
    /// Entry date
    /// </summary>
    public DateTime EntryDate { get; }

    /// <summary>
    /// Entry base date
    /// </summary>
    public BaseDate EntryBaseDate { get; }

    /// <summary>
    /// Value
    /// </summary>
    public double Value { get; }

    private Costs(DateTime entryDate, BaseDate entryBaseDate, double value)
    {
        EntryDate = entryDate;
        EntryBaseDate = entryBaseDate;
        Value = value;
    }

    /// <summary>
    /// Creates a costs
    /// </summary>
    /// <returns>new costs</returns>
    public static Costs Create(double value)
    {
        var dtTimeNow = DateTime.Now;
        return new Costs(dtTimeNow, new BaseDate(dtTimeNow), value);
    }
}