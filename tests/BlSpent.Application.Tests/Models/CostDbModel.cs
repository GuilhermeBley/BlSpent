using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlSpent.Application.Tests.Models;

/// <summary>
/// Model of <see cref="BlSpent.Core.Entities.Cost"/>
/// </summary>
[Table("Costs")]
public class CostDbModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Costs date
    /// </summary>
    public DateTime CostDate { get; set; }

    /// <summary>
    /// Cost base date
    /// </summary>
    public DateTime EntryBaseDate { get; set; }

    /// <summary>
    /// Value of cost
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Identifier of page
    /// </summary>
    public Guid PageId { get; }
    public PageDbModel PageModel { get; set; } = null!;
}