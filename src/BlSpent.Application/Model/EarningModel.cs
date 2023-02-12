using Smartec.Validations;

namespace BlSpent.Application.Model;

/// <summary>
/// Model of <see cref="BlSpent.Core.Entities.Earning"/>
/// </summary>
public class EarningModel
{
    public Guid Id { get; set; }

    /// <summary>
    /// Costs date
    /// </summary>
    public DateTime EarnDate { get; set; }

    /// <summary>
    /// Earn base date
    /// </summary>
    public BaseDate EntryBaseDate { get; set; }

    /// <summary>
    /// Value of earn
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Identifier of page
    /// </summary>
    public Guid PageId { get; set; }
}