using Smartec.Validations;

namespace BlSpent.Application.Model;

/// <summary>
/// Model of <see cref="BlSpent.Core.Entities.Earning"/>
/// </summary>
public class EarningModel
{
    /// <summary>
    /// Costs date
    /// </summary>
    public DateTime EarnDate { get; set; }

    /// <summary>
    /// Earn base date
    /// </summary>
    public BaseDate EntryBaseDate => new BaseDate(EarnDate);

    /// <summary>
    /// Value of earn
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Identifier of page
    /// </summary>
    public Guid PageId { get; set; }
}