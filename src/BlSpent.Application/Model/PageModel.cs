namespace BlSpent.Application.Model;

/// <summary>
/// Model of <see cref="BlSpent.Core.Entities.Page"/>
/// </summary>
public class PageModel
{
    /// <summary>
    /// Identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Owner user
    /// </summary>
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