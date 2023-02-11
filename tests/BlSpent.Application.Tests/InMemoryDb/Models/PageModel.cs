using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlSpent.Application.Tests.Models;

/// <summary>
/// Model of <see cref="BlSpent.Core.Entities.Page"/>
/// </summary>
[Table("Pages")]
public class PageModel
{
    /// <summary>
    /// Identifier
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Owner user
    /// </summary>
    [Required]
    [StringLength(BlSpent.Core.Entities.Page.MAX_LENGTH_PAGE_NAME, MinimumLength = BlSpent.Core.Entities.Page.MIN_LENGTH_PAGE_NAME)]
    public string PageName { get; set; } = string.Empty;

    /// <summary>
    /// Create date of page
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// Concurrency stamp, represents last refresh
    /// </summary>
    public Guid ConcurrencyStamp { get; set; }
}