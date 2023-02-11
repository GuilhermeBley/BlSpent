using System.ComponentModel.DataAnnotations.Schema;

namespace BlSpent.Application.Tests.Models;

/// <summary>
/// Model of <see cref="BlSpent.Core.Entities.Earning"/>
/// </summary>
[Table("Earnings")]
public class GoalModel
{
    /// <summary>
    /// Target date to goal
    /// </summary>
    public DateTime TargetDate { get; set; }

    /// <summary>
    /// Value to goal
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Identifier of page
    /// </summary>
    public Guid PageId { get; set; }
    public PageModel PageModel { get; set; } = null!;
}