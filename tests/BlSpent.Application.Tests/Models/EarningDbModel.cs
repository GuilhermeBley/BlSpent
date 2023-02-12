using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlSpent.Application.Tests.Models;

/// <summary>
/// Model of <see cref="BlSpent.Core.Entities.Earning"/>
/// </summary>
[Table("Earnings")]
public class EarningDbModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Costs date
    /// </summary>
    public DateTime EarnDate { get; set; }

    /// <summary>
    /// Earn base date
    /// </summary>
    public DateTime EntryBaseDate { get; set; }

    /// <summary>
    /// Value of earn
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Identifier of page
    /// </summary>
    public Guid PageId { get; set; }
    public PageDbModel PageModel { get; set; } = null!;
}