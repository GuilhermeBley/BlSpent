using Smartec.Validations;

namespace BlSpent.Application.Model;

/// <summary>
/// Model of <see cref="BlSpent.Core.Entities.Cost"/>
/// </summary>
public class CostModel
{
    public Guid Id { get; set; }
    
    /// <summary>
    /// Costs date
    /// </summary>
    public DateTime CostDate { get; set; }

    /// <summary>
    /// Cost base date
    /// </summary>
    public BaseDate EntryBaseDate { get; set; }

    /// <summary>
    /// Value of cost
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Identifier of page
    /// </summary>
    public Guid PageId { get; }

    public CostModel(Guid id, DateTime costDate, BaseDate entryBaseDate, double value, Guid pageId)
    {
        Id = id;
        CostDate = costDate;
        EntryBaseDate = entryBaseDate;
        Value = value;
        PageId = pageId;
    }
}